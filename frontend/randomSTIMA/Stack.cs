using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Stack
    {
        private List<string> friends;
        private int numFriends;
        public Stack()
        {
            friends = new List<string>();
            numFriends = 0;
        }

        public string Top()
        {
            return friends[numFriends - 1];
        }
        public void Push(string newElmt)
        {
            friends.Add(newElmt);
            numFriends++;
        }

        public string Pop()
        {
            if (numFriends > 0)
            {
                numFriends--;
                string result = friends[numFriends];
                friends.RemoveAt(numFriends);
                return result;

            }
            return "NULL";
        }

        public bool isEmpty() { return numFriends == 0; }

        public void Show()
        {
            Console.WriteLine("Friends: " + numFriends);
            Console.Write("Members: ");
            foreach(var elmt in friends)
            {
                Console.Write(elmt);
            }
            Console.WriteLine(" ");
        }
    }

}
