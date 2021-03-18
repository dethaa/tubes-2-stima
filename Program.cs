using System;
using System.Collections.Generic;

namespace Application
{
    class inputFile {
        /*
        static void Main(string[] args)
        {
            //Console.Write("Masukkan nama file: ");
            //string fileName = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\HP\Documents\C#\test1.txt");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\HP\Documents\C#\" + fileName);
            //System.Console.WriteLine("Isi dari " + fileName + ":");
            foreach (string line in lines){
                Console.WriteLine(line);
            }
            int count = Int32.Parse(lines[0]);
            //System.Console.WriteLine(lines.Length);
            //string[] words = lines[1].Split(' ');
            //foreach (string word in words){
                //Console.WriteLine(word);

            int i;
            for (i=1;i<count;i++){
                string[] words = lines[i].Split(' ');
                foreach (string word in words){
                    Console.WriteLine(word);
                }
            }
        } */

        public void input(string fileName){
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\HP\Documents\C#\" + fileName);
            System.Console.WriteLine("Isi dari " + fileName + ":");
            int count = Int32.Parse(lines[0]);

            List<List<string>> list1 = new List<List<string>>();
            
            int i;
            for (i=1;i<count;i++){
                List<string> list2 = new List<string>();
                string[] words = lines[i].Split(' ');
                foreach (string word in words){
                    list2.Add(word); 
                }
                list1.Add(list2);
                //A,B
            }
        }
    } 
    public class Node {
        private string name;
        private List<string> adj;
        private int numAdj;
        private bool visited;

        public Node(string name){
        this.name = name;
        adj = new List<string>();
        this.numAdj = 0;
        this.visited = false;
        Console.WriteLine("ctor ok");
        }
        public string getName() { return name; }
        public string getAdjOnIdx(int idx) { return this.adj[idx]; }
        public int getNumAdj() { return numAdj; }
        public bool isVisited() { return visited; }
        public void printAdj()
        {
            int i;
            for(i = 0; i < this.numAdj; i++)
            {
                Console.Write(this.getAdjOnIdx(i)+ " ");
            }
            Console.WriteLine("\n");
        }
        public void showDetails()
        {
            Console.WriteLine("Nama node: " + this.name);
            Console.WriteLine("Jumlah tetangga: " + this.numAdj);
            Console.Write("Daftar tetangga: ");
            this.printAdj();
        }
    }
 
}
