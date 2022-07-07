using System;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Lab_0704_Json_Array
{
    public partial class Form1 : Form
    {
        public Post[] posts { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string jsonResult = client
                .DownloadString("https://jsonplaceholder.typicode.com/posts");
            
            var seializer = new JavaScriptSerializer();
            //Post post = (Post)seializer.Deserialize(jsonResult, typeof(Post));
            posts = (Post[])seializer.Deserialize(jsonResult,typeof(Post[]));
            foreach (Post post in posts) {
                comboBox1.Items.Add(post.id);
            }       
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = comboBox1.Text;
            foreach (Post post in posts) {
                if (post.id == int.Parse(id)) {
                    textBox1.Text = post.userId.ToString();
                    textBox2.Text = post.title;
                    textBox3.Text = post.body;
                }
            }
        }

    }
}
