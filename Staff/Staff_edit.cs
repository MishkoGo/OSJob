using OpenJob;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSJob
{
    public partial class Staff_edit : Form
    {
        string s_id;
        public Staff_edit(string S_ID)
        {
            InitializeComponent();
            s_id = S_ID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Staff_edit_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = false;
            DataTable dt = Db_class.Ds("SELECT staff.surname, staff.name, staff.patronymic, staff.funct_id, staff.adopted, companies.s_name, functions.s_name, functions.parent_dep, staff.s_name, staff.telega, staff.fired  FROM  staff JOIN functions ON staff.funct_id = functions.id JOIN companies ON functions.comp_id = companies.id WHERE staff.id = " + s_id).Tables[0];
            DataRow dr = dt.Rows[0];
            if (dr[7].ToString() == "0") //если должность не в подразделении
            {
                comboBox1.Items.Add(dr[5].ToString());
                comboBox1.SelectedIndex = 0;
            }
            else //если должность в подразделении
            {
                DataTable dtt = Db_class.Ds("SELECT s_name FROM departs WHERE id=" + dr[7].ToString()).Tables[0];
                DataRow drr = dtt.Rows[0];
                comboBox1.Items.Add(drr[0].ToString());
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.Items.Add(dr[6].ToString());
            comboBox2.SelectedIndex = 0;
            textBox1.Text = dr[0].ToString();
            textBox2.Text = dr[1].ToString();
            textBox3.Text = dr[2].ToString();
            textBox5.Text = dr[8].ToString();
            textBox4.Text = dr[9].ToString();
            if (dr.IsNull(10))
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
                dateTimePicker2.Value = DateTime.Parse(dr[10].ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker2.Enabled = true;
                dateTimePicker2.Value = DateTime.Now;
            }
            else
                dateTimePicker2.Enabled = false;
        }
    }
}
