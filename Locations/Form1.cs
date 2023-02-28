using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OpenJob;
using System.Xml.Linq;

namespace Locations
{
    public partial class Form1 : Form
    {
        string db_path;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение всей строки в датагрид, а не ячейки
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            treeView1.HideSelection = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            db_path = Properties.Settings.Default.db_path;
            TreeBuild();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form set = new Settings
            {
                FormBorderStyle = FormBorderStyle.FixedSingle
            };
            set.ShowDialog();
        }
        private void открытьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Properties.Settings.Default.db_path;
            openFileDialog1.Filter = "Файлы DB (*.db)|*.db";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                db_path = openFileDialog1.FileName;
                TreeBuild();
            }
        }
        private void TreeBuild() //процедура построения дерева
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode
            {
                Text = Path.GetFileName(db_path),
                Tag = "",
                //ImageIndex = 0,
                //SelectedImageIndex = 0
            };
            treeView1.Nodes.Add(root);
            BindTreeViewControl();
            treeView1.Nodes[0].Expand(); // Разворачиваем узел
        }
        private void BindTreeViewControl() //Создание основных объектов
        {
            try
            {
                string sel = "";
                sel = "SELECT * FROM objects WHERE par_id IS NULL";
                var nodes = Db_class.Ds(sel, db_path).Tables[0].Rows;
                foreach (DataRow mains in nodes)
                {
                    TreeNode root = new TreeNode
                    {
                        Text = mains["s_name"].ToString(),
                        Tag = mains["id"].ToString(),
                        Name = mains["f_name"].ToString(),
                        //ImageIndex = 1,
                        //SelectedImageIndex = 1
                    };
                    root.Expand(); // Разворачиваем узел
                                   //root.SelectedImageIndex = 0;
                                   //MessageBox.Show(sel);
                    CreateNode(root);
                    treeView1.Nodes[0].Nodes.Add(root);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
            
        }
        public void CreateNode(TreeNode node)
        {
            try
            {
                string sel = "";
                sel = "SELECT * FROM objects WHERE par_id=" + node.Tag.ToString();
                var nodes = Db_class.Ds(sel, db_path).Tables[0].Rows;
                if (nodes.Count == 0) { return; }
                foreach (DataRow selNode in nodes)
                {
                    TreeNode ChildNode = new TreeNode
                    {
                        Text = selNode["s_name"].ToString(),
                        Tag = selNode["id"].ToString(),
                        Name = selNode["f_name"].ToString(),
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };
                    node.Nodes.Add(ChildNode);
                    node.Expand(); // Разворачиваем узел
                    CreateNode(ChildNode);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataGridBuild();
            dataGridView1.ClearSelection();
            textBox3.Text = treeView1.SelectedNode.Tag.ToString();
            button2.Text = "Добавить";
        }
        private void DataGridBuild() //процедура заполнения ДатаГрид
        {
            try
            {
                if (treeView1.SelectedNode.Text == Path.GetFileName(db_path))
                {
                    dataGridView1.DataSource = Db_class.Ds("SELECT * FROM objects WHERE par_id IS NULL", db_path);
                }
                else
                {
                    dataGridView1.DataSource = Db_class.Ds("SELECT * FROM objects WHERE par_id=" + treeView1.SelectedNode.Tag, db_path);
                }
                dataGridView1.DataMember = "table1";
                dataGridView1.Columns[0].Visible = false;
                //dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            { 
                textBox3.Text = Convert.ToString(dataGridView1[3, dataGridView1.SelectedRows[0].Index].Value);// Cells[3].Value);
                textBox1.Text = Convert.ToString(dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value);
                textBox2.Text = Convert.ToString(dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value);
                button2.Text = "Изменить";
            }
            else
            {
                try
                {
                    textBox3.Text = Convert.ToString(treeView1.SelectedNode.Tag);// Cells[3].Value);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    button2.Text = "Добавить";
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string q = "UPDATE objects SET f_name = '" + textBox1.Text + "', s_name = '" + textBox2.Text + "' WHERE id=" + Convert.ToString(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value);
                MessageBox.Show(Db_class.Upd(q, db_path));
            }
            else
            {
                if (treeView1.SelectedNode.Text == Path.GetFileName(db_path))
                {
                    string q = "PRAGMA foreign_keys = OFF; INSERT INTO objects (f_name, s_name) VALUES('" + textBox1.Text + "','" + textBox2.Text + "')";
                    MessageBox.Show(Db_class.Ins(q, db_path));
                }
                else
                {
                    string q = "INSERT INTO objects (f_name, s_name, par_id) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                    MessageBox.Show(Db_class.Ins(q, db_path));
                }
            }
            DataGridBuild();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            TreeBuild();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null) //если что-то выбрано
            {
                MessageBox.Show(Db_class.Del("objects", treeView1.SelectedNode.Tag.ToString(), db_path));
                TreeBuild();
            }
        }
    }
}
