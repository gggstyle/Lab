using Newtonsoft.Json;
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

namespace Lab0705_Json_Linq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            WebClient client = new WebClient();
            string jsonResult = client
                .DownloadString("https://jsonplaceholder.typicode.com/users");
            var serializer = new JavaScriptSerializer();
            User[] users = (User[])serializer.Deserialize(jsonResult,typeof(User[]));
            var result = users.Where(u => u.name.Contains(textBox1.Text));
            foreach (User user in result) { 
                textBox2.AppendText(user.name + "\n");
                //string jsonStr = serializer.Serialize(user);
                string jsonStr = JsonConvert.SerializeObject(user,Formatting.Indented);
                textBox2.AppendText(jsonStr + "\n");
            }
        }
    }
}
