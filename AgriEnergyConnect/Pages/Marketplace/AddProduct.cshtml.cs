using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace AgriEnergyConnect.Pages.Marketplace
{
    public class AddProductModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            ProductModel product = new ProductModel();

            product.Name = Request.Form["name"];
            product.Category = Request.Form["category"];
            product.Description = Request.Form["description"];
            product.Price = Convert.ToDecimal(Request.Form["price"]);
            product.ImgUrl = Request.Form["url"];

            //Adding a new Product To The DB
            try
            {
                String connString = "server=localhost;database=agriconnectdb;uid=root;pwd='';";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    String sql = $"INSERT INTO products (name, description, category, imgUrl, user_id, price) VALUES ('{product.Name}', '{product.Description}', '{product.Category}', '{product.ImgUrl}', '34446025-5926-443f-afad-b5936d6c2dc1', '{product.Price}');";

                    using(MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            Response.Redirect("/Profile");
        }
    }
}
