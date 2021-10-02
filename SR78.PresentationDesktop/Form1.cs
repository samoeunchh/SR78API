using SR78.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR78.PresentationDesktop
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client;
        public Form1()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            HttpResponseMessage response = await client.GetAsync("api/brands");
            listView1.Items.Clear();
            if (response.IsSuccessStatusCode)
            {
                var brands =await response.Content.ReadAsAsync<List<Brand>>();
                if (brands == null)
                {
                    MessageBox.Show("No record");
                    return;
                }
                foreach(var item in brands)
                {
                    var li = listView1.Items.Add(item.BrandId.ToString());
                    li.SubItems.Add(item.BrandName);
                    li.SubItems.Add(item.Description);
                }
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                MessageBox.Show(msg);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Brand Name is required");
                return;
            }
            var brand = new Brand
            {
                BrandName = textBox1.Text,
                Description = textBox2.Text
            };
            HttpResponseMessage response = await client.PostAsJsonAsync<Brand>("api/brands", brand);
            if (response.IsSuccessStatusCode)
            {
                Form1_Load(sender, e);
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                MessageBox.Show(msg);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var text = "Do you want to delete this record?";
                DialogResult result = MessageBox.Show(text, "Delete",MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    var id = listView1.SelectedItems[0].Text;
                    HttpResponseMessage response = await client.DeleteAsync("api/brands/"+ id);
                    if (response.IsSuccessStatusCode)
                    {
                        Form1_Load(sender, e);
                    }
                    else
                    {
                        var msg = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        MessageBox.Show(msg);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item for deleting...");
            }
        }
    }
}
