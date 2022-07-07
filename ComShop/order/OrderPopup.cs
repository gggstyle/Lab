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

namespace ComShop.order
{
    public partial class OrderPopup : Form
    {
        apd64_62011212131Entities8 context = new apd64_62011212131Entities8();
        private products product;
        private ListView listViewProduct;
        
        public int status { get; set; }
        public OrderPopup(products product, ListView listViewProduct)
        {
            InitializeComponent();
            this.product = product;
            this.listViewProduct = listViewProduct;
            
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

        private void OrderPopup_Load(object sender, EventArgs e)
        {
            textBoxID.Text = product.product_id.ToString();
            textBoxProductName.Text = product.name.ToString();
            textBoxPrice.Text = product.price.ToString();
            label2.Text = product.pid.ToString();
            try
            {
                pictureBoxProduct.Image = ConvertBase64ToImage(product.image.ToString());
            }
            catch (System.FormatException er)
            {
                pictureBoxProduct.LoadAsync(product.image.ToString());
            }
            numericUpDown1_ValueChanged(sender, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                numericUpDown1.Value = 1;
            }
            int price = (int)product.price;
            int amount = (int)numericUpDown1.Value;
            textBoxTotal.Text = amount * price + "";
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            var id = int.Parse(label2.Text);
            var result = from b in context.boxsetItems
                         join p in context.products on
                          b.pid equals p.pid
                         join b1 in context.boxsets on
                         b.bid equals b1.bid
                         where b.pid == id
                         select new
                         {
                             bid = b.bid
                         };

            if (result.FirstOrDefault() != null)
            {
                int boxsetId = result.FirstOrDefault().bid;
                var result2 = from b in context.boxsetItems
                              join p in context.products on
                              b.pid equals p.pid
                              join b1 in context.boxsets on
                              b.bid equals b1.bid
                              where b.bid == boxsetId && b.pid != id
                              select new
                              {
                                  รหัสสินค้า = p.product_id,
                                  รหัส = b.pid,
                                  ชื่อ = p.name,
                                  ราคาโปร = b1.price,
                                  ราคาจริง = p.price
                                  

                              };
                string name = result2.FirstOrDefault().ชื่อ;
                string pomotion = result2.FirstOrDefault().ราคาโปร.ToString();
                string priceProduct = result2.FirstOrDefault().ราคาจริง.ToString();
                string product_id = result2.FirstOrDefault().รหัสสินค้า.ToString();
                string pid = result2.FirstOrDefault().รหัส.ToString();
                Console.WriteLine(result2.FirstOrDefault().รหัส);
                //MessageBox.Show("ซื้อร่วมกับ " + name + "ลดเหลือ " + pomotion + "บาท");
                DialogResult dr = MessageBox.Show("ซื้อร่วมกับ " + name + "ลด " + "10%", "โปรโมชั่น", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string[] item1 = new string[] {
                    product_id,
                    name,
                    "1",
                    priceProduct,
                    priceProduct,
                    "มีส่วนลด",
                    pomotion

        };
                    listViewProduct.Items.Add(new ListViewItem(item1));
 
                    this.Close();
                }
                else if (DialogResult == DialogResult.No)
                {
                    this.Close();
                }


            }
            else
            {
                Console.WriteLine("00000000000000000000");
            }
            int zoro = 0;
            int price = (int)product.price;
            int amount = (int)numericUpDown1.Value;
            string[] item = new string[] {
                    product.product_id,
                    product.name,
                    numericUpDown1.Value.ToString(),
                    product.price.ToString(),
                    amount * price + "",
                    //label2.Text
                    "",
                    zoro.ToString()

        };
            listViewProduct.Items.Add(new ListViewItem(item));
            
            this.Close();
        }

        private void OrderPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            status = 1;
        }
    }
}
