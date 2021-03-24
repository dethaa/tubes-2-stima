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
                inputAda = true;
                filename = browse.SafeFileName;
                label11.Text = filename;
                inputTest = Input.inputToList(filename);
               
                nodeTest = Input.makeNodes(inputTest);
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                foreach (var node in nodeTest)
                {
                    comboBox1.Items.Add(node.getName());
                    comboBox2.Items.Add(node.getName());
                }

            }
            
        }

        //DFS Button
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked && inputAda==true)
            {
                if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1)
                {
                    string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                    string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                    hasil = DFS.Explore(nodeTest, selected1, selected2);

                    if (hasil.Count != 0)
                    {
                        List<string> hasilNonTuple = DFS.ExploreNotTuple(nodeTest, selected1, selected2);
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
                            graph.AddEdge(arrayInput[0], arrayInput[1]);

                        }
                        foreach (var array in hasil)
                        {
                            //graph.AddEdge(array[0], array[1]);
                            graph.AddEdge(array[0], array[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                            graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                            graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;

                        }
                        gViewer1.Graph = graph;
                    }
                    else
                    {
                        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                        tupleInput = Input.inputToListTuple(filename, hasil);

                        foreach (var arrayInput in tupleInput)
                        {
                            graph.AddEdge(arrayInput[0], arrayInput[1]);

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
                        label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                    }
                }
                
                

            }

            
        }

        //BFS Button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked && inputAda == true)
            {
                
                if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1)
                {

                    string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                    string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                    hasil = BFS.Explore(nodeTest, selected1, selected2);
                    if (hasil.Count != 0)
                    {
                        List<string> hasilNonTuple = BFS.ExploreNotTuple(nodeTest, selected1, selected2);
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
                            graph.AddEdge(arrayInput[0], arrayInput[1]);

                        }
                        foreach (var array in hasil)
                        {
                            //graph.AddEdge(array[0], array[1]);
                            graph.AddEdge(array[0], array[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                            graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                            graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;

                        }
                        gViewer1.Graph = graph;
                    }
                    else
                    {
                        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                        tupleInput = Input.inputToListTuple(filename, hasil);

                        foreach (var arrayInput in tupleInput)
                        {
                            graph.AddEdge(arrayInput[0], arrayInput[1]);

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
                        label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                    }
                }
                
            }
        }

        //Choose Account
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                
                int newLabelYPos = 499;
              
                label5.Text = "Friend Recommendations for " + comboBox1.GetItemText(comboBox1.SelectedItem) + " :";
                List<string> recommendNode = new List<string>();
                recommendNode = DFS.Recommend(nodeTest, comboBox1.GetItemText(comboBox1.SelectedItem));

                int recommendCount = recommendNode.Count;

                List<Control> removeLabel = this.Controls.OfType<Control>().ToList();
                foreach(Control c in removeLabel)
                {
                    if (c.Name == "newLabel")
                    {
                        this.Controls.Remove(c);
                        c.Dispose();
                    }
                    
                }
               
                for (int i=0; i<recommendCount; i++)
                {
                    
                    newLabel = new Label();
                    newLabel.AutoSize = true;
                    newLabel.Location = new System.Drawing.Point(66,newLabelYPos);
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

                if (comboBox2.SelectedIndex > -1)
                {

                    if (radioButton1.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = DFS.Explore(nodeTest, selected1, selected2);
                        
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = DFS.ExploreNotTuple(nodeTest, selected1, selected2);
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
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

                            }
                            foreach (var array in hasil)
                            {
                                //graph.AddEdge(array[0], array[1]);
                                graph.AddEdge(array[0], array[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;

                            }
                            gViewer1.Graph = graph;
                        } 
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

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
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                        
                    }

                    if (radioButton2.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = BFS.Explore(nodeTest, selected1, selected2);
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = BFS.ExploreNotTuple(nodeTest, selected1, selected2);
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
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

                            }
                            foreach (var array in hasil)
                            {
                                //graph.AddEdge(array[0], array[1]);
                                graph.AddEdge(array[0], array[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;

                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

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
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                    }
                    


                }
                
            }
          
        }

        //Explore friends with
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
            {
              
                if (comboBox1.SelectedIndex > -1)
                {
                    
                    int newLabelYPos = 499;

                    label5.Text = "Friend Recommendations for " + comboBox1.GetItemText(comboBox1.SelectedItem) + " :";
                    List<string> recommendNode = new List<string>();
                    recommendNode = DFS.Recommend(nodeTest, comboBox1.GetItemText(comboBox1.SelectedItem));
                    int recommendCount = recommendNode.Count;

                    List<Control> removeLabel = this.Controls.OfType<Control>().ToList();
                    foreach (Control c in removeLabel)
                    {
                        if (c.Name == "newLabel")
                        {
                            this.Controls.Remove(c);
                            c.Dispose();
                        }

                    }
                    for (int i = 0; i < recommendCount; i++)
                    {

                        newLabel = new Label();
                        newLabel.AutoSize = true;
                        newLabel.Location = new System.Drawing.Point(66, newLabelYPos);
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
                    if (radioButton1.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = DFS.Explore(nodeTest, selected1, selected2);
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = DFS.ExploreNotTuple(nodeTest, selected1, selected2);
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
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

                            }
                            foreach (var array in hasil)
                            {
                                //graph.AddEdge(array[0], array[1]);
                                graph.AddEdge(array[0], array[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;

                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

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
                            label5.Text = "Tidak ada jalur koneksi yang tersedia \n Anda harus memulai koneksi baru itu sendiri";
                        }
                    }

                    if (radioButton2.Checked)
                    {
                        string selected1 = comboBox1.GetItemText(comboBox1.SelectedItem);
                        string selected2 = comboBox2.GetItemText(comboBox2.SelectedItem);
                        hasil = BFS.Explore(nodeTest, selected1, selected2);
                        if (hasil.Count != 0)
                        {
                            List<string> hasilNonTuple = BFS.ExploreNotTuple(nodeTest, selected1, selected2);
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
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

                            }
                            foreach (var array in hasil)
                            {
                                //graph.AddEdge(array[0], array[1]);
                                graph.AddEdge(array[0], array[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                                graph.FindNode(array[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                                graph.FindNode(array[1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;

                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");


                            tupleInput = Input.inputToListTuple(filename, hasil);

                            foreach (var arrayInput in tupleInput)
                            {
                                graph.AddEdge(arrayInput[0], arrayInput[1]);

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
    }
}

