using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IT_Store.Properties;
namespace IT_Store
{
    public partial class Form1 : Form
    {
        apd64_62011212131Entities context = new apd64_62011212131Entities();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e1)
        {

            // Ling Query           
            var result = (from e in context.employee
                          where e.username == textBoxUsername.Text
                          && e.password == textBoxPassword.Text

                          select new
                          {
                              name = e.name,
                              type = e.type
                          }).FirstOrDefault();
            string name = result.name;

            if (result != null)
            {
                this.Hide();
                // MainForm form = new MainForm(result);
                MainForm form = new MainForm(result);
                form.Show();
            }
            else
            {
                MessageBox.Show("กรุณาป้อนข้อมูลให้ถูกต้อง");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
