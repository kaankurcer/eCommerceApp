using eCommerceApp.DTOs;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace eCommerceApp.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IStringLocalizer<ProductsController> _stringLocalizer;
        public ProductsController(IStringLocalizer<ProductsController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Products";
            sqlCmd.Connection = myConnection;

            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            List<Product> products = new List<Product>();
            Product prd = null;
            while (reader.Read())
            {
                prd = new Product();
                prd.Id = Convert.ToInt32(reader.GetValue(0));

                prd.Name = _stringLocalizer[reader.GetValue(1).ToString()];
                prd.Category = _stringLocalizer[reader.GetValue(2).ToString()];
                products.Add(prd);
            }
            if (products.Count == 0)
                return BadRequest("There are no products to display!");
            return products;
        }

        [HttpGet("category")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(string category)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Products WHERE Category='" + category + "'";
            sqlCmd.Connection = myConnection;

            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            List<Product> products = new List<Product>();
            Product prd = null;
            while (reader.Read())
            {
                prd = new Product();
                prd.Id = Convert.ToInt32(reader.GetValue(0));
                prd.Name = reader.GetValue(1).ToString();
                prd.Category = reader.GetValue(2).ToString();
                products.Add(prd);
            }
            if (products.Count == 0)
                return BadRequest("There are no products of given category!");
            return products;
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<bool>> AddProduct(ProductDto productDto)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Products (Name, Category) VALUES (@Name, @Category)";
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@Name", productDto.Name);
            sqlCmd.Parameters.AddWithValue("@Category", productDto.Category);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            return true;
        }
    
        
    
    
    }
}