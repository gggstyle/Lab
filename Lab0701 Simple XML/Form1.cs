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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab0701_Simple_XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string path = Directory.GetCurrentDirectory();
            XmlReader reader = XmlReader.Create(path + Path.DirectorySeparatorChar
                +"bookstore.xml");
            //การแปลงxmlกับclass
            XmlSerializer serializer = new XmlSerializer(typeof(bookstore));
            var bs = (bookstore)serializer.Deserialize(reader);
            foreach (bookstoreBook book in bs.book) { 
                listBox1.Items.Add("Book Title: " + book.title.Value);
                listBox1.Items.Add("Author : ");
                foreach(var author in book.author)
                {
                    listBox1.Items.Add(" > " + author);
                }
                listBox1.Items.Add("==========");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear ();
            string path = Directory.GetCurrentDirectory();
            XmlReader reader = XmlReader.Create(path + Path.DirectorySeparatorChar
                + "bookstore.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(bookstore));
            var bs = (bookstore)serializer.Deserialize(reader);
            foreach (bookstoreBook book in bs.book) {
                if (book.title.Value == textBox1.Text) {
                    listBox1.Items.Add("Book Totle: " + book.title.Value);
                    foreach (string author in book.author) {
                        listBox1.Items.Add(" > " + author);
                    }
                    listBox1.Items.Add("=================");
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string path = Directory.GetCurrentDirectory();
            XElement xElement = XElement.Load(path
                + Path.DirectorySeparatorChar + "bookstore.xml");
            var books = xElement.Descendants("book");
            var result = books
                .Where(b => b.Element("title").Value == textBox1.Text);
            foreach (var book in books) { 
                listBox1.Items.Add("Book Title: " + book.Element("title").Value);
                foreach (var author in book.Elements("author")) {
                    listBox1.Items.Add(" > " + author.Value);
                }
            }

        }
    }
}
