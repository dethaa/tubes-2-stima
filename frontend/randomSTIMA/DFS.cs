using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class DFS
    {
        public DFS()
        {

        }

        public static List<string> getNodeWithAdj(List<Node> listNode, string person)
        {
            List<string> result = new List<string>();
            foreach (var elmt in listNode)
            {
                if (elmt.hasAdj(person)) { result.Add(elmt.getName()); }
            }

            return result;
        }
        public static List<string> Recommend(List<Node> listNode, string person)
        {
            Node personNode = Utility.searchNode(listNode, person);
            listNode[Utility.getNodeIdx(listNode, person)].hasVisited();
            Node friendNode;
            // Stack stack = new Stack();
            List<string> newFriends = new List<string>();

            while(!personNode.isAllAdjVisited(listNode))
            {
                friendNode = Utility.searchNode(listNode, personNode.getPriorityAdj(listNode));
                listNode[Utility.getNodeIdx(listNode, friendNode.getName())].hasVisited();
                while(!friendNode.isAllAdjVisited(listNode))
                {
                    newFriends.Add(friendNode.getPriorityAdj(listNode));
                    listNode[Utility.getNodeIdx(listNode, friendNode.getPriorityAdj(listNode))].hasVisited();
                }
            }

            Utility.resetStatus(listNode);
            return newFriends;
        }

        public static List<List<string>> Explore(List<Node> listNode, string person, string friend)
        {
            Node temp = Utility.searchNode(listNode, person);
            Stack stack = new Stack();

            string garbage;
            string adj = person;
            List<string> result = new List<string>();

            stack.Push(adj);
            listNode[Utility.getNodeIdx(listNode, person)].hasVisited();
            while (stack.Top() != friend && !Utility.isAllVisited(listNode) && !stack.isEmpty())
            {
                if (temp.getPriorityAdj(listNode) == "NULL")
                {
                    garbage = stack.Pop();
                    temp = Utility.searchNode(listNode, stack.Top());
                } else
                {
                    adj = temp.getPriorityAdj(listNode);
                    temp = Utility.searchNode(listNode, adj);
                    listNode[Utility.getNodeIdx(listNode, temp.getName())].hasVisited();
                    stack.Push(adj);
                }

            }

            if (stack.Top() == friend)
            {
                while(!stack.isEmpty())
                {
                    result.Add(stack.Pop());
                }
                
            }

            Utility.resetStatus(listNode);
            result.Reverse();
            return Utility.createRelationTuple(result);
        }
    }
}
