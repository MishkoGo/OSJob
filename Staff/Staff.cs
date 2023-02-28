using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenJob;

namespace OSJob
{
    public partial class Staff : Form
    {
        private BindingSource _bsTypes;
        string a;
        public Staff()
        {
            InitializeComponent();
            _bsTypes = new BindingSource();
            _bsTypes.DataSource = typeof(ComboItem);
            comboBox1.DataSource = _bsTypes;
            comboBox1.DisplayMember = nameof(ComboItem.Text);
            comboBox1.ValueMember = nameof(ComboItem.Id);
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            ImageList ImgList = new ImageList();
            ImgList.Images.Add(Properties.Resources.comp);
            ImgList.Images.Add(Properties.Resources.dep);
            ImgList.ImageSize = new Size(24, 24);
            treeView1.ImageList = ImgList;
            treeView1.HideSelection = false;
            foreach (ComboItem ci in Db_class.Bs("SELECT id, s_name FROM companies"))
            {
                _bsTypes.Add(ci);
            }
            if (_bsTypes.Count == 0)
            {
                MessageBox.Show("Отсутствуют организации");
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                TreeBuild();
            }
        }

        private void Staff_Activated(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                TreeBuild();
            }
        }
        private void TreeBuild() //процедура построения дерева
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode
            {
                Text = comboBox1.Text,
                Tag = Convert.ToString(comboBox1.SelectedValue),
                ImageIndex = 0,
                SelectedImageIndex = 0
            };
            treeView1.Nodes.Add(root);
            BindTreeViewControl();
            treeView1.Nodes[0].Expand(); // Разворачиваем узел
        }
        private void BindTreeViewControl() //Создание основных отделов предприятия
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string sel = "";
                int selectedId = Convert.ToInt32(comboBox1.SelectedValue);
                sel = "SELECT * FROM departs WHERE parent_dep=0 AND comp_id=" + Convert.ToString(selectedId);//treeView1.Nodes[0].Tag;
                var nodes = Db_class.Ds(sel).Tables[0].Rows;
                foreach (DataRow mains in nodes)
                {
                    TreeNode root = new TreeNode
                    {
                        Text = mains["s_name"].ToString(),
                        Tag = mains["id"].ToString(),
                        Name = mains["f_name"].ToString(),
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };
                    root.Expand(); // Разворачиваем узел
                    CreateNode(root);
                    treeView1.Nodes[0].Nodes.Add(root);
                }
            }
        }
        public void CreateNode(TreeNode node)
        {
            string sel = "";
            //построение отделов
            sel = "SELECT * FROM departs WHERE parent_dep=" + node.Tag.ToString() + " AND comp_id =" + Convert.ToString(comboBox1.SelectedValue);//treeView1.Nodes[0].Tag;
            var nodes = Db_class.Ds(sel).Tables[0].Rows;
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text == comboBox1.Text)
            {
                a = "0";
            }
            else
            {
                a = treeView1.SelectedNode.Tag.ToString();
            }
            DisplayData();
        }
        private void DisplayData() //процедура обновления данных
        {
            label3.Text = treeView1.SelectedNode.Text;
            dataGridView1.DataSource = Db_class.Ds("SELECT staff.s_name, functions.s_name, staff.id FROM staff JOIN functions ON staff.funct_id=functions.id JOIN companies ON functions.comp_id = companies.id WHERE companies.s_name = '" + comboBox1.Text + "' AND functions.parent_dep = " + a);
            dataGridView1.DataMember = "table1";
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView1.Columns[2].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null) //если что-то выбрано
            {
                Form Staff_add = new Staff_add(
                            a,
                            treeView1.SelectedNode.Text,
                            comboBox1.Text)
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle
                };
                Staff_add.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрано подразделение для добавления сотрудника");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int i = dataGridView1.CurrentCell.RowIndex;
                Form Staff_edit = new Staff_edit(
                    dataGridView1.Rows[i].Cells[2].Value.ToString());
                //{
                //    FormBorderStyle = FormBorderStyle.FixedSingle
                //};
                Staff_edit.Show(); ;
            }
            else
            {
                MessageBox.Show("Выберите запиь для редактирования.");
            }
        }
    }
}
