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
                foreach(var item in brands)
                {
                    var li = listView1.Items.Add(item.BrandId.ToString());
                    li.SubItems.Add(item.BrandName);
                    li.SubItems.Add(item.Description);
                }
            }
        }
    }
}
