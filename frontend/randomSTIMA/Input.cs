using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    public class Input
    {
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
                index1 = Utility.findString(strList[0], listAll);
                if (index1 == -1)
                {
                    listAll.Add(strList);
                }
                else
                {
                    listAll[index1].Add(strList[1]);
                }

                index2 = Utility.findString(strList[1], listAll);
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

        //input tuple tanpa node hasil
        public static List<List<string>> inputToListTuple(string fileName, List<List<string>> nodeHasil)
        {
            int i;
            
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int count = Int32.Parse(lines[0]);
            List<List<string>> listAll = new List<List<string>>();

            for (i = 1; i <= count; i++)
            {
                bool ketemu = false;
                string[] strArr = lines[i].Split(' ');
                List<string> strList = strArr.ToList();
               
                foreach (var array in nodeHasil)
                {
                    if ((array[0]==strList[0] && array[1]==strList[1]) || (array[0]==strList[1] && array[1]==strList[0]))
                    {
                       ketemu = true;
                        
                    }
                }
                if (ketemu)
                {
                    continue;
                } else
                {
                    listAll.Add(strList);
                }
                
            }
            return listAll;
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
                temp.sortAdj();
                result.Add(temp);
            }
            
            return result;

        }

        public static List<Node> makeNodesExceptCurrAccount(List<List<string>> listAdj, string account)
        {
            List<Node> result = new List<Node>();
            foreach (var nodes in listAdj)
            {
                if (nodes[0] == account)
                {
                    continue;
                } else
                {
                    Node temp = new Node(nodes[0]);
                    for (int i = 1; i < nodes.Count; i++)
                    {
                        temp.addNewAdj(nodes[i]);
                    }
                    temp.sortAdj();
                    result.Add(temp);
                }
                
            }



            return result;

        }

        // return Node dengan menggunakan string
        public static Node getNode(string namaNode, List<Node> node)
        {
            foreach (var nodeName in node)
            {
                if (nodeName.getName() == namaNode)
                {
                    return nodeName;
                }
            }
            return node[0];
        }
       

    }
}
