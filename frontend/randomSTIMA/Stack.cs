using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomSTIMA
{
    class Stack
    {
        /* Atribut dari stack */
        /* Menyimpan nama node */
        private List<string> friends;
        /* Menyimpan total isi stack saat ini */
        private int numFriends;

        /* Constructor */
        public Stack()
        {
            friends = new List<string>();
            numFriends = 0;
        }

        /* Mengembalikan elemen teratas stack */
        public string Top()
        {
            if (numFriends > 0)
            {
                 return friends[numFriends - 1];
            }
            return "NULL";
        }
        
        /* Memasukkan elemen baru ke stack sebagai elemen teratas */
        public void Push(string newElmt)
        {
            friends.Add(newElmt);
            numFriends++;
        }

        /* Mengembalikan elemen teratas dari stack dan menghapusnya dari stack */
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

        /* Mengembalikan apakah stack kosong atau tidak */
        public bool isEmpty() { return numFriends == 0; }

        /* Mengoutput seluruh atribut stack ke layar */
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
