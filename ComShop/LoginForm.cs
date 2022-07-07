using ComShop.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComShop
{
    public partial class LoginForm : Form
    {
        
        apd64_62011212131Entities8 context = new apd64_62011212131Entities8();
        public LoginForm()
        
        {         
            InitializeComponent();
        }
        private void btnOK_Click(object sender, EventArgs e1)
        {
            // Ling Query
            //if (textBoxUsername.Text == null || textBoxPassword.Text == null)
            //{
            //    MessageBox.Show("กรุณาป้อนข้อมูล");
            //}
            
                var result = (from e in context.employee
                              where e.username == textBoxUsername.Text
                              && e.password == textBoxPassword.Text

                              select new
                              {
                                  name = e.name,
                                  type = e.type
                              }).FirstOrDefault();
                

                if (result != null)
                {
                    string name = result.name;
                    this.Hide();
                    MainForm form = new MainForm(result);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("กรุณาป้อนข้อมูลให้ถูกต้อง");
                }

            
            

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_KeyPress(object sender, KeyPressEventArgs e1)
        {
            
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e1)
        {
            if (e1.KeyChar == (char)13)
            {
                if (textBoxUsername.Text == null || textBoxPassword.Text == null)
                {
                    MessageBox.Show("กรุณาป้อนข้อมูล");
                }
                else
                {
                    var result = (from e in context.employee
                                  where e.username == textBoxUsername.Text
                                  && e.password == textBoxPassword.Text

                                  select new
                                  {
                                      name = e.name,
                                      type = e.type
                                  }).FirstOrDefault();


                    if (result != null)
                    {
                        string name = result.name;
                        this.Hide();
                        MainForm form = new MainForm(result);
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("กรุณาป้อนข้อมูลให้ถูกต้อง");
                    }

                }

            }
        }
    }
}
