using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Friend
    {
        private string name;
        private int degree;
        private List<string> mutuals;
        public Friend(string name, int degree)
        {
            this.name = name;
            this.degree = degree;
            this.mutuals = new List<string>();
        }

        public string getName() { return name; }
        public int getDegree() { return degree; }

        public List<string> getMutuals() { return mutuals; }

        public void setMutuals(List<string> mutuals) { this.mutuals = mutuals; }
        public void show()
        {
            Console.WriteLine(name + " , " + degree);
        }

        public bool isNull()
        {
            return name == "NULL" && degree == 0;
        }

        public void setFriend(string name, int degree)
        {
            this.name = name;
            this.degree = degree;
        }
    }
}
