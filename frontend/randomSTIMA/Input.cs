using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    public class Input
    {
        public Input()
        {
        }

        // membaca file dan menyimpan ke dalam array
        public static void readFile()
        {
            // harusnya mengembalikan list of tuple/list of string
        }

        // menjadikan 2 node saling bertetangga
        // misal ada (A, B) maka di adj punya A diisi B dan di adj punya B diisi A
        public static void makeAdjEachOther(Node a, Node b)
        {
            a.addNewAdj(b.getName());
            b.addNewAdj(a.getName());
        }

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

        public static bool isNodeExists(List<Node> listNode, string name)
        {
            return searchNode(listNode, name).getName() == "NULL";
        }

        public static List<List<string>> inputToList(string fileName)
        {
            int i, index1, index2;
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int count = Int32.Parse(lines[0]);
            List<List<string>> listAll = new List<List<string>>();

            for (i = 1; i <= count; i++)
            {
                string[] strArr = lines[i].Split(' ');
                List<string> strList = strArr.ToList();
                index1 = findString(strList[0], listAll);
                if (index1 == -1)
                {
                    listAll.Add(strList);
                }
                else
                {
                    listAll[index1].Add(strList[1]);
                }

                index2 = findString(strList[1], listAll);
                if (index2 == -1)
                {
                    List<string> listTemp = new List<string>();
                    listTemp.Add(strList[1]);
                    listTemp.Add(strList[0]);
                    listAll.Add(listTemp);
                }
                else
                {
                    listAll[index2].Add(strList[0]);
                }
            }
            return listAll;
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

        // membuat list of nodes
        public static List<Node> makeNodes(List<List<string>> listAdj)
        {
            List<Node> result = new List<Node>();
            foreach (var nodes in listAdj)
            {
                Node temp = new Node(nodes[0]);
                for (int i = 1; i < nodes.Count; i++)
                {
                    temp.addNewAdj(nodes[i]);
                }
                result.Add(temp);
            }
            return result;

        }

    }
}
