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
    public partial class Staff_add : Form
    {
        private BindingSource _bsTypes;
        string c_s_n;
        string d_id;
        string d_s_n;
        public Staff_add(string D_ID, string D_S_NAME, string C_S_NAME)
        {
            InitializeComponent();
            d_id = D_ID;
            d_s_n = D_S_NAME;
            c_s_n = C_S_NAME;
            _bsTypes = new BindingSource();
            _bsTypes.DataSource = typeof(ComboItem);
            comboBox2.DataSource = _bsTypes;
            comboBox2.DisplayMember = nameof(ComboItem.Text);
            comboBox2.ValueMember = nameof(ComboItem.Id);
        }

        private void Staff_add_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(d_s_n);
            comboBox1.SelectedItem = d_s_n;
            dateTimePicker1.Value = DateTime.Now;
            if (d_id != "0")
            {
                foreach (ComboItem ci in Db_class.Bs("SELECT functions.id, functions.s_name FROM functions JOIN departs ON functions.parent_dep = departs.id WHERE departs.id = " + Convert.ToString(d_id)))
                {
                    _bsTypes.Add(ci);
                }
                if (_bsTypes.Count == 0)
                {
                    MessageBox.Show("Отсутствуют должности в выбранном подразделении.");
                    this.Close();
                }
            }
            else
            {
                foreach (ComboItem ci in Db_class.Bs("SELECT functions.id, functions.s_name FROM functions JOIN companies ON functions.comp_id = companies.id WHERE functions.parent_dep = 0 AND companies.s_name = '" + Convert.ToString(c_s_n) + "'"))
                {
                    _bsTypes.Add(ci);
                }
                if (_bsTypes.Count == 0)
                {
                    MessageBox.Show("Отсутствуют должности в выбранном подразделении.");
                    this.Close();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox1.Text + " " + textBox2.Text.Substring(0, 1) + "." + textBox3.Text.Substring(0, 1) + ".";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string q = "INSERT INTO staff(surname, name, patronymic, s_name, funct_id, adopted, telega) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + comboBox2.SelectedValue + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + textBox4.Text + "')";
                MessageBox.Show(Db_class.Ins(q));
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
        }
    }
}
