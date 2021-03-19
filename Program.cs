using System;
using System.Collections.Generic;

namespace Application
{
    class inputFile {
        
        static void Main(string[] args) {
            int i;
            //Console.Write("Masukkan nama file: ");
            //string fileName = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\HP\Documents\C#\test1.txt");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\HP\Documents\C#\" + fileName);
            //System.Console.WriteLine("Isi dari " + fileName + ":");
            foreach (string line in lines){
                //Console.WriteLine(line);
            }
            int count = Int32.Parse(lines[0]);
            //System.Console.WriteLine(lines.Length);
            //string[] words = lines[1].Split(' ');
            //foreach (string word in words){
                //Console.WriteLine(word);

            List<List<string>> listAll = new List<List<string>>();
            List<string> listTemp = new List<string>();
            for(i=1;i<count;i++){
                int index1,index2;
                string[] words = lines[i].Split(' ');
                index1 = findString(words[0],listAll);
                if (index1 == -1){
                    listTemp.Clear();
                    listTemp.AddRange(words);
                } else {
                    listAll[index1].Add(words[1]);
                }

                index2 = findString(words[1],listAll);
                if (index2 == -1){
                    listTemp.Clear();
                    listTemp.Add(words[1]);
                    listTemp.Add(words[0]);
                } else {
                    listAll[index2].Add(words[0]);
                }
            }
            listAll.ForEach(Console.WriteLine);
        } 

        public static int findString(string str, List<List<string>> listAll){
            int k = 0;
            while (k < listAll.Count){
                if (str == listAll[k][0]){
                    return k;
                }
                k++;
            }
            return -1;
        }
    } 

}
