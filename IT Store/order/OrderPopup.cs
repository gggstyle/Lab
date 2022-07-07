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

namespace IT_Store.order
{
    public partial class OrderPopup : Form
    {
        private products product ;
        private ListView listViewProduct ;
        public int status { get; set; }
        public OrderPopup(products product,ListView listViewProduct)
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
            if(numericUpDown1.Value == 0)
            {
                numericUpDown1.Value = 1;
            }
            int price = (int)product.price;
            int amount = (int)numericUpDown1.Value;
            textBoxTotal.Text = amount * price + "";
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            int price = (int)product.price;
            int amount = (int)numericUpDown1.Value;
            string[] item = new string[] {
                    product.product_id,
                    product.name,
                    numericUpDown1.Value.ToString(),
                    product.price.ToString(),
                    amount * price + ""

        };
            listViewProduct.Items.Add(new ListViewItem(item));

            this.Close();
        }

        private void OrderPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            status =  1;
        }
    }
}
