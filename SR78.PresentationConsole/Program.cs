using SR78.DataLayer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SR78.PresentationConsole
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("https://localhost:44397/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            GetCategoryAsync().Wait();
            Console.ReadLine();
        }
        static async Task GetCategoryAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/Category");
            if (response.IsSuccessStatusCode)
            {
                var categorys = await response.Content.ReadAsAsync<List<Category>>();
                foreach(var item in categorys)
                {
                    Console.WriteLine("Category Name:{0}", item.CategoryName);
                }
            }
            else
            {
                Console.WriteLine("No data");
            }
        }
    }
}
