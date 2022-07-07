using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Lab0703_Json_Read
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string jsonResult = client
                .DownloadString("https://jsonplaceholder.typicode.com/posts/" 
                +textBox1.Text);
            // Json => C# = Deserialization
            var seializer = new JavaScriptSerializer();
            Post post = (Post)seializer.Deserialize(jsonResult,typeof(Post));
            textBox2.Text = post.userId.ToString();
            textBox3.Text = post.title;
            textBox4.Text = post.body;

        }
    }
}
