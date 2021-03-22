using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyobadewe
{
    class BFS
    {
        public static List<List<string>> Explore(List<Node> listNode, string person, string friend)
        {
            Node startNode = Utility.searchNode(listNode, person);
            Stack stack = new Stack();

            string adj = person;
            List<string> result = new List<string>();
            stack.Push(adj);
            listNode[Utility.getNodeIdx(listNode, person)].hasVisited();
            
             while (stack.Top() != friend && !Utility.isAllVisited(listNode))
             {
                List<string> listTemp1 = Utility.getUnvisitedAdj(startNode, listNode);
                int i = Utility.findString2(friend, listTemp1);
                if (i == -1)
                {
                    Utility.setAllVisited(listNode, listTemp1);
                    int k = 0;
                    while (listTemp1.Any())
                    {
                        Node temp2 = Utility.searchNode(listNode, listTemp1[0]);
                        List<string> listTemp2 = Utility.getUnvisitedAdj(temp2, listNode);
                        Utility.setAllParent(listNode, listTemp2, listTemp1[0]);

                        int j = Utility.findString2(friend, listTemp2);
                        if (j == -1)
                        {
                            Utility.setAllVisited(listNode, listTemp2);
                            listTemp1.RemoveAt(0);
                            listTemp1.AddRange(listTemp2);
                           
                        }
                        else
                        {
                            pushCheck(stack, listTemp2[j], listNode);
                            listTemp1.Clear();
                        }
                    }
                }
                else
                {
                    stack.Push(listTemp1[i]);
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

        public static void pushCheck(Stack stack, string str, List<Node> listNode)
        {
            Node temp = Utility.searchNode(listNode, str);
            if (temp.getParent() == "null")
            {
                stack.Push(str);
            }
            else
            {
                pushCheck(stack, temp.getParent(), listNode);
                stack.Push(str);
            }
        }

    }
}
