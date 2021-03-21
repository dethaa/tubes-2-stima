using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Stack
    {
        private List<string> elements;
        private int numElements;
        Stack()
        {
            elements = new List<string>();
            numElements = 0;
        }

        public void Push(string newElmt)
        {
            elements.Add(newElmt);
            numElements++;
        }

        public string Pop()
        {
            if (numElements > 0)
            {
                numElements--;
                string result = elements[numElements];
                elements.RemoveAt(numElements);
                return result;

            }

            return "NULL";

        }

        public void Show()
        {
            Console.WriteLine("Elements: " + numElements);
            Console.Write("Members: ");
            foreach(var elmt in elements)
            {
                Console.Write(elmt);
            }
            Console.WriteLine(" ");
        }
    }

}
