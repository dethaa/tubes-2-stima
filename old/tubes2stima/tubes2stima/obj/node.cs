using System;
namespace Application
{
    public class node {
        private string name;
        private string[] adj;
        private int numAdj;
        private bool visited;

        public node(string name, string[] adj, int numAdj) {
            this.name = name;
            this.adj = adj;
            this.numAdj = numAdj;
            this.visited = false;
        }
    }
}
