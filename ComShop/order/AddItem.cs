using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComShop.order
{
    public partial class AddItem : Form
    {
        apd64_62011212131Entities8 context = new apd64_62011212131Entities8();
        public AddItem()
        {
            InitializeComponent();
        }

        private void AddItem_FormClosed(object sender, FormClosedEventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://www.bnn.in.th/th/p/" + textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            HtmlNodeCollection products = doc.DocumentNode.SelectNodes("//div[@class=\"product-detail-summary\"]");
            /// HtmlAgilityPack.HtmlDocument doc1 = web1.Load(url);
            
            if (products != null)
            {
                foreach (var product in products)
                {
                    HtmlNode title = product.SelectSingleNode("h1[@class='page-title product-name']");
                    textBox2.Text = title.InnerText;
                    HtmlNode detail = product.SelectSingleNode("div[@class='product-short-description html-content']");
                    textBox3.Text = detail.InnerText;
                    HtmlNode image = product.SelectSingleNode("//img[@class='image']");
                    string img = image.GetAttributeValue("src", "");
                    pictureBox1.Load(img);
                    textBox5.Text = img;
                    HtmlNode price = product.SelectSingleNode("div/div/div[@class='selling-price']");
                    string[] subPrice = price.InnerText.Split(',', '฿');
                    string join = string.Join("", subPrice);
                    textBox4.Text = join;
                    
                    //work ส่งอาทิตย์หน้าคือ ลิ้งของหน้า จอมอนิเตอร์วางไว้ค้นหา เพื่อค้นหาว่ามีกี่หน้า
                    //วนลูปเอา url มาเปลี่ยนเลขข้างหลัง
                    //แต่ละหน้าสั่งดึงข้อมูล
                    //กดโชว์ข้อมูล
                    //ปุ่ม insert ลงฐานข้อมูล
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            products products = new products();
            products.product_id = textBox1.Text;
            products.name = textBox2.Text;
            products.detail = textBox3.Text;
            //string[] subPrice = textBox4.Text.Split(',', '฿');
            //string join = string.Join("", subPrice);
            //Console.WriteLine(join);
            products.price = int.Parse(textBox4.Text);
            //products.price = int.Parse(textBox4.Text);
            products.amount = int.Parse(numericUpDown1.Value.ToString());
            products.type = comboBox1.Text;
            products.image = textBox5.Text;
            context.products.Add(products);

            int change = context.SaveChanges();
            MessageBox.Show("บันทึกข้อมูลสำเร็จ " + change + " ชิ้น");



        }

        private void AddItem_Load(object sender, EventArgs e)
        {

        }
    }
}
