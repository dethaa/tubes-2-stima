using System;
using System.Collections.Generic;

public class Node {
    private string name;
    private List<string> adj;
    private int numAdj;
    private bool visited;

    public Node(string name)
    {
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