using System;
using System.Collections.Generic;
using Gtk;

namespace tubes2stima
{
    class MainClass
    {
        static void Main(string[] args)
        {
            //Console.Write("Masukkan nama file: ");
            //string fileName = Console.ReadLine();
            //List<List<string>> listAll = inputToList(fileName);
            List<List<string>> test = Input.inputToList("test1.txt");
            List<Node> result = Input.makeNodes(test);

            foreach (var node in result)
            {
                node.showDetails();
            }
        }
       
    }
}
