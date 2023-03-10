using OpenJob;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OSJob
{
    public partial class Struct : Form
    {
        private BindingSource _bsTypes;
        public Struct()
        {
            InitializeComponent();
            _bsTypes = new BindingSource();
            _bsTypes.DataSource = typeof(ComboItem);
            comboBox1.DataSource = _bsTypes;
            comboBox1.DisplayMember = nameof(ComboItem.Text);
            comboBox1.ValueMember = nameof(ComboItem.Id);
        }

        private void Struct_Load(object sender, EventArgs e)
        {
            ImageList ImgList = new ImageList();
            ImgList.Images.Add(Properties.Resources.comp);
            ImgList.Images.Add(Properties.Resources.dep);
            ImgList.Images.Add(Properties.Resources.shtat);
            ImgList.ImageSize = new Size(24, 24);
            treeView1.ImageList = ImgList;
            //treeView1.ImageList.ImageSize = new Size(20, 20);
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
            //label3.Text = Convert.ToString(comboBox1.SelectedValue) + (_bsTypes.Current as ComboItem).Text ;
        }
        private void Struct_Activated(object sender, EventArgs e)
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
            Main_funct();
            BindTreeViewControl();
            treeView1.Nodes[0].Expand(); // Разворачиваем узел
        }
        private void Main_funct() //Создание основных должностей предприятия
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string sel = "";
                int selectedId = Convert.ToInt32(comboBox1.SelectedValue);
                sel = "SELECT * FROM functions WHERE parent_dep=0 AND comp_id=" + Convert.ToString(selectedId);//treeView1.Nodes[0].Tag;
                var nodes = Db_class.Ds(sel).Tables[0].Rows;
                foreach (DataRow mains in nodes)
                {
                    TreeNode root = new TreeNode
                    {
                        Text = mains["s_name"].ToString(),
                        Tag = mains["id"].ToString(),
                        Name = mains["f_name"].ToString(),
                        ImageIndex = 2,
                        SelectedImageIndex = 2
                    };
                    root.Expand(); // Разворачиваем узел
                    CreateNode(root);
                    treeView1.Nodes[0].Nodes.Add(root);
                }
            }
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
            //построение должностей
            string sel = "";
            sel = "SELECT * FROM functions WHERE parent_dep=" + node.Tag.ToString() + " AND comp_id =" + Convert.ToString(comboBox1.SelectedValue);//treeView1.Nodes[0].Tag;
            var ns = Db_class.Ds(sel).Tables[0].Rows;
            if (ns.Count == 0) { return; }
            foreach (DataRow selNode in ns)
            {
                TreeNode ChildNode = new TreeNode
                {
                    Text = selNode["s_name"].ToString(),
                    Tag = selNode["id"].ToString(),
                    Name = selNode["f_name"].ToString(),
                    ImageIndex = 2,
                    SelectedImageIndex = 2
                };
                node.Nodes.Add(ChildNode);
            }
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

        private void btn_add_Click(object sender, EventArgs e) //кнопка Добавить
        {
            {
                string p = "";
                if (treeView1.SelectedNode != null) //если что-то выбрано
                {
                    if (treeView1.SelectedNode.Parent == null) //если родительский элемент предприятие
                    {
                        p = "1";
                    }
                    Form Depart_add = new Depart_add(
                        p,
                        Convert.ToString(comboBox1.SelectedValue),
                        treeView1.SelectedNode.Tag.ToString(),
                        treeView1.SelectedNode.Text)
                        {
                            FormBorderStyle = FormBorderStyle.FixedSingle
                        };
                    Depart_add.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не выбран родительский элемент.");
                }
            }
        }
        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null) //если что-то выбрано
            {
                if (treeView1.SelectedNode.Parent != null) //если выбранный элемент не предприятие
                {
                    if (treeView1.SelectedNode.ImageIndex == 1) //если выбранный элемент подразделение
                    {
                        Form Depart_edit = new Depart_edit(
                        treeView1.SelectedNode.Tag.ToString(),
                        comboBox1.Text,
                        treeView1.SelectedNode.Name.ToString(),
                        treeView1.SelectedNode.Text.ToString());
                        Depart_edit.ShowDialog();
                    }
                    else //если выбранный элемент должность
                    {
                        Form Funct_edit = new Funct_edit(
                        treeView1.SelectedNode.Tag.ToString(),
                        comboBox1.Text,
                        treeView1.SelectedNode.Name.ToString(),
                        treeView1.SelectedNode.Text.ToString());
                        Funct_edit.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя редактировать предприятие в данной форме.");
                }
            }
            else
            {
                MessageBox.Show("Не выбран элемент для редактирования.");
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null) //если что-то выбрано
            {
                if (treeView1.SelectedNode.Parent != null) //если выбранный элемент не предприятие
                {
                    string id = "";
                    id = treeView1.SelectedNode.Tag.ToString();
                    DialogDel dd = new DialogDel();
                    dd.ShowDialog();
                    if (DialogResult.OK == dd.DialogResult)
                    {
                        if (treeView1.SelectedNode.ImageIndex == 1) //если выбранный элемент подразделение
                        {
                            {
                                MessageBox.Show(Db_class.Del("departs", id));
                            }
                        }
                        if (treeView1.SelectedNode.ImageIndex == 2) //если выбранный элемент должность
                        {
                            {
                                MessageBox.Show(Db_class.Del("functions", id));
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить предприятие в данной форме.");
                }
                //TreeBuild();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            {
                string p = "";
                if (treeView1.SelectedNode != null) //если что-то выбрано
                {
                    if (treeView1.SelectedNode.Parent == null) //если родительский элемент предприятие
                    {
                        p = "1";
                    }
                    Form Funct_add = new Funct_add(
                        p,
                        Convert.ToString(comboBox1.SelectedValue),
                        treeView1.SelectedNode.Tag.ToString(),
                        treeView1.SelectedNode.Text)
                    {
                        FormBorderStyle = FormBorderStyle.FixedSingle
                    };
                    Funct_add.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не выбран родительский элемент.");
                }
            }
        }
    }
}
