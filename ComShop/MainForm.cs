using AForge.Video.DirectShow;
using ComShop.order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
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
        apd64_62011212131Entities8 context = new apd64_62011212131Entities8();
        FilterInfoCollection webcams;
        VideoCaptureDevice videoIn;
        public MainForm(dynamic result)
        {
            this.result = result;
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            customersBindingSource.DataSource = context.customers.ToList();
            employeeBindingSource.DataSource = context.employee.ToList();
            productsBindingSource.DataSource = context.products.ToList();
            boxsetsBindingSource.DataSource = context.boxsets.ToList();

            // dataGridView5.DataSource = context.products.ToList();
            //var results = context.products.Distinct()
            //    .Select(p => new { p.product_id, p.type });
            //foreach(var result in results)
            //{
            //    comboBox2.Items.Add(new ComboBoxItem(result.product_id,result.type));
            //}
            var result3 = (from p in context.products
                           join b in context.boxsetItems on
                           p.pid equals b.pid
                           join b1 in context.boxsets on
                           b.bid equals b1.bid

                           select new
                           {
                               รหัสชุด = b1.bid,
                               ชื่อสินค้า = p.name,
                               ราคาโปรโมชั้น = b1.price,
                               ชุด = b1.detail,
                           });
            dataGridView8.DataSource = result3.ToList();
           
            label22.Text = result.name;
            if (result.type == "เจ้าของร้าน")
            {
                MessageBox.Show("เจ้าของร้าน");

                empolyee.Text = "เจ้าของร้าน";

                foreach (TabPage tab in tabControl1.TabPages)
                {
                    tab.Enabled = false;
                }

                (tabControl1.TabPages[0] as TabPage).Enabled = true;//ล
                (tabControl1.TabPages[1] as TabPage).Enabled = true;//ล
                (tabControl1.TabPages[2] as TabPage).Enabled = true;//ล
                (tabControl1.TabPages[4] as TabPage).Enabled = true;//ล
                (tabControl1.TabPages[5] as TabPage).Enabled = true;
                //(tabControl2.TabPages[2] as TabPage).Enabled = true;//ลูกค้า
                tabControl1.TabPages.Remove(tabPage3);
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button3.Enabled = false;

            }
            else if (result.type == "พนักงานขายหน้าร้าน")
            {
                MessageBox.Show("พนักงานขายหน้าร้าน");
                foreach (TabPage tab in tabControlManage.TabPages)
                {
                    tab.Enabled = false;
                }

                (tabControlManage.TabPages[1] as TabPage).Enabled = true;//ลูกค้า
                (tabControlManage.TabPages[2] as TabPage).Enabled = true;//สินค้า
                empolyee.Text = "พนักงานขายหน้าร้าน";
                tabControlManage.TabPages.Remove(tabPageEmployee);
                tabControl2.TabPages.Remove(tabPage9);
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                empolyee.Text = "พนักงานคลังสินค้า";
                //name.Text = "พนักงานคลังสินค้า";
                MessageBox.Show("พนักงานคลังสินค้า");
               // foreach (TabPage tab in tabControlManage.TabPages)
                //{
                  //  tab.Enabled = false;
                //}


                //(tabControlManage.TabPages[0] as TabPage).Enabled = true;//สินค้า
                //(tabControlManage.TabPages[1] as TabPage).Enabled = true;//สินค้า
               // (tabControlManage.TabPages[2] as TabPage).Enabled = true;//สิน
                tabControlManage.TabPages.Remove(tabPageEmployee);
                tabControlManage.TabPages.Remove(tabPage4);
                tabControl2.TabPages.Remove(tabPage9);

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
            emp.type = comboBox3.Text;

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
            pd.image = textBox20.Text;
            //if (pictureBox1 != null)
            //{
              //  pd.image = ConvertImageToBase64(pictureBox1.Image);
                // Console.WriteLine(pd.image.Length);
            //}
            //else
            ///{
             //   pd.image = null;
           // }
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
                LoginForm form = new LoginForm();
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
                    //Console.WriteLine("ID : " + result);
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
                    else
                    {
                        MessageBox.Show("ไม่พบสินค้า");
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
            //MessageBox.Show(sum + "");
            orders orders = new orders();
            orderItems orderItems = new orderItems();
            products products = new products();
            string phone = textBoxPhone.Text;
            var result = (from c in context.customers
                          where c.phone == phone
                          select new { cid = c.cid, }).FirstOrDefault();
            
            // int chage = context.SaveChanges();
            // MessageBox.Show("Chage : " + chage + "records");
            if (result != null)
            {
                int cid = result.cid;
                orders.cid = cid;
                // orders.cid = int.Parse(textBoxCid.Text);

                orders.total = int.Parse(label40.Text);
                orders.datetime = DateTime.Now;
                context.orders.Add(orders);
                foreach (ListViewItem item in listViewProduct.Items)
                {
                    string product_id = item.SubItems[0].Text;
                    orderItems.oid = orders.oid;
                    var product = context.products
                     .Where(p => p.product_id == product_id)
                     .First();


                    orderItems.pid = product.pid;

                    orderItems.amount = int.Parse(item.SubItems[2].Text);

                    string deleteAmount = item.SubItems[2].Text;

                    int pId = (int)orderItems.pid;
                    var pro = context.products
                        .Where(p => p.pid == pId).First();


                    if (pro.amount >= int.Parse(deleteAmount))
                    {
                        pro.amount = pro.amount.Value - int.Parse(deleteAmount);
                        context.orderItems.Add(orderItems);
                        context.SaveChanges();
                        
                        listViewProduct.Items.Remove(item);
                        textBoxPhone.Text = "";
                        textBoxName.Text = "";
                        textBoxStatus.Text = "";
                        label40.Text = "";


                    }
                    else
                    {
                        MessageBox.Show(pro.name + " ไม่มีสินค้า");

                    }
                    

                }
                MessageBox.Show("Save Completed");


                //productsBindingSource.EndEdit();
                //int change = context.SaveChanges();
                //MessageBox.Show("Change " + change + "record ");
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
            //label40.Text = 
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
                    int total = TotalPrice(listViewProduct.Items);
                    int pomotion1 = TotalPomotion(listViewProduct.Items);
                    //label40.Text = total.ToString();
                    label40.Text = total.ToString();
                    label41.Text = pomotion1.ToString();
                    if (int.Parse(label40.Text) > 0)
                    {
                        button35.Enabled = true;
                    }
                    else {
                        button35.Enabled = false;
                    }
                    
                    //--
                    
                }
                else
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง");
                }
            
            //int boxsetId = result.FirstOrDefault().bid;
            //var result2 = from b in context.boxsetItems
            //              join p in context.products on
            //              b.pid equals p.pidvar result2 = from b in context.boxsetItems
            //              join p in context.products on
            //              b.pid equals p.pid
            //              join b1 in context.boxsets on
            //              b.bid equals b1.bid
            //              where b1.detail != null
            //              select new
            //              {
            //                  รหัสสินค้า = p.product_id,
            //                  รหัส = b.pid,
            //                  ชื่อ = p.name,
            //                  ราคาโปร = b1.price,
            //                  ราคาจริง = p.price

            //              };
            //string name1 = result2.FirstOrDefault().ชื่อ;
            //string proID = result2.FirstOrDefault().รหัสสินค้า;
            //int one = 1;
            
            //    foreach (ListViewItem item in listViewProduct.Items)
            //    {
            //        item.SubItems[0].Text.Equals(proID);
            //    if (item.SubItems[0].Text.Equals(proID).ToString() != null && int.Parse(proID > 2) {
            //        button35.Enabled = true;
            //------------------------

                }

        }

        private int TotalPomotion(ListView.ListViewItemCollection items)
        {
            int pomotion1 = 0;
            foreach (ListViewItem item in items)
            {
                pomotion1 += int.Parse(item.SubItems[6].Text);
                

            }
            return pomotion1;
        }

        private int TotalPrice(ListView.ListViewItemCollection items)
        {
            int total = 0;
            foreach (ListViewItem item in items)
            {
                total += int.Parse(item.SubItems[4].Text);

            }
            return total;
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //customersBindingSource.DataSource = context.customers.ToList();
            //employeeBindingSource.DataSource = context.employee.ToList();
            //productsBindingSource.DataSource = context.products.ToList();
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
            //Console.WriteLine("fair");

        }

        private void button24_Click(object sender, EventArgs e)
        {

            string type = comboBox2.Text;
            if (type == null)
            {
                MessageBox.Show("กรุณากรอกข้อมูลเพื่อค้นหา");
            }
            else {
                var result = context.products.Where(p => p.type.Contains(type));
                productsBindingSource.DataSource = result.ToList();
            }

           

        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            //int cid = result.cid;
            var result3 = (from c in context.orders
                           join c1 in context.customers on
                           c.cid equals c1.cid
                           //where c.cid == cid
                           orderby c.oid descending
                           select new
                           {
                               รหัสออเดอร์ = c.oid,
                               ชื่อลูกค้า = c1.name,
                               ราคารวมทั้งหมด = c.total,
                               วันเวลาที่ซื้อ = c.datetime,
                               type = c1.status
                           });
            dataGridView6.DataSource = result3.ToList();

        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dataGridView6.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                int id = int.Parse(dataGridView6.Rows[e.RowIndex].Cells["รหัสออเดอร์"].FormattedValue.ToString());
                var result = (from m in context.orderItems
                              join m1 in context.products on
                              m.pid equals m1.pid
                              join m2 in context.orders on
                              m.oid equals m2.oid
                              where m2.oid == id
                              select new
                              {
                                  ชื่อสินค้า = m1.name,
                                  ราคา = m1.price,
                                  จำนวน = m.amount,
                                  //รวม = m2.total

                              });

                dataGridView7.DataSource = result.ToList();
            }

        }

        private void button26_Click(object sender, EventArgs e)
        {
            string search = textBox18.Text.Trim();

            var result3 = (from c in context.orders
                           join c1 in context.customers on
                           c.cid equals c1.cid
                           where c1.name.Contains(search) || c1.phone.Contains(search)
                           orderby c.oid descending
                           select new
                           {
                               รหัสออเดอร์ = c.oid,
                               ชื่อลูกค้า = c1.name,
                               ราคารวมทั้งหมด = c.total,
                               วันเวลาที่ซื้อ = c.datetime,
                           });
            dataGridView6.DataSource = result3.ToList();

            if (result3 == null)


            {
                MessageBox.Show("กรุณากรอกชื่อและเบอร์โทรศัพท์ใหม่อีกครั้ง");
            }
        }


        private void button27_Click(object sender, EventArgs e)
        {
            var result3 = (from c in context.orders
                           join c1 in context.customers on
                           c.cid equals c1.cid
                           //where c.cid == cid
                           orderby c.oid descending
                           select new
                           {
                               รหัสออเดอร์ = c.oid,
                               ชื่อลูกค้า = c1.name,
                               ราคารวมทั้งหมด = c.total,
                               วันเวลาที่ซื้อ = c.datetime,
                           });
            dataGridView6.DataSource = result3.ToList();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewProduct.Items)
                if (item.Selected)
                {
                    listViewProduct.Items.Remove(item);
                    //button35.Enabled = false;
                }
                else
                {
                    MessageBox.Show("กรุณาเลือกรายการที่ต้องการนำออก");
                }

        }

        private void button29_Click(object sender, EventArgs e)
        {
            var result3 = (from c in context.orders
                           join c1 in context.customers on
                           c.cid equals c1.cid
                           //where c.cid == cid
                           orderby c.oid descending
                           select new
                           {
                               รหัสออเดอร์ = c.oid,
                               ชื่อลูกค้า = c1.name,
                               ราคารวมทั้งหมด = c.total,
                               วันเวลาที่ซื้อ = c.datetime,
                               type = c1.status
                           });
            dataGridView6.DataSource = result3.ToList();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string name = textBox19.Text;
            //string id = textBox19.Text;
            string amount = textBox19.Text;
            string pid = textBox19.Text;
            string price = textBox19.Text;
            string type = textBox19.Text;
            var result = context.products.Where(p => p.name.Contains(name) || p.pid.ToString().Contains(amount)
            || p.price.ToString().Equals(price) || p.type.Contains(type) || p.product_id.Contains(pid));
            productsBindingSource.DataSource = result.ToList();
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string name = textBox19.Text;
                //string id = textBox19.Text;
                string amount = textBox19.Text;
                string pid = textBox19.Text;
                string price = textBox19.Text;
                string type = textBox19.Text;
                var result = context.products.Where(p => p.name.Contains(name) || p.pid.ToString().Contains(amount)
                || p.price.ToString().Equals(price) || p.type.Contains(type) || p.product_id.Contains(pid));
                productsBindingSource.DataSource = result.ToList();

            }
        }

        private void button26_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                string search = textBox18.Text.Trim();

                var result3 = (from c in context.orders
                               join c1 in context.customers on
                               c.cid equals c1.cid
                               where c1.name.Contains(search) || c1.phone.Contains(search)
                               orderby c.oid descending
                               select new
                               {
                                   รหัสออเดอร์ = c.oid,
                                   ชื่อลูกค้า = c1.name,
                                   ราคารวมทั้งหมด = c.total,
                                   วันเวลาที่ซื้อ = c.datetime,
                               });
                dataGridView6.DataSource = result3.ToList();

                if (result3 == null)


                {
                    MessageBox.Show("กรุณากรอกชื่อและเบอร์โทรศัพท์ใหม่อีกครั้ง");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(dateFrom.Value.Date<= dateTo.Value.Date)
            {
                //Console.WriteLine("Date to : "+ dateTo.Value.Date);
                var result1 = (from p in context.products
                              join oi in context.orderItems on p.pid equals oi.pid
                              join o in context.orders on oi.oid equals o.oid
                              where (o.datetime >= dateFrom.Value.Date) && (o.datetime <= dateTo.Value)
                              select new
                              {
                                  id = p.product_id,
                                  name = p.name,
                                  sum = oi.amount*p.price,
                                  date = o.datetime,
                              }).ToList();

                var result2 = (from item in result1
                               group item by item.id into g
                               select new
                               {
                                   รหัสสินค้า = g.Key,
                                   ชื่อสินค้า = g.FirstOrDefault().name,
                                   รายรับ = g.Sum(item => item.sum),
                                   วันที่ = g.FirstOrDefault().date

                               });
           
                dgvRevenueReport.DataSource = result2.ToList();
            }
            else
            {
                MessageBox.Show("กรุณากรอกวันที่ใหม่อีกครั้ง");
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            int date = int.Parse(textBox4.Text);
            DateTime today = DateTime.Today.Date;
            DateTime endDate= today.AddDays(-date);

            if (dateFrom.Value.Date <= dateTo.Value.Date)
            {
                //Console.WriteLine("Date to : "+ dateTo.Value.Date);
                var result1 = (from p in context.products
                              join oi in context.orderItems on p.pid equals oi.pid
                              join o in context.orders on oi.oid equals o.oid
                              //where (o.datetime == today) && (o.datetime <= endDate)
                              where o.datetime >= endDate
                              select new
                              {
                                  id = p.product_id,
                                  name = p.name,
                                  sum = oi.amount*p.price,
                                  date = o.datetime,
                              }).ToList();

                var result2 = (from item in result1
                               group item by item.id into g
                               select new
                               {
                                   รหัสสินค้า = g.Key,
                                   ชื่อสินค้า = g.FirstOrDefault().name,
                                   รายรับ = g.Sum(item => item.sum),
                                   วันที่ = g.FirstOrDefault().date

                               });
           
                dgvRevenueReport.DataSource = result2.ToList();
            }
            else
            {
                MessageBox.Show("กรุณากรอกวันที่ใหม่อีกครั้ง");
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
             AddItem addItem = new AddItem();
             addItem.ShowDialog();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dataGridView5.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string image = dataGridView5.Rows[e.RowIndex].Cells[5].Value.ToString();
                try
                {
                    pictureBox3.Image = ConvertBase64ToImage(image);
                }
                catch (System.FormatException er)
                {
                    pictureBox3.LoadAsync(image);
                }
            }

        }

        private void button33_Click(object sender, EventArgs e)
        {
            //int id = int.Parse(dataGridView5.Rows[e.].Cells["pid"].FormattedValue.ToString());

            //var result = context.products
            //        .Where(p => p.pid == id)
            //        .First();
            if (listView1.Items.Count < 2)
            {
                string[] item = new string[] {
                    textBox21.Text,
                    textBox22.Text,
                    textBox23.Text
                };
                listView1.Items.Add(new ListViewItem(item));
                int sum = Total(listView1.Items);
                labelSum.Text = sum.ToString();
            }
            else {
                MessageBox.Show("สามารถเลือกได้เพียง 2 ชิ้นเท่านั้น");
            }
               


        }

        private int Total(ListView.ListViewItemCollection items)
        {
            int sum = 0;
            foreach (ListViewItem item in items)
            {
                sum += int.Parse(item.SubItems[2].Text);

            }
            return sum;
        }

        private void dataGridView5_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            boxsetItems boxsetItems = new boxsetItems();
            boxsets boxsets = new boxsets();
            products products = new products();
            boxsets.detail = textBox24.Text;
            boxsets.status = 1;
            context.boxsets.Add(boxsets);
            int chage = context.SaveChanges();
            MessageBox.Show("บันทึก "+ chage);
            //boxsets.price = int.Parse(labelSum.Text) - (int?)(int.Parse(labelSum.Text) * 0.1);
            boxsets.price = (int?)(int.Parse(labelSum.Text) * 0.1);
            if (chage > 0) {
                foreach (ListViewItem item in listView1.Items)
                {
                    boxsetItems.pid = int.Parse(item.SubItems[0].Text);
                    boxsetItems.bid = boxsets.bid;

                    context.boxsetItems.Add(boxsetItems);
                    context.SaveChanges();
                    listView1.Items.Remove(item);
                    textBox21.Text = "";
                    textBox22.Text = "";
                    textBox23.Text = "";
                    textBox24.Text = "";
                }
                MessageBox.Show("บันทึก");
                var result3 = (from p in context.products
                               join b in context.boxsetItems on
                               p.pid equals b.pid
                               join b1 in context.boxsets on
                               b.bid equals b1.bid

                               select new
                               {
                                   รหัสชุด = b1.bid,
                                   ชื่อสินค้า = p.name,
                                   ราคาโปรโมชั้น = b1.price,
                                   ชุด = b1.detail,
                               });
                dataGridView8.DataSource = result3.ToList();

            }
            
        }

        private void button34_Click(object sender, EventArgs e)
        {
            string search = textBox25.Text;
            //string id = textBox19.Text;
            
            var result = context.products.Where(p => p.name.Contains(search) || p.pid.ToString().Contains(search)
            || p.price.ToString().Equals(search) || p.type.Contains(search) || p.product_id.Contains(search));
            productsBindingSource.DataSource = result.ToList();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ลบออกจากรายการที่เลือก", "ลบรายการสินค้า", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                foreach (ListViewItem item in listView1.Items)
                    if (item.Selected)
                        listView1.Items.Remove(item);

            }
            else if (DialogResult == DialogResult.No)
            {
                //Nothing to do
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            //var result = from b in context.boxsetItems
            //             join p in context.products on
            //              b.pid equals p.pid
            //             join b1 in context.boxsets on
            //             b.bid equals b1.bid
            //             where b.pid != null
            //             select new
            //             {
            //                 bid = b.bid
            //             };
            //int totalPomo = TotalPricePomo(listViewProduct.Items);
            string t = label40.Text;
            //double pomo;
            foreach (ListViewItem item in listViewProduct.Items) {
                if (item.SubItems[5].Text == "มีส่วนลด")
                {
                    //double pomo = int.Parse(t) - (int)(int.Parse(t) * 0.1);
                    double pomo = int.Parse(t) - int.Parse(label41.Text);
                    
                    //double pomo = 
                    label40.Text = pomo.ToString();
                    //label41.Text = totalPomo.ToString();
                    button35.Enabled = false;
                }
                else {
                   //MessageBox.Show("ไม่มี");
                }
                
                
            }
            //MessageBox.Show("ไม่มีส่วนลด");
            //}
            //------------------------------------------------
            //var id = listViewProduct.CheckedItems.Equals.();
            // Console.WriteLine(id);
            //var result = from b in context.boxsetItems
            //             join p in context.products on
            //              b.pid equals p.pid
            //             join b1 in context.boxsets on
            //             b.bid equals b1.bid
            //             where b.pid != null && b.bid.ToString().Count() > 1
            //             select new
            //             {
            //                 bid = b.bid
            //             };

            //if (result.FirstOrDefault() != null)
            //{
            //    int boxsetId = result.FirstOrDefault().bid;
            //    var result2 = from b in context.boxsetItems
            //                  join p in context.products on
            //                  b.pid equals p.pid
            //                  join b1 in context.boxsets on
            //                  b.bid equals b1.bid
            //                  where b.pid != null && b1.bid.ToString().Count() > 1
            //                  select new
            //                  {
            //                      รหัสสินค้า = p.product_id,
            //                      รหัส = b.pid,
            //                      ชื่อ = p.name,
            //                      ราคาโปร = b1.price,
            //                      ราคาจริง = p.price

            //                  };
            //    string name = result2.FirstOrDefault().ชื่อ;
            //    string pomotion = result2.FirstOrDefault().ราคาโปร.ToString();
            //    string priceProduct = result2.FirstOrDefault().ราคาจริง.ToString();
            //    string product_id = result2.FirstOrDefault().รหัสสินค้า.ToString();
            //    string pid = result2.FirstOrDefault().รหัส.ToString();
            //    Console.WriteLine(result2.FirstOrDefault().รหัส);
            //    MessageBox.Show("ซื้อร่วมกับ " + name + "ลดเหลือ " + pomotion + "บาท"+pid);

            //string promo = pomo - ((pomo) * 0.1));
            //}
        }

        private int TotalPricePomo(ListView.ListViewItemCollection items)
        {
            int pricePomo = 0;
            foreach (ListViewItem item in items)
            {
                pricePomo += int.Parse(item.SubItems[6].Text);

            }
            return pricePomo;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            int total = TotalPrice(listViewProduct.Items);
            //label40.Text = total.ToString();
            label40.Text = total.ToString();
            if (int.Parse(label40.Text) > 0)
            {
                button35.Enabled = true;
            }
            else
            {
                button35.Enabled = false;
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }
    }

    internal class ComboBoxItem
    {
        public string Value { get; set; } //product id
        public string Text { get; set; }//product type
        public ComboBoxItem(string val, string text)
        {
            Value = val;
            Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
