using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertComShop
{
    public partial class Form1 : Form
    {
        apd64_62011212131Entities3 context = new apd64_62011212131Entities3();
        string proType;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string url = "https://www.bnn.in.th/en/p/computer-hardware-diy/monitor-computer-hardware-diy?in_stock=true&sort_by=relevance&page=1";
            //HtmlWeb web = new HtmlWeb();
            //HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            //HtmlNode products = doc.DocumentNode.SelectSingleNode("//div[@total-page]");
            //if (products != null)
            //{
            //        textBox2.Text = products.GetAttributeValue("total-page","");
            //}
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            int page = int.Parse(textBox2.Text);

            HtmlWeb web1 = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc1 = web1.Load(url);
            HtmlNodeCollection productsType = doc1.DocumentNode.SelectNodes("//h1[@class='page-title']");

            if (productsType != null)
            {
                var typeProduct = productsType.SingleOrDefault();
                proType = typeProduct.InnerText;
            }
            for (int i = 1; i <= page; i++) {
                
                string[] spiltUrl = url.Split('=');
                string url1 = "";
                for (int j = 0; j < spiltUrl.Length-1; j++) {

                    url1 = url1 +"="+spiltUrl[j];      
                }
                url1 = url1 +"="+i;
                url1 = url1.Substring(1);
                //Console.WriteLine(url1);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url1);
                HtmlNodeCollection products = doc.DocumentNode.SelectNodes("//a[@class='product-item']");
                
                    if (products != null) {
                    foreach (var product in products) {
                        string productId = product.GetAttributeValue("href", "");
                        string[] subId = productId.Split('-');
                        productId = subId[subId.Length - 1];
                        //Console.WriteLine(productId);
                        HtmlNode name = product.SelectSingleNode("div/div[@class='product-name']");
                        //Console.WriteLine(name.InnerText );
                        HtmlNode detail = product.SelectSingleNode("div/div[@class='product-short-attribute']");
                        //Console.WriteLine(detail.InnerText);
                        HtmlNode price = product.SelectSingleNode("div/div[@class='product-price']");
                        //Console.WriteLine(price.InnerText);
                        HtmlNode image = product.SelectSingleNode("//img[@class='image']");
                        string img = image.GetAttributeValue("src", "");
                        //pictureBox1.Load(img);
                        //Console.WriteLine(image.InnerText);

                        //HtmlNode type = product.SelectSingleNode("div/div/div/div/h1[@class='page-title']");
                        //Console.WriteLine(type.InnerText);
                        ListViewItem Item = new ListViewItem(productId);
                        Item.SubItems.Add(name.InnerText);
                        listView1.Items.Add(Item);

                    }
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            HtmlNode products = doc.DocumentNode.SelectSingleNode("//div[@total-page]");
            if (products != null)
            {
                textBox2.Text = products.GetAttributeValue("total-page", "");
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                string url = "https://www.bnn.in.th/th/p/" + this.listView1.SelectedItems[0].Text;
                HtmlNodeCollection products = new HtmlWeb().Load(url).DocumentNode.SelectNodes("//div[@class=\"product-detail-summary\"]");
            if (products != null)
            {
                foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)products))
                {

                    textBox3.Text = listView1.SelectedItems[0].Text;
                    HtmlNode name = htmlNode.SelectSingleNode("h1[@class='page-title product-name']");
                    textBox4.Text = name.InnerText;
                    HtmlNode detail = htmlNode.SelectSingleNode("div[@class='product-short-description html-content']");
                    textBox5.Text = detail.InnerText;
                    string img = htmlNode.SelectSingleNode("//img[@class='image']").GetAttributeValue("src", "");
                    pictureBox1.Load(img);
                    HtmlNode price = htmlNode.SelectSingleNode("div/div/div[@class='selling-price']");

                    textBox6.Text = price.InnerText;    
                }
            }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            products products = new products();
            Console.WriteLine(listView1.Items.Count);
            foreach (ListViewItem item in this.listView1.Items)
            {
                
                //Console.WriteLine(item.SubItems[0].Text);
                products.product_id = item.Text;
                
                string url = "https://www.bnn.in.th/th/p/" + products.product_id;
                HtmlNodeCollection product = new HtmlWeb().Load(url).DocumentNode.SelectNodes("//div[@class=\"product-detail-summary\"]");
                if (product != null)
                {
                    foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)product))
                    {
                       
                        HtmlNode name = htmlNode.SelectSingleNode("h1[@class='page-title product-name']");
                        products.name = name.InnerText; 
                        HtmlNode detail = htmlNode.SelectSingleNode("div[@class='product-short-description html-content']");
                        string details = detail.InnerText;
                        products.detail = details;
                        string img = htmlNode.SelectSingleNode("//img[@class='image']").GetAttributeValue("src", "");
                        products.image = img;
                        HtmlNode price = htmlNode.SelectSingleNode("div/div/div[@class='selling-price']");
                        string[] subPrice = price.InnerText.Split(',','฿');
                        string join = string.Join("", subPrice);
                        Console.WriteLine(join);
                        products.price = int.Parse(join);
                        
                        products.type = proType;
                        products.amount = 20;
                        
                        
                        context.products.Add(products);
                        int change = context.SaveChanges();
                    }
                }
            }
            MessageBox.Show("Save");
        }
    }
}
