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

namespace Lab0702_Banana
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //h1[@class='page-title product-name'] หาชื่อ
            //คำอธิบาย ราคา รูปภาพ ประเภท
            //"div[@class='product-short-description html-content']"
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://www.bnn.in.th/th/p/" + textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            HtmlNodeCollection products = doc.DocumentNode.SelectNodes("//div[@class=\"product-detail-summary\"]");
            if (products != null) {
                foreach (var product in products) {
                    HtmlNode title = product.SelectSingleNode("h1[@class='page-title product-name']");
                    textBox2.Text = title.InnerText;
                    HtmlNode detail = product.SelectSingleNode("div[@class='product-short-description html-content']");
                    textBox3.Text = detail.InnerText;
                    HtmlNode image = product.SelectSingleNode("//img[@class='image']");
                    string img = image.GetAttributeValue("src", "");
                    pictureBox1.Load(img);
                    HtmlNode price = product.SelectSingleNode("div/div/div[@class='selling-price']");
                    textBox4.Text = price.InnerText;
                    //work ส่งอาทิตย์หน้าคือ ลิ้งของหน้า จอมอนิเตอร์วางไว้ค้นหา เพื่อค้นหาว่ามีกี่หน้า
                    //วนลูปเอา url มาเปลี่ยนเลขข้างหลัง
                    //แต่ละหน้าสั่งดึงข้อมูล
                    //กดโชว์ข้อมูล
                    //ปุ่ม insert ลงฐานข้อมูล
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
