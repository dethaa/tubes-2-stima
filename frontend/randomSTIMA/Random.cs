using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;

namespace randomSTIMA
{
    public partial class Random : Form
    {
        public Random()
        {
            InitializeComponent();
        }

        List<List<string>> inputTest;
        List<Node> nodeTest;
        List<List<string>> hasil;
        List<List<string>> tupleInput;
        bool inputAda;

        private Label newLabel;

        private OpenFileDialog browse = new OpenFileDialog();

        private string filename;

        //Browse
        private void button1_Click(object sender, EventArgs e)
        {
            
            browse.Filter = "*.txt (file berekstensi txt)|*.txt";
            if (browse.ShowDialog()== DialogResult.OK)
            {
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                List<Control> removeLabel = this.Controls.OfType<Control>().ToList();
                foreach (Control c in removeLabel)
                {
                    if (c.Name == "newLabel")
                    {
                        this.Controls.Remove(c);
                        c.Dispose();
                    }

                }
                //clear label, radioButton, dan comboBox jika ada yang dipilih/ditampilkan sebelumnya
                label1.Text = "";
                label5.Text = "";
                label12.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                inputAda = true;
                filename = browse.SafeFileName;
                label11.Text = filename;
                inputTest = Input.inputToList(filename);
                List<List<string>> empty = new List<List<string>>();
                List<List<string>> firstInput = Input.inputToListTuple(filename, empty);

                nodeTest = Input.makeNodes(inputTest);
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                //isi comboBox1 (Choose Account) dan comboBox2 (Explore friends with) dengan isi nodeTest
                foreach (var node in nodeTest)
                {
                    comboBox1.Items.Add(node.getName());
                    comboBox2.Items.Add(node.getName());
                }
                
                //Tampilkan graf awal
                foreach (var arrayInput in firstInput)
                {
                    graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; 

                }
                gViewer1.Graph = graph;

            }
            
        }

