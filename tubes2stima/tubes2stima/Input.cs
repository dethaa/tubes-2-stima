using System;
using System.Collections.Generic;

namespace tubes2stima
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
                if(node.isNodeName(name)) {
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

            //List<Node> result = new List<Node>();

            //string prev = "NULL";
            /* A B
             * A C
             * A D
             * B C
             */

            //Node temp;
            //for (int i = 0; i < result.Count; i++) {
            //    //if (prev == "NULL" || prev != listAdj[i][0])
            //    //{
            //    if (!isNodeExists(result, listAdj[i][0]))
            //    {
            //        temp = new Node(listAdj[i][0]);
            //        result.Add(temp);

            //    }

            //    if (!isNodeExists(result, listAdj[i][1]))
            //    {
            //        temp = new Node(listAdj[i][1]);
            //        result.Add(temp);

            //    }

            //    Node node1 = searchNode(result, listAdj[i][0]);
            //    Node node2 = searchNode(result, listAdj[i][1]);


            //    makeAdjEachOther(node1, node2);
                    
                    //prev = listAdj[i][0];
                    
                //} else {
                //    if (!isNodeExists(result, listAdj[i][1]))
                //    {
                //        temp = new Node(listAdj[i][1]);
                //        result.Add(temp);

                //    }

                //    makeAdjEachOther(searchNode(result, listAdj[i][0]), searchNode(result, listAdj[i][1]));

                //}
            // }
            return result;
            
        }
    }
}
