using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Element
    {
        private string name;
        private int degree;
        public Element(string name, int degree)
        {
            this.name = name;
            this.degree = degree;
        }

        public string getName() { return name; }
        public int getDegree() { return degree; }
        public void show()
        {
            Console.WriteLine(name + " , " + degree);
        }

        public bool isNull()
        {
            return name == "NULL" && degree == 0;
        }

        public void setElement(string name, int degree)
        {
            this.name = name;
            this.degree = degree;
        }
    }
}
