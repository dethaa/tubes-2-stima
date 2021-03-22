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

        public static int getNodeIdx(List<Node> listNode, string person)
        {
            int i = 0;
            foreach(var elmt in listNode)
            {
                if (elmt.getName() == person) { return i; }
                i++;
            }

            return -1;
        }

        public static bool isAllVisited(List<Node> listNode)
        {
            
            foreach(var adj in listNode)
            {
                if(!adj.isVisited()) { return false; }
            }

            return true;
            
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
        
        // mengembalikan mutual friend, dipastikan ada isinya
        public static List<string> getMutualFriend(List<Node> listNode, string person, string friend)
        {
            List<string> result = new List<string>();
            List<string> mutualFriend = DFS.getNodeWithAdj(listNode, friend);
            foreach (var i in mutualFriend)
            {
                if (Utility.searchNode(listNode, person).hasAdj(i))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        // mereset semua status menjadi false
        public static void resetStatus(List<Node> listNode)
        {
            foreach (var node in listNode) { node.notVisited(); }
        }

        // mengubah list hasil a b c d menjadi (a,b)(b,c)(c,d)
        public static List<List<string>> createRelationTuple(List<string> listNode)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> temp;
            for (int i = 0; i < listNode.Count - 1; i++)
            {
                temp = new List<string>();
                temp.Add(listNode[i]);
                temp.Add(listNode[i + 1]);
                result.Add(temp);
            }

            return result;
        }

    }
}
