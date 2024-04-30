using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace AgriEnergyConnect.Pages
{
    public class ProfileModel : PageModel
    {
        public List<ProductModel> listedProducts = new List<ProductModel>();

        public void OnGet()
        {
            try
            {
                String connString = "server=localhost;database=agriconnectdb;uid=root;pwd='';";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    String sql = $"SELECT * FROM products";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using(MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductModel product = new ProductModel();
                                product.Id = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Description = reader.GetString(2);
                                product.Category = reader.GetString(3);
                                product.Price = reader.GetDecimal(6);
                                product.ImgUrl = reader.GetString(4);
                                product.UserId = reader.GetInt32(5);

                                listedProducts.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
