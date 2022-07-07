using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab0301
{
    public partial class Form1 : Form
    {
        StudentProjectEntities context = new StudentProjectEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Linq Guery
            //var result = from s in context.Students select s;
            //dataGridView1.DataSource = result.ToList();

            //Linq Method
            var result = context.Students;
            dataGridView1.DataSource = result.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                //var result = from a in context.Students
                //           where a.student_id == textBox1.Text
                //         select a;
                var result = context.Students
                             .Where(a => a.student_id == textBox1.Text);
                        
                dataGridView1.DataSource=result.ToList();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var result = from s in context.Students
            //             where s.student_id == textBox1.Text
            //             && s.student_password == textBox2.Text
            //             select s;
            var result = context.Students
                .Where(s => s.student_id == textBox1.Text
                && s.student_password == textBox2.Text);
            dataGridView1.DataSource = result.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //var result = from s in context.Students
            //                 orderby s.student_fullname descending
            //                 select s;
            var result = context.Students
                .OrderByDescending(s => s.student_fullname);
            dataGridView1.DataSource = result.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //var result = from s in context.Students
            //             select new { ID = s.student_id, Fullname = s.student_fullname };
            var result = context.Students
                .Select(s => new { s.student_id, s.student_fullname })
                .OrderBy(s => s.student_id);
            dataGridView1.DataSource = result.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = from s in context.Students
                         select s;
            MessageBox.Show("Number of records : " + result.Count());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var result = from s in context.Students
                         where s.student_id == textBox4.Text
                         select s;
            
            result.ToList().ForEach(s => s.student_fullname = textBox8.Text);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

        }
    }
}
