using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    public class Node
    {
        // attributes
        private string name;
        private List<string> adj;
        private int numAdj;
        private bool visited;


        // constructor
        public Node(string name)
        {
            this.name = name;
            adj = new List<string>();
            numAdj = 0;
            visited = false;
        }

        // getter

        // mengambil nama node
        public string getName() { return name; }
        public List<string> getAllAdj() { return adj; }
        public string getAdjOnIdx(int idx) { return adj[idx]; }
        public int getNumAdj() { return numAdj; }
        public bool isVisited() { return visited; }

        // methods

        // menambahkan tetangga baru
        public void addNewAdj(string name)
        {
            adj.Add(name);
            numAdj++;
        }

        // memeriksa apakah node punya tetangga dengan nama ..
        public bool hasAdj(string name)
        {
            foreach (var node in adj)
            {
                if (node == name) { return true; }
            }

            return false;

        }

        // memeriksa apakah nama node sama dengan ...
        public bool isNodeName(string name) { return this.name == name; }

        // mengubah status node menjadi telah dikunjungi
        public void hasVisited() { visited = true; }

        // mengubah status node menjadi belum dikunjungi
        public void notVisited() { visited = false; }

        // output methods

        // menampilkan semua tetangga ke layar
        public void printAdj()
        {
            int i;
            for (i = 0; i < numAdj; i++)
            {
                Console.Write(getAdjOnIdx(i) + " ");
            }
            Console.Write("\n");
        }

        // menampilkan informasi node
        public void showDetails()
        {
            Console.WriteLine("Nama node: " + name);
            Console.WriteLine("Jumlah tetangga: " + numAdj);
            Console.Write("Daftar tetangga: ");
            printAdj();
            Console.WriteLine("Dikungjungi: " + visited + "\n");
        }
    }

}