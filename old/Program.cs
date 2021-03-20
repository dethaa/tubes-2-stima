using System;
using System.Linq;
using System.Collections.Generic;

namespace Application
{
    class inputFile {
        
        static void Main(string[] args) {
            int i;
            //Console.Write("Masukkan nama file: ");
            //string fileName = Console.ReadLine();
            //List<List<string>> listAll = inputToList(fileName);

            List<List<string>> listAll = inputToList("test1.txt");
            for (i=0; i<listAll.Count;i++){
                listAll[i].ForEach(Console.Write);
                Console.WriteLine();
            } 
        } 

        public static List<List<string>> inputToList(string fileName){
            int i, index1, index2;
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\HP\Documents\C#\" + fileName);
            int count = Int32.Parse(lines[0]);
            List<List<string>> listAll = new List<List<string>>();
            
            for(i=1;i<=count;i++){
                string[] strArr = lines[i].Split(' ');
                List<string> strList = strArr.ToList();
                index1 = findString(strList[0],listAll);
                if (index1 == -1){
                    listAll.Add(strList);
                } else {
                    listAll[index1].Add(strList[1]);
                }
                
                index2 = findString(strList[1],listAll);
                if (index2 == -1){
                    List<string> listTemp = new List<string>();
                    listTemp.Add(strList[1]);
                    listTemp.Add(strList[0]);
                    listAll.Add(listTemp);
                } else {
                    listAll[index2].Add(strList[0]);
                } 
            } 
            return listAll;
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
