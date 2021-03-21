using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Stack
    {
        private List<Element> elements;
        private int numElements;
        public Stack()
        {
            elements = new List<Element>();
            numElements = 0;
        }

        public Element Top()
        {
            return elements[numElements - 1];
        }
        public void Push(Element newElmt)
        {
            elements.Add(newElmt);
            numElements++;
        }

        public Element Pop()
        {
            if (numElements > 0)
            {
                numElements--;
                Element result = elements[numElements];
                elements.RemoveAt(numElements);
                return result;

            }
            return new Element("NULL", 0);
        }

        public bool isEmpty() { return numElements == 0; }

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
