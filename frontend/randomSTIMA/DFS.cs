﻿using System;
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
            Node friendNode;
            List<string> newFriends = new List<string>();
            foreach (var friend in personNode.getAllAdj())
            {
                friendNode = Utility.searchNode(listNode, friend);
                foreach (var rec in friendNode.getAllAdj())
                {
                    if (!personNode.hasAdj(rec) && rec != person && !newFriends.Contains(rec))
                    {
                        newFriends.Add(rec);
                    }
                }
            }

            return newFriends;
        }

        public static List<string> Explore(List<Node> listNode, string person, string friend)
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


            return result;
        }
    }
}
