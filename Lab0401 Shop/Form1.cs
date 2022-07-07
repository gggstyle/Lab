using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab0401_Shop
{
    public partial class Form1 : Form
    {
        apd64_62011212131Entities context = new apd64_62011212131Entities();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.OrderNumber = "123456";
            order.CustomerId = 2;
            order.TotalAmount = decimal.Parse(label3.Text);

            context.Order.Add(order);
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + " records");

            if (change == 1) {
                foreach (ListViewItem item in listView1.Items) {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = order.Id;
                    orderItem.ProductId = int.Parse(item.SubItems[0].Text);
                    orderItem.UnitPrice = decimal.Parse(item.SubItems[3].Text);

                    orderItem.Quantity = int.Parse(item.SubItems[2].Text);
                    context.OrderItem.Add(orderItem);
                    context.SaveChanges();
                }
                MessageBox.Show("Save Completed");
            }


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductName = textBox11.Text;
            product.UnitPrice = decimal.Parse(textBox10.Text);
            product.Package = textBox9.Text;
            product.IsDiscontinued = bool.Parse(textBox8.Text);
            product.SupplierId = int.Parse(((ComboBoxItem)(comboBox1.SelectedItem)).Value);
            context.Product.Add(product);
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + "records");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ค้นหาข้อมูลขึ้นมาก่อน แล้วค่อย update 
            int id = int.Parse(textBox12.Text);
            var result = context.Product
                .Where(p => p.Id == id)
                .First();
            result.ProductName = textBox11.Text;
            result.UnitPrice = decimal.Parse(textBox10.Text);
            result.Package = textBox9.Text;
            result.IsDiscontinued = bool.Parse(textBox8.Text);
            result.SupplierId = int.Parse(((ComboBoxItem)(comboBox1.SelectedItem)).Value);


            // Save ข้อมูลที่เปลี่ยนแล้ว
            int change = context.SaveChanges();
            if (change > 0)
            {
                MessageBox.Show("Update Success");
                productBindingSource.DataSource = context.Product.ToList();
            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //หาให้ได้ก่อนว่าข้อมูลที่จะ delete คืออะไร
            int id = int.Parse(textBox12.Text);
            var result = context.Product
                .Where(p => p.Id == id)
                .First();
            context.Product.Remove(result);
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + " records");
            productBindingSource.DataSource = context.Product.ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            customerBindingSource.DataSource = context.Customer.ToList();
            productBindingSource.DataSource = context.Product.ToList();
            //select table Supplier เรียงตาม Company Name
            var results = context.Supplier.OrderBy(s => s.CompanyName)
                .Select(s => new { s.Id, s.CompanyName });
            //วนลูป
            foreach (var result in results)
            {
                comboBox1.Items.Add(new ComboBoxItem(result.Id.ToString(), result.CompanyName));
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.FirstName = textBox18.Text;
            customer.LastName = textBox17.Text;
            customer.City = textBox16.Text;
            customer.Country = textBox15.Text;
            customer.Phone = textBox14.Text;

            context.Customer.Add(customer);
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + " records");
            customerBindingSource.DataSource = context.Customer.ToList();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            customerBindingSource.AddNew();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            customerBindingSource.EndEdit();
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + " records");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string id = textBox18.Text;
            int idInt = int.Parse(id);
            //เอาข้อมูลไปหาว่าข้อมูลเป็นไง
            var toDelete = context.Customer
                .Where(c => c.Id == idInt)
                .First();
            context.Customer.Remove(toDelete);
            int change = context.SaveChanges();
            MessageBox.Show("Change " + change + "records");

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView2.SelectedRows[0]
                .Cells[0].Value.ToString());
            var result = context.Product.Where(p => p.Id == id).First();

            textBox12.Text = result.Id.ToString();
            textBox11.Text = result.ProductName;
            textBox10.Text = result.UnitPrice.ToString();
            textBox9.Text = result.Package;
            textBox8.Text = result.IsDiscontinued.ToString();
            comboBox1.Text = result.Supplier.CompanyName;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(((ComboBoxItem)(comboBox1.SelectedValue)).Value);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                numericUpDown1.Focus();
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                int id = int.Parse(textBox1.Text);
                var result = context.Product.Where(p => p.Id == id).First();

                //เอาสินค้าไปใส่ใน listView
                //สร้าง array ตาม table
                string[] item = new string[] {
                    result.Id.ToString(),
                    result.ProductName,
                    numericUpDown1.Value.ToString(),
                    result.UnitPrice.ToString(),
                    (result.UnitPrice * numericUpDown1.Value).ToString()
                };
                //เอา array add เข้าไปแล้วแปลงเป็น ListViewItem
                listView1.Items.Add(new ListViewItem(item));
                double sum = calculateTotal(listView1.Items);
                label3.Text = sum.ToString();


            }
        }

        private double calculateTotal(ListView.ListViewItemCollection items)
        {
            //คำนวณ เอาทุกอย่างที่อยู่ใน listView1 มาลูปแต่ละรอบแล้วเอามาบวกกัน
            double sum = 0;
            foreach (ListViewItem item in items)
            {
                sum += double.Parse(item.SubItems[4].Text);
            }
            return sum;
        }

        internal class ComboBoxItem
        {
            public string Value { get; set; } // Supplier ID
            public string Text { get; set; } // Supplier Company Name
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