        //DFS Button
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //cek apakah radioButton DFS dipilih
            if (radioButton1.Checked && inputAda==true)
            {
                //cek apakah combobox choose account dan explore friends with dipilih
                if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1)
                {
                    //output berupa jalur koneksi dalam bentuk visualisasi graf, jalur (path), dan derajat koneksinya. hasil didapat dengan algoritma DFS
                    string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                    string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                    hasil = DFS.Explore(nodeTest, selected1, selected2);

                    if (hasil.Count != 0)
                    {
                        List<string> hasilNonTuple = DFS.ExploreNotTuple(nodeTest, selected1, selected2);
                        string algoPath = "";
                        int countIsiHasilNonTuple = 0;
                        foreach (var isi in hasilNonTuple)
                        {
                            algoPath += isi;
                            countIsiHasilNonTuple += 1;
                            if (countIsiHasilNonTuple != hasilNonTuple.Count)
                            {
                                algoPath += " -> ";
                            }

                        }
                        label1.Text = algoPath;
                        int degree = hasilNonTuple.Count - 2;
                        if (degree == 0)
                        {
                            
                            label12.Text = degree.ToString() + "-degree connection";
                        } else
                        {
                            string numberOrd = Utility.numberOrder(degree);
                            label12.Text = degree.ToString() + numberOrd + "-degree connection";
                        }
                        
                        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                        tupleInput = Input.inputToListTuple(filename, hasil);

                        foreach (var arrayInput in tupleInput)
                        {
                            graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;

                        }
                        foreach (var array in hasil)
                        {
                            
                            Microsoft.Msagl.Drawing.Edge newEdge = graph.AddEdge(array[0], array[1]);
                            newEdge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                            newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                            graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;
                            graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;

                        }
                        gViewer1.Graph = graph;
                    }
                    else
                    {
                        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                        tupleInput = Input.inputToListTuple(filename, hasil);

                        foreach (var arrayInput in tupleInput)
                        {
                            graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                        }
                        gViewer1.Graph = graph;
                        List<Control> removeLabel2 = this.Controls.OfType<Control>().ToList();
                        foreach (Control c in removeLabel2)
                        {
                            if (c.Name == "newLabel")
                            {
                                this.Controls.Remove(c);
                                c.Dispose();
                            }

                        }
                        label1.Text = "";
                        label12.Text = "";
                        label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                    }
                }
                
                

            }

            
        }

        //BFS Button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //cek apakah radioButton BFS dipilih
            if (radioButton2.Checked && inputAda == true)
            {
                //cek apakah combobox choose account dan explore friends with dipilih
                if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1)
                {
                    //output berupa jalur koneksi dalam bentuk visualisasi graf, jalur (path), dan derajat koneksinya. hasil didapat dengan algoritma DFS
                    string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                    string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                    hasil = BFS.Explore(nodeTest, selected1, selected2);
                    if (hasil.Count != 0)
                    {
                        List<string> hasilNonTuple = BFS.ExploreNotTuple(nodeTest, selected1, selected2);
                        string algoPath = "";
                        int countIsiHasilNonTuple = 0;
                        foreach (var isi in hasilNonTuple)
                        {
                            algoPath += isi;
                            countIsiHasilNonTuple += 1;
                            if (countIsiHasilNonTuple != hasilNonTuple.Count)
                            {
                                algoPath += " -> ";
                            }

                        }
                        label1.Text = algoPath;
                        
                        int degree = hasilNonTuple.Count - 2;
                        if (degree == 0)
                        {

                            label12.Text = degree.ToString() + "-degree connection";
                        }
                        else
                        {
                            string numberOrd = Utility.numberOrder(degree);
                            label12.Text = degree.ToString() + numberOrd + "-degree connection";
                        }
                        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                        tupleInput = Input.inputToListTuple(filename, hasil);

                        foreach (var arrayInput in tupleInput)
                        {
                            graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                        }
                        foreach (var array in hasil)
                        {
                            Microsoft.Msagl.Drawing.Edge newEdge = graph.AddEdge(array[0], array[1]);
                            newEdge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                            newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                            graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;
                            graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;

                        }
                        gViewer1.Graph = graph;
                    }
                    else
                    {
                        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                        tupleInput = Input.inputToListTuple(filename, hasil);

                        foreach (var arrayInput in tupleInput)
                        {
                            graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                        }
                        gViewer1.Graph = graph;
                        List<Control> removeLabel2 = this.Controls.OfType<Control>().ToList();
                        foreach (Control c in removeLabel2)
                        {
                            if (c.Name == "newLabel")
                            {
                                this.Controls.Remove(c);
                                c.Dispose();
                            }

                        }
                        label1.Text = "";
                        label12.Text = "";
                        label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                    }
                }
                
            }
        }

        //Choose Account
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cek apakah comboBox "Choose Account" sudah diceklis
            if (comboBox1.SelectedIndex > -1)
            {
                //Posisi Y friend recommendation pertama
                int newLabelYPos = 95; 

                //Inisialisasi output friend recommendation
                label5.Text = "Friend Recommendations for " + comboBox1.GetItemText(comboBox1.SelectedItem) + " :";
                List<string> recommendNode = new List<string>();
                recommendNode = DFS.Recommend(nodeTest, comboBox1.GetItemText(comboBox1.SelectedItem));
                int recommendCount = recommendNode.Count;

                
                //Hapus label friend recommendation yang dahulu jika ada
                List<Control> removeLabel = this.Controls.OfType<Control>().ToList();
                foreach(Control c in removeLabel)
                {
                    if (c.Name == "newLabel")
                    {
                        this.Controls.Remove(c);
                        c.Dispose();
                    }
                    
                }
               
                 //Output friend recommendation untuk combobox Choose Account yang dipilih
                for (int i=0; i<recommendCount; i++)
                {
                    
                    newLabel = new Label();
                    newLabel.AutoSize = true;
                    newLabel.Location = new System.Drawing.Point(605,newLabelYPos); 
                    newLabel.Size = new System.Drawing.Size(31, 15);
                    newLabel.Name = "newLabel";
                    string mutual = " ";
                    int countMutual = 0;
                    List<string> mutualNode = new List<string>();
                    mutualNode = Utility.getMutualFriend(nodeTest, comboBox1.GetItemText(comboBox1.SelectedItem), recommendNode[i]);
                    foreach (var isi in mutualNode)
                    {
                        mutual += isi;
                        countMutual += 1;
                        if (countMutual != mutualNode.Count)
                        {
                            mutual += ", ";
                        }
                    }
                    
                    newLabel.Text = recommendNode[i] + "\n" + mutualNode.Count.ToString() + " Mutual Friends:" + mutual;
                    this.Controls.Add(newLabel);
                    
                    newLabelYPos += 40;
                }

                //Cek apakah comboBox "Explore friends with" sudah diceklis
                if (comboBox2.SelectedIndex > -1)
                {
                    //Jika memilih radioButton DFS, output berupa jalur koneksi dalam bentuk visualisasi graf, jalur (path), dan derajat koneksinya. hasil didapat dengan algoritma DFS
                    if (radioButton1.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = DFS.Explore(nodeTest, selected1, selected2);
                        

                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = DFS.ExploreNotTuple(nodeTest, selected1, selected2);
                            string algoPath = "";
                            int countIsiHasilNonTuple = 0;
                            foreach (var isi in hasilNonTuple)
                            {
                                algoPath += isi;
                                countIsiHasilNonTuple += 1;
                                if (countIsiHasilNonTuple != hasilNonTuple.Count)
                                {
                                    algoPath += " -> ";
                                }

                            }
                            label1.Text = algoPath;

                            int degree = hasilNonTuple.Count - 2;
                            if (degree == 0)
                            {

                                label12.Text = degree.ToString() + "-degree connection";
                            }
                            else
                            {
                                string numberOrd = Utility.numberOrder(degree);
                                label12.Text = degree.ToString() + numberOrd + "-degree connection";
                            }
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                            

                            tupleInput = Input.inputToListTuple(filename, hasil);
                            

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            
                            foreach (var array in hasil)
                            {
                                Microsoft.Msagl.Drawing.Edge newEdge = graph.AddEdge(array[0], array[1]);
                                newEdge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                                newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;

                            }
                            gViewer1.Graph = graph;
                        } 
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            gViewer1.Graph = graph;
                            List<Control> removeLabel2 = this.Controls.OfType<Control>().ToList();
                            foreach (Control c in removeLabel2)
                            {
                                if (c.Name == "newLabel")
                                {
                                    this.Controls.Remove(c);
                                    c.Dispose();
                                }

                            }
                            label1.Text = "";
                            label12.Text = "";
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                        
                    }

                    //Jika memilih radioButton BFS, output berupa jalur koneksi dalam bentuk visualisasi graf, jalur (path), dan derajat koneksinya. hasil didapat dengan algoritma BFS
                    if (radioButton2.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = BFS.Explore(nodeTest, selected1, selected2);
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = BFS.ExploreNotTuple(nodeTest, selected1, selected2);
                            string algoPath = "";
                            int countIsiHasilNonTuple = 0;
                            foreach (var isi in hasilNonTuple)
                            {
                                algoPath += isi;
                                countIsiHasilNonTuple += 1;
                                if (countIsiHasilNonTuple != hasilNonTuple.Count)
                                {
                                    algoPath += " -> ";
                                }

                            }
                            label1.Text = algoPath;

                            int degree = hasilNonTuple.Count - 2;
                            if (degree == 0)
                            {

                                label12.Text = degree.ToString() + "-degree connection";
                            }
                            else
                            {
                                string numberOrd = Utility.numberOrder(degree);
                                label12.Text = degree.ToString() + numberOrd + "-degree connection";
                            }
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                            

                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            foreach (var array in hasil)
                            {
                                Microsoft.Msagl.Drawing.Edge newEdge = graph.AddEdge(array[0], array[1]);
                                newEdge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                                newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;

                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            gViewer1.Graph = graph;
                            List<Control> removeLabel2 = this.Controls.OfType<Control>().ToList();
                            foreach (Control c in removeLabel2)
                            {
                                if (c.Name == "newLabel")
                                {
                                    this.Controls.Remove(c);
                                    c.Dispose();
                                }

                            }
                            label12.Text = "";
                            label1.Text = "";
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                    }
                    


                }
                
            }
          
        }

        //Explore friends with
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cek apakah comboBox "Explore friends with" sudah diceklis
            if (comboBox2.SelectedIndex > -1)
            {
                //Cek apakah comboBox "Choose Account" sudah diceklis
                if (comboBox1.SelectedIndex > -1)
                {
                    //Posisi Y friend recommendation pertama
                    int newLabelYPos = 95; 

                    //Inisialisasi output friend recommendation
                    label5.Text = "Friend Recommendations for " + comboBox1.GetItemText(comboBox1.SelectedItem) + " :";
                    List<string> recommendNode = new List<string>();
                    recommendNode = DFS.Recommend(nodeTest, comboBox1.GetItemText(comboBox1.SelectedItem));
                    int recommendCount = recommendNode.Count;


                    //Hapus label friend recommendation yang dahulu jika ada
                    List<Control> removeLabel = this.Controls.OfType<Control>().ToList();
                    foreach (Control c in removeLabel)
                    {
                        if (c.Name == "newLabel")
                        {
                            this.Controls.Remove(c);
                            c.Dispose();
                        }

                    }

                    //Output friend recommendation untuk combobox Choose Account yang dipilih
                    for (int i = 0; i < recommendCount; i++)
                    {

                        newLabel = new Label();
                        newLabel.AutoSize = true; 
                        newLabel.Location = new System.Drawing.Point(605, newLabelYPos);
                        newLabel.Size = new System.Drawing.Size(31, 15);
                        newLabel.Name = "newLabel";
                        string mutual = " ";
                        int countMutual = 0;
                        List<string> mutualNode = new List<string>();
                        mutualNode = Utility.getMutualFriend(nodeTest, comboBox1.GetItemText(comboBox1.SelectedItem), recommendNode[i]);
                        foreach (var isi in mutualNode)
                        {
                            mutual += isi;
                            countMutual += 1;
                            if (countMutual != mutualNode.Count)
                            {
                                mutual += ", ";
                            }
                        }

                        newLabel.Text = recommendNode[i] + "\n" + mutualNode.Count.ToString() + " Mutual Friends:" + mutual;
                        this.Controls.Add(newLabel);

                        newLabelYPos += 40;
                    }

                    //Jika memilih radioButton DFS, output berupa jalur koneksi dalam bentuk visualisasi graf, jalur (path), dan derajat koneksinya. hasil didapat dengan algoritma DFS
                    if (radioButton1.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = DFS.Explore(nodeTest, selected1, selected2);
                        
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = DFS.ExploreNotTuple(nodeTest, selected1, selected2);
                            string algoPath = "";
                            int countIsiHasilNonTuple = 0;
                            foreach (var isi in hasilNonTuple)
                            {
                                algoPath += isi;
                                countIsiHasilNonTuple += 1;
                                if (countIsiHasilNonTuple != hasilNonTuple.Count)
                                {
                                    algoPath += " -> ";
                                }

                            }
                            label1.Text = algoPath;

                            int degree = hasilNonTuple.Count - 2;
                            if (degree == 0)
                            {

                                label12.Text = degree.ToString() + "-degree connection";
                            }
                            else
                            {
                                string numberOrd = Utility.numberOrder(degree);
                                label12.Text = degree.ToString() + numberOrd + "-degree connection";
                            }
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);
                            

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                           
                            foreach (var array in hasil)
                            {
                                Microsoft.Msagl.Drawing.Edge newEdge = graph.AddEdge(array[0], array[1]);
                                newEdge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                                newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;

                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            gViewer1.Graph = graph;
                            List<Control> removeLabel2 = this.Controls.OfType<Control>().ToList();
                            foreach (Control c in removeLabel2)
                            {
                                if (c.Name == "newLabel")
                                {
                                    this.Controls.Remove(c);
                                    c.Dispose();
                                }

                            }
                            label1.Text = "";
                            label12.Text = "";
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                    }

                    //Jika memilih radioButton BFS, output berupa jalur koneksi dalam bentuk visualisasi graf, jalur (path), dan derajat koneksinya. hasil didapat dengan algoritma BFS
                    if (radioButton2.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = BFS.Explore(nodeTest, selected1, selected2);
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = BFS.ExploreNotTuple(nodeTest, selected1, selected2);
                            string algoPath = "";
                            int countIsiHasilNonTuple = 0;
                            foreach (var isi in hasilNonTuple)
                            {
                                algoPath += isi;
                                countIsiHasilNonTuple += 1;
                                if (countIsiHasilNonTuple != hasilNonTuple.Count)
                                {
                                    algoPath += " -> ";
                                }

                            }
                            label1.Text = algoPath;

                            int degree = hasilNonTuple.Count - 2;
                            if (degree == 0)
                            {

                                label12.Text = degree.ToString() + "-degree connection";
                            }
                            else
                            {
                                string numberOrd = Utility.numberOrder(degree);
                                label12.Text = degree.ToString() + numberOrd + "-degree connection";
                            }
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            foreach (var array in hasil)
                            {
                                Microsoft.Msagl.Drawing.Edge newEdge = graph.AddEdge(array[0], array[1]);
                                newEdge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                                newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Turquoise;

                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None; ;

                            }
                            gViewer1.Graph = graph;
                            List<Control> removeLabel2 = this.Controls.OfType<Control>().ToList();
                            foreach (Control c in removeLabel2)
                            {
                                if (c.Name == "newLabel")
                                {
                                    this.Controls.Remove(c);
                                    c.Dispose();
                                }

                            }
                            label1.Text = "";
                            label12.Text = "";
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                    }
                    


                }
                

            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        

        private void label11_Click(object sender, EventArgs e)
        {

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        

        private void gViewer1_Load(object sender, EventArgs e)
        {
           
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click_1(object sender, EventArgs e)
        {

        }
    }
}

