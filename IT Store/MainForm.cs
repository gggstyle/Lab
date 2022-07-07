using AForge.Video.DirectShow;
//using IT_Store.order;
using IT_Store;
using IT_Store.order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace ComShop
{
    public partial class MainForm : Form
    {
        dynamic result;
        apd64_62011212131Entities context = new apd64_62011212131Entities();
        FilterInfoCollection webcams;
        VideoCaptureDevice videoIn;
        public MainForm(dynamic result)
        {
            this.result = result;
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {

            label22.Text = result.name;
            if (result.type == 0)
            {
                MessageBox.Show("เจ้าของร้าน");

                empolyee.Text = "เจ้าของร้าน";

            }
            else if (result.type == 1)
            {
                MessageBox.Show("พนักงานขายหน้าร้าน");
                foreach (TabPage tab in tabControlManage.TabPages)
                {
                    tab.Enabled = false;
                }

                (tabControlManage.TabPages[1] as TabPage).Enabled = true;//ลูกค้า
                (tabControlManage.TabPages[2] as TabPage).Enabled = true;//สินค้า
                empolyee.Text = "พนักงานขายหน้าร้าน";
            }
            else
            {
                empolyee.Text = "พนักงานคลังสินค้า";
                //name.Text = "พนักงานคลังสินค้า";
                MessageBox.Show("พนักงานคลังสินค้า");
                foreach (TabPage tab in tabControlManage.TabPages)
                {
                    tab.Enabled = false;
                }


                (tabControlManage.TabPages[2] as TabPage).Enabled = true;//สินค้า

            }
            webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo webcam in webcams)
            {
                comboBox1.Items.Add(webcam.Name);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            //customersBindingSource.DataSource = context.customers.ToList();
            //employeeBindingSource.DataSource = context.employee.ToList();
            //productsBindingSource.DataSource = context.products.ToList();
            //webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //foreach (FilterInfo webcam in webcams)
            //{
            //    comboBox1.Items.Add(webcam.Name);
            //}
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e1)
        {
            employeeBindingSource.EndEdit();

            int change = context.SaveChanges();
            MessageBox.Show("Update  " + change + "record ");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Insert Data

            employeeBindingSource.AddNew();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            employee emp = new employee();
            emp.name = textBox2.Text;
            emp.username = textBox3.Text;
            emp.password = textBox5.Text;
            emp.type = int.Parse(textBox4.Text);

            context.employee.Add(emp);
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + "record ");

            employeeBindingSource.DataSource = context.employee.ToList();
        }

        private void button6_Click(object sender, EventArgs e1)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete row?", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                string id = textBox1.Text;
                int idInt = int.Parse(id);
                var toDelete = context.employee
                    .Where(e => e.eid == idInt)
                    .First();
                //remove คนเดียว
                context.employee.Remove(toDelete);
                int change = context.SaveChanges();
                MessageBox.Show("Delete " + change + "record ");
                employeeBindingSource.DataSource = context.employee.ToList();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Nothing to do
            }



        }

        private void button7_Click(object sender, EventArgs e1)
        {
            string name = textBox6.Text;
            string id = textBox6.Text;
            string uname = textBox6.Text;
            if (name != null)
            {
                var result = context.employee.Where(e => e.name.Contains(name) || e.eid.ToString().Contains(id) || e.username.Contains(uname));
                employeeBindingSource.DataSource = result.ToList();

                //}if (name != null) {

                //    int idInt = 0;
                //    if (id != null)
                //    {
                //        idInt = int.Parse(id);
                //    }
                //    else {
                //        MessageBox.Show("not found");
                //    }
                //    using (var context = new apd64_62011212131Entities6())
                //    {
                //        var result = from e in context.employee
                //                     where e.eid == idInt
                //                     select e;
                //    //dataGridView1.DataSource = result.ToList();
                //    employeeBindingSource.DataSource = result.ToList();
                //    }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

            customersBindingSource.AddNew();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            customers ctm = new customers();
            ctm.name = textBox10.Text;
            ctm.phone = textBox9.Text;
            ctm.status = "Member";
            context.customers.Add(ctm);

            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + "record ");
            customersBindingSource.DataSource = context.customers.ToList();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            customersBindingSource.EndEdit();
            int change = context.SaveChanges();
            MessageBox.Show("Update  " + change + "record ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete row?", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                string id = textBox11.Text;
                int idInt = int.Parse(id);
                var toDelete = context.customers
                    .Where(c => c.cid == idInt)
                    .First();
                //remove คนเดียว
                context.customers.Remove(toDelete);
                int change = context.SaveChanges();
                MessageBox.Show("Delete " + change + "record ");
                customersBindingSource.DataSource = context.customers.ToList();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Nothing to do
            }

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            string name = textBox7.Text;
            string id = textBox7.Text;
            string phone = textBox7.Text;
            var result = context.customers.Where(c => c.name.Contains(name) || c.cid.ToString().Contains(id) || c.phone.Contains(phone));
            customersBindingSource.DataSource = result.ToList();
            //string id = textBox7.Text;
            //int idInt = int.Parse(id);

            //using (var context = new apd64_62011212131Entities6())
            //{
            //    var result = from c in context.customers
            //                 where c.cid == idInt
            //                 select c;
            //    dataGridView2.DataSource = result.ToList();
            //    //customersBindingSource.DataSource = context.customers.ToList();

            // }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox11.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //textBox10.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //textBox9.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            customersBindingSource.DataSource = context.customers.ToList();

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);


            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string image = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
                try
                {
                    pictureBox1.Image = ConvertBase64ToImage(image);
                }
                catch (System.FormatException er)
                {
                    pictureBox1.LoadAsync(image);
                }
            }
        }
        public string ConvertImageToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            productsBindingSource.AddNew();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            products pd = new products();
            pd.name = textBox14.Text;
            pd.detail = textBox13.Text;
            pd.price = int.Parse(textBox12.Text);
            pd.amount = int.Parse(textBox8.Text);
            pd.type = textBox16.Text;
            if (pictureBox1 != null)
            {
                pd.image = ConvertImageToBase64(pictureBox1.Image);
                Console.WriteLine(pd.image.Length);
            }
            else
            {
                pd.image = null;
            }
            context.products.Add(pd);

            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + "record ");
            productsBindingSource.DataSource = context.products.ToList();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            productsBindingSource.EndEdit();
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + "record ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete row?", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                string id = textBox15.Text;
                int idInt = int.Parse(id);
                var toDelete = context.products
                    .Where(p => p.pid == idInt)
                    .First();
                //remove คนเดียว
                context.products.Remove(toDelete);
                int change = context.SaveChanges();
                MessageBox.Show("Delete " + change + "record ");
                productsBindingSource.DataSource = context.products.ToList();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Nothing to do
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string name = textBox17.Text;
            string id = textBox17.Text;
            string detail = textBox17.Text;
            string pid = textBox17.Text;
            string price = textBox17.Text;
            string type = textBox17.Text;
            var result = context.products.Where(p => p.name.Contains(name) || p.pid.ToString().Contains(id) || p.detail.Contains(detail)
            || p.price.ToString().Contains(price) || p.type.Contains(type) || p.product_id.Contains(pid));
            productsBindingSource.DataSource = result.ToList();
            //int idInt = int.Parse(id);
            //using (var context = new apd64_62011212131Entities6())
            //{
            //    var result = from p in context.products
            //                 where p.pid == idInt
            //                 select p;
            //    dataGridView3.DataSource = result.ToList();

            //}
        }

        private void tabControlManage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (tabControlManage.SelectedIndex)
            //{
            //    case 0:
            //        employeeBindingSource.DataSource = context.employee.ToList();
            //        break;
            //    case 1:
            //        customersBindingSource.DataSource = context.customers.ToList();
            //        break;
            //    case 2:
            //        productsBindingSource.DataSource = context.products.ToList();
            //        break ;
            //}
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Sign Out", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                this.Hide();
                Form1 form = new Form1();
                form.Show();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Nothing to do
            }

        }

        private void button19_Click(object sender, EventArgs e)
        {
            videoIn = new VideoCaptureDevice(webcams[comboBox1.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = videoIn;
            videoSourcePlayer1.Start();
            
            timer1.Start();


        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (videoIn != null && videoIn.IsRunning)
            {
                videoSourcePlayer1.Stop();
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var capture = videoSourcePlayer1.GetCurrentVideoFrame();
           
            if (capture != null)
            {
                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(capture);
                if (result != null)
                {
                    Console.WriteLine("ID : " + result);
                    string id = result.ToString();
                    var product = context.products
                    .Where(p => p.product_id == id)
                    .First();
                    if (product != null)
                    {
                        timer1.Stop();
                        OrderPopup orderPopup = new OrderPopup(product, listViewProduct);
                        orderPopup.ShowDialog();

                        if (orderPopup.status == 1)
                        {
                            timer1.Start();
                        }
                    }
                }
            }
        }

        private void listViewProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

            int sum = calculateTotal(listViewProduct.Items);
            MessageBox.Show(sum + "");
            orders orders = new orders();
            orderItems orderItems = new orderItems();
            string phone = textBoxPhone.Text;
            var result = (from c in context.customers
                          where c.phone == phone
                          select new { cid = c.cid, }).FirstOrDefault();
            int cid = result.cid;
            orders.cid = cid;
            // orders.cid = int.Parse(textBoxCid.Text);

            orders.total = sum;
            orders.datetime = DateTime.Now;
            context.orders.Add(orders);
            int chage = context.SaveChanges();
            MessageBox.Show("Chage : " + chage + "records");
            if (chage > 0)
            {
                foreach (ListViewItem item in listViewProduct.Items)
                {
                    string product_id = item.SubItems[0].Text;
                    orderItems.oid = orders.oid;
                    var product = context.products
                     .Where(p => p.product_id == product_id)
                     .First();
                    orderItems.pid = product.pid;

                    orderItems.amount = int.Parse(item.SubItems[2].Text);

                    context.orderItems.Add(orderItems);
                    context.SaveChanges();
                }
                MessageBox.Show("Save Completed");
            }

        }

        private int calculateTotal(ListView.ListViewItemCollection items)
        {
            int sum = 0;
            foreach (ListViewItem item in items)
            {
                sum += int.Parse(item.SubItems[3].Text);

            }
            return sum;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            employeeBindingSource.DataSource = context.employee.ToList();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            productsBindingSource.DataSource = context.products.ToList();
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                string phone = textBoxPhone.Text;
                var result = (from c in context.customers
                              where c.phone == phone
                              select new
                              {
                                  name = c.name,
                                  status = c.status,
                              }).FirstOrDefault();
                if (result != null)
                {
                    string name = result.name;
                    string status = result.status;
                    textBoxName.Text = name;
                    textBoxStatus.Text = status;

                }
                else
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง");
                }
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customersBindingSource.DataSource = context.customers.ToList();
            employeeBindingSource.DataSource = context.employee.ToList();
            productsBindingSource.DataSource = context.products.ToList();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void empolyee_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void employeeBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void customersBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void productsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e1)
        {
            if (e1.KeyChar == (char)13)
            {
                string name = textBox6.Text;
                string id = textBox6.Text;
                string uname = textBox6.Text;
                if (name != null)
                {
                    var result = context.employee.Where(e => e.name.Contains(name) || e.eid.ToString().Contains(id) || e.username.Contains(uname));
                    employeeBindingSource.DataSource = result.ToList();
                }
            }
        }

        private void tabControlManage_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e1)
        {
            if (e1.KeyChar == (char)13)
            {
                string name = textBox7.Text;
                string id = textBox7.Text;
                string phone = textBox7.Text;
                if (name != null)
                {
                    var result = context.customers.Where(c => c.name.Contains(name) || c.cid.ToString().Contains(id) || c.phone.Contains(phone));
                    customersBindingSource.DataSource = result.ToList();
                }

            }
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e1)
        {
            if (e1.KeyChar == (char)13)
            {
                string name = textBox17.Text;
                string id = textBox17.Text;
                string detail = textBox17.Text;
                string pid = textBox17.Text;
                string price = textBox17.Text;
                string type = textBox17.Text;
                var result = context.products.Where(p => p.name.Contains(name) || p.pid.ToString().Contains(id) || p.detail.Contains(detail)
                || p.price.ToString().Contains(price) || p.type.Contains(type) || p.product_id.Contains(pid));
                productsBindingSource.DataSource = result.ToList();
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("fair");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
