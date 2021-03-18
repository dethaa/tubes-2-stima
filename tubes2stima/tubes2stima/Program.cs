using System;
using Gtk;

namespace tubes2stima
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] testarr = { "a", "b", "c" };
            Node test = new Node("Test", testarr, 3);
            test.showDetails();
        }
    }
}
