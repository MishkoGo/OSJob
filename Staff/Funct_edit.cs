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
    public partial class Funct_edit : Form
    {
        string dep_id;
        string p_s_n;
        string f_n;
        string s_n;
        public Funct_edit(string DEP_ID, string P_S_NAME, string F_NAME, string S_NAME)
        {
            InitializeComponent();
            dep_id = DEP_ID;
            p_s_n = P_S_NAME;
            f_n = F_NAME;
            s_n = S_NAME;
        }

        private void Funct_edit_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.Text = p_s_n;
            textBox2.Text = f_n;
            textBox3.Text = s_n;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Db_class.Upd("UPDATE functions SET f_name = '" + textBox2.Text + "', s_name = '" + textBox3.Text + "' WHERE id=" + Convert.ToString(dep_id));
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
