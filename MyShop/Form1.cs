using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyShop
{
    public partial class Form1 : Form
    {
        apd64_62011212131Entities3 context = new apd64_62011212131Entities3();
        PictureBox pictureBox;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myOrderItemBindingSource.DataSource = context.MyOrderItem.ToList();
            //myMenuBindingSource.DataSource = context.MyMenu.ToList();

            var result = (from m in context.MyMenu
                          where m.type == "selected"
                          orderby m.menu_name
                          select new
                          {
                              รหัสสินค้า = m.mid,
                              ชื่อเมนู = m.menu_name,
                              ราคา = m.unit_price,
                              image = m.image
                          });
            readData(result,flpSelected);
           
            result = (from m in context.MyMenu
                      where m.type == "coffee"
                      orderby m.menu_name
                      select new
                      {
                          รหัสสินค้า = m.mid,
                          ชื่อเมนู = m.menu_name,
                          ราคา = m.unit_price,
                          image = m.image
                      });
            readData(result, flpCoffee);

            result = (from m in context.MyMenu
                      where m.type == "tea"
                      orderby m.menu_name
                      select new
                      {
                          รหัสสินค้า = m.mid,
                          ชื่อเมนู = m.menu_name,
                          ราคา = m.unit_price,
                          image = m.image
                      });
            readData(result, flpTea);

            result = (from m in context.MyMenu
                      where m.type == "milk"
                      orderby m.menu_name
                      select new
                      {
                          รหัสสินค้า = m.mid,
                          ชื่อเมนู = m.menu_name,
                          ราคา = m.unit_price,
                          image = m.image
                      });
            readData(result, flpMilk);

            result = (from m in context.MyMenu
                      where m.type == "juice"
                      orderby m.menu_name
                     
                      select new
                      {
                          รหัสสินค้า = m.mid,
                          ชื่อเมนู = m.menu_name,
                          ราคา = m.unit_price,
                          image = m.image
                      });

            readData(result, flpJuice);
        }

        private void readData(dynamic result,FlowLayoutPanel flp) {
            foreach (var item in result)
            {
                Panel panel = new Panel();
                Panel panelPrice = new Panel();
                panel.Width = 150;
                panel.Height = 200;
                pictureBox = new PictureBox();
                pictureBox.Width = 150;
                pictureBox.Height = 150;
                //pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Image = (Bitmap)(new ImageConverter()).ConvertFrom(item.image);
                Label labelName = new Label();
                Label labelPrice = new Label();
                labelName.AutoSize = false;
                labelName.Text = item.ชื่อเมนู.ToString();
                labelName.TextAlign = ContentAlignment.MiddleCenter;
                labelName.Dock = DockStyle.Bottom;
                labelName.BackColor = Color.White;
                labelPrice.Text = "ราคา " + item.ราคา.ToString() + " บาท";
                labelPrice.TextAlign = ContentAlignment.MiddleCenter;
                labelPrice.Dock = DockStyle.Bottom;
                labelPrice.BackColor = Color.White;
                panel.Dock = DockStyle.Fill;
                panel.Controls.Add(pictureBox);
                panel.Dock = DockStyle.Bottom;
                panelPrice.Dock = DockStyle.Fill;
                panelPrice.Controls.Add(labelName);
                panelPrice.Dock = DockStyle.Bottom;
                //labelName.Text = item.ราคา.ToString();
                panelPrice.Controls.Add(labelPrice);
                pictureBox.Tag = item.รหัสสินค้า;
                panel.Controls.Add(panelPrice);
                pictureBox.Click += new EventHandler(PictureBox_Click);
                flp.Controls.Add(panel);
            }


        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(((PictureBox)sender).Tag.ToString());
            int id = int.Parse(((PictureBox)sender).Tag.ToString());

            var result = (from m in context.MyMenu
                          where m.mid == id

                          select new
                          {
                              mid = m.mid,
                              name = m.menu_name,
                              price = m.unit_price,
                              image = m.image
                          }).FirstOrDefault();
            menuName.Text = result.name;
            unitPrice.Text = result.price.ToString();
            label7.Text = result.mid.ToString();
            //Console.WriteLine(id);
            numericUpDown1.Focus();
            if (result.image != null)
            {
                pictureBox1.Image = (Bitmap)(new ImageConverter()).ConvertFrom(result.image);

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13){
                string[] item = new string[] {
                    menuName.Text,
                    numericUpDown1.Value.ToString(),
                    unitPrice.Text,
                    (int.Parse(unitPrice.Text) * numericUpDown1.Value).ToString(),
                    label7.Text
                };
                listView1.Items.Add(new ListViewItem (item));
                int sum = calculateTotal(listView1.Items);
                
                label5.Text = sum.ToString();

                int amount = calAmount(listView1.Items);
                label8.Text = amount.ToString();
            }
        }

        private int calAmount(ListView.ListViewItemCollection items)
        {
            int amount = 0;
            foreach (ListViewItem item in items)
            {
                amount += int.Parse(item.SubItems[1].Text);

            }
            return amount;
        }

        private int calculateTotal(ListView.ListViewItemCollection items)
        {
            int sum = 0;
            foreach(ListViewItem item in items)
            {
                sum += int.Parse(item.SubItems[3].Text);

            }
            return sum;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyOrder myOrder = new MyOrder();
            myOrder.totalAmount = int.Parse(label5.Text);
            myOrder.amount = int.Parse(label8.Text);
            context.MyOrder.Add(myOrder);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + "records");
            if(change == 1)
            {
                
                foreach (ListViewItem item in listView1.Items)
                {
                    MyOrderItem myOrderItem = new MyOrderItem();
                    myOrderItem.oid = myOrder.oid;
                    Console.WriteLine(myOrder.oid);
                    myOrderItem.mid = int.Parse(item.SubItems[4].Text);
                    myOrderItem.count = int.Parse(item.SubItems[1].Text);
                    myOrderItem.sum = int.Parse(item.SubItems[3].Text);



                    //myOrderItem.MyMenu.unit_price = int.Parse(item.SubItems[2].Text);
                    // myOrderItem
                    context.MyOrderItem.Add(myOrderItem);
                    context.SaveChanges();


                }
                
                MessageBox.Show("Save completed");
                listView1.Items.Clear();
                menuName.Text = "";
                label7.Text = "";
                unitPrice.Text = "";
                label5.Text = " ";
                //pictureBox1 = null;


            }

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
                if (item.Selected)
                    listView1.Items.Remove(item);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
           
        {
           
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                
                int id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["รหัสออเดอร์"].FormattedValue.ToString());
                var result = (from m in context.MyOrderItem
                              join m1 in context.MyMenu on
                              m.mid equals m1.mid
                              join m2 in context.MyOrder on
                              m.oid equals m2.oid
                              where m2.oid == id
                              select new
                              {
                                  ชื่อเมนู = m1.menu_name,           
                                  ราคา = m1.unit_price,
                                  จำนวน = m.count,
                                  รวม = m.sum

                              });
                              
                dataGridView2.DataSource = result.ToList();
            }



        }

        private void flpSelected_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            var result = (from m in context.MyOrder
                          orderby m.oid descending
                          select new
                          {
                              รหัสออเดอร์ = m.oid,
                              
                              จำนวน = m.amount,
                              ราคารวม = m.totalAmount

                          });
            dataGridView1.DataSource = result.ToList();


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }
    }
}
