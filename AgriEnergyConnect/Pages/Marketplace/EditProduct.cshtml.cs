using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using MySql.Data.MySqlClient;

namespace AgriEnergyConnect.Pages.Marketplace
{
    public class EditProductModel : PageModel
    {
        public ProductModel editedProduct = new ProductModel();

        public ProductModel updatedProduct = new ProductModel();

        public string productId;
        public void OnGet()
        {
            productId = Request.Query["id"];

            try
            {
                String connString = "server=localhost;database=agriconnectdb;uid=root;pwd='';";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    String sql = $"SELECT * FROM products WHERE product_id = {productId}";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
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

                                editedProduct = product;
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

        public void OnPost()
        {
            updatedProduct.Name = Request.Form["name"];
            updatedProduct.Category = Request.Form["category"];
            updatedProduct.Description = Request.Form["description"];
            updatedProduct.Price = Convert.ToDecimal(Request.Form["price"]);
            updatedProduct.ImgUrl = Request.Form["url"];

            try
            {
                String connString = "server=localhost;database=agriconnectdb;uid=root;pwd='';";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    String sql = $"UPDATAE products SET name = {updatedProduct.Name}, description = {updatedProduct.Description}, category = {updatedProduct.Category}, imgUrl = {updatedProduct.ImgUrl}, price = {updatedProduct.Price} WHERE product_id = {productId}";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
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
        }
    }
}
