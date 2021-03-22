using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyobadewe
{
    class Utility
    {
        public Utility() { }

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
            foreach (var elmt in listNode)
            {
                if (elmt.getName() == person) { return i; }
                i++;
            }

            return -1;
        }

        public static bool isAllVisited(List<Node> listNode)
        {

            foreach (var adj in listNode)
            {
                if (!adj.isVisited()) { return false; }
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

        public static int findString2(string str, List<string> listStr)
        {
            int k = 0;
            while (k < listStr.Count)
            {
                if (str == listStr[k])
                {
                    return k;
                }
                k++;
            }
            return -1;
        }

        public static string compareString(string A, string B)
        {
            if (String.Compare(A, B) == 1)
            {
                return A;
            }
            else
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
            }
            else
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
            foreach (var node in listNode)
            {
                if (node.getName() != person)
                {
                    if (node.hasAdj(friend) && node.hasAdj(person))
                    {
                        result.Add(node.getName());
                    }
                }
            }
            return result;
        }

        //mengembalikan tetangga yang belum dikunjungi dari suatu node
        public static List<string> getUnvisitedAdj(Node node, List<Node> listNode)
        {
            List<string> result = new List<string>();
            List<string> listAdj = node.getAllAdj();
            foreach (var name in listAdj)
            {
                if (!searchNode(listNode,name).isVisited())
                {
                    result.Add(name);
                }
            }
            return result;
        }

        //men-set semua node yang namanya terdapat pada listStr menjadi sudah dikunjungi
        public static void setAllVisited(List<Node> listNode, List<string> listStr)
        {
            foreach (var name in listStr)
            {
                listNode[getNodeIdx(listNode, name)].hasVisited();
            }
        }

        //mengubah parent dari node yang namanya berada pada listStr menjadi parentName
        public static void setAllParent(List<Node> listNode, List<string> listStr, string parentName)
        {
            foreach (var name in listStr)
            {
                listNode[getNodeIdx(listNode, name)].setParent(parentName);
            }
        }

        // mereset semua status menjadi false dan parent menjadi null
        public static void resetStatus(List<Node> listNode)
        {
            foreach (var node in listNode) 
            { 
                node.notVisited();
                node.setParent("null");
            }

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
