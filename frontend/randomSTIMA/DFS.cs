using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class DFS
    {
        public DFS()
        {

        }

        public static bool checkIfInResult(List<Element> result, string name)
        {
            foreach (var elmt in result)
            {
                if(elmt.getName() == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static string getAdjNotVisited(List<Element> result, Node A)
        {
            List<string> adjCopy = new List<string>();
            foreach (var adj in A.getAllAdj())
            {
                adjCopy.Add(adj);
            }

            Node copy = new Node(A.getName(), adjCopy);
            bool found = false;
            while(!found)
            {
                if(checkIfInResult(result, copy.getPriortyAdj())) {
                    adjCopy.Remove(copy.getPriortyAdj());
                    copy.setAdj(adjCopy);
                } else
                {
                    return copy.getPriortyAdj();
                }
            }

            return "NULL";
        }

        // Mencari mutual friend dari person melalui friend. Dipastikan person dan
        // friend ada di
        public static List<Element> GetMutuals(List<Node> nodes, string person, string friend)
        {
            List<Element> result = new List<Element>();
            Stack stack = new Stack();
            stack.Push(new Element(person, 0));
            Node firstPerson = Utility.searchNode(nodes, person);
            Element previous = new Element("NULL", 0);
            List<string> initAdjFirst = firstPerson.getAllAdj();
            while(!stack.isEmpty())
            {
                if (stack.Top().getName() == person)
                {
                    previous = stack.Pop();
                } else
                {
                    if(getAdjNotVisited(result, Utility.searchNode(nodes, previous.getName())) == "NULL")
                    {
                        previous = stack.Pop();
                        result.Add(previous);
                    } else
                    {
                        if (previous.isNull()) {
                            previous.setElement(firstPerson.getPriortyAdj(previous.getName()), previous.getDegree()+1);
                        } else
                        {
                            if (previous.getDegree() > stack.Top().getDegree())
                            {
                                previous.setElement(getAdjNotVisited(result, Utility.searchNode(nodes, previous.getName())), previous.getDegree()-1);
                            } else
                            {
                                previous.setElement(getAdjNotVisited(result, Utility.searchNode(nodes, previous.getName())), previous.getDegree() + 1);
                            }
                            stack.Push(previous);
                        }
                    }

                }

            }
            // for now
            return result;
        }
    }
}
