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

        public static int getMaxMutualIdx(List<Node> listNode, List<string> friends, string person){
            int idx = 0, mutuals = Utility.getMutualFriend(listNode,friends[0], person).Count;
            for (int i = 1; i < friends.Count; i++) {
                if (Utility.getMutualFriend(listNode, friends[i], person).Count > mutuals) {
                    mutuals = Utility.getMutualFriend(listNode,friends[i], person).Count;
                    idx = i;
                }
            }

            return idx;
        }

        public static List<string> sortRecommend(List<Node> listNode,List<string> friends, string person) 
        {
            int idx;
            List<string> result = new List<string>();
            while (friends.Count > 0) {
                idx = getMaxMutualIdx(listNode, friends, person);
                result.Add(friends[idx]);
                friends.RemoveAt(idx);
            }

            return result;
        }

        //mengembalikan rekomendasi teman yang memiliki mutual friend yang sama dengan simpul bernama person
        public static List<string> Recommend(List<Node> listNode, string person)
        {
            Node personNode = Utility.searchNode(listNode, person);
            listNode[Utility.getNodeIdx(listNode, person)].hasVisited();
            Node friendNode;
            List<string> newFriends = new List<string>();

            while(!personNode.isAllAdjVisited(listNode))
            {
                friendNode = Utility.searchNode(listNode, personNode.getPriorityAdj(listNode));
                listNode[Utility.getNodeIdx(listNode, friendNode.getName())].hasVisited();
                while(!friendNode.isAllAdjVisited(listNode))
                {
                    string recommendedFriend = friendNode.getPriorityAdj(listNode);
                    if (!personNode.hasAdj(recommendedFriend))
                    {
                        newFriends.Add(recommendedFriend);
                    }
                    listNode[Utility.getNodeIdx(listNode, recommendedFriend)].hasVisited();

                }
            }

            Utility.resetStatus(listNode);
            return sortRecommend(listNode,newFriends, person);
        }

        /* Mengembalikan list of (list of string) yang merepresentasikan sisi sisi hingga terbentuk jalur pertemanan dari simpul person ke simpul friend. 
         * Penelusuran menggunakan algoritma DFS.
         * Isi list kosong apabila tidak terdapat koneksi. */
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
        public static List<string> ExploreNotTuple(List<Node> listNode, string person, string friend)
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
                }
                else
                {
                    adj = temp.getPriorityAdj(listNode);
                    temp = Utility.searchNode(listNode, adj);
                    listNode[Utility.getNodeIdx(listNode, temp.getName())].hasVisited();
                    stack.Push(adj);
                }

            }

            if (stack.Top() == friend)
            {
                while (!stack.isEmpty())
                {
                    result.Add(stack.Pop());
                }

            }

            Utility.resetStatus(listNode);
            result.Reverse();
            return result;
        }
    }
}
