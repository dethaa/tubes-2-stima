using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Utility
    {
        public Utility() {}

        // mencari node dengan nama .. di list of node
        // dipastikan ada node dengan nama yang dicari
        public static Node searchNode(List<Node> listNode, string name)
        {
            foreach (var node in listNode)
            {
                if (node.isNodeName(name))
                {
                    return node;
                }
            }

            // ini biar gak error aja walaupun sebenernya gak bakal kesini sih
            return new Node("NULL");

        }

        public static void makeAdjEachOther(Node a, Node b)
        {
            a.addNewAdj(b.getName());
            b.addNewAdj(a.getName());
        }

        public static bool isNodeExists(List<Node> listNode, string name)
        {
            return searchNode(listNode, name).getName() == "NULL";
        }

        public static int findString(string str, List<List<string>> listAll)
        {
            int k = 0;
            while (k < listAll.Count)
            {
                if (str == listAll[k][0])
                {
                    return k;
                }
                k++;
            }
            return -1;
        }

        public static string compareString(string A, string B)
        {
            if (String.Compare(A, B) == 1) {
                return A;
            } else
            {
                return B;
            }
        }

        public static Node compareNode(Node A, Node B)
        {
            if (compareString(A.getName(), B.getName()) == A.getName())
            {
                if (A.isVisited())
                {
                    return A;
                }

                return B;
            } else
            {
                if (B.isVisited())
                {
                    return A;
                }
                return B;
            }
        }

    }
}
