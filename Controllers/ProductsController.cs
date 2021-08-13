using eCommerceApp.DTOs;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace eCommerceApp.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts(string language)
        {
            SqlDataReader reader = null;
            SqlConnection databaseConnection = new SqlConnection();
            databaseConnection.ConnectionString = connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = databaseConnection;
            sqlCmd.CommandType = CommandType.Text;
            databaseConnection.Open();

            //Check if the language already exists or not
            language = language.ToLower();
            int languageId = 0;
            sqlCmd.CommandText = "SELECT * FROM Languages WHERE Language='" + language + "'";
            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                if (language.Equals(reader.GetValue(1).ToString()))
                {
                    languageId = Convert.ToInt32(reader.GetValue(0));
                    break;
                }
                return BadRequest("No localization options avaliable for the given language");
            }
            reader.Close();

            sqlCmd.CommandText = "SELECT * FROM Products";

            List<int> productIds = new List<int>();
            List<int> categoryIds = new List<int>();
            List<string> productNames = new List<string>();
            List<Product> products = new List<Product>();

            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                int productId = Convert.ToInt32(reader.GetValue(0));
                string productName = reader.GetValue(1).ToString();
                int categoryId = Convert.ToInt32(reader.GetValue(2));

                productIds.Add(productId);
                productNames.Add(productName);
                categoryIds.Add(categoryId);
            }

            reader.Close();

            for (int i = 0; i < productIds.Count; i++)
            {
                //Initialize the temporary product object
                Product tmpProduct = new Product();
                string productTranslation = "";
                string categoryTranslation = "";

                //Get the product name translation
                sqlCmd.CommandText = "SELECT * FROM ProductTranslations WHERE ProductId ='" + productIds[i] + "' AND LanguageId='" + languageId + "'";
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    productTranslation = reader.GetValue(2).ToString();
                }
                reader.Close();

                //If a translation cant be found, get the english name
                if (productTranslation.Equals(""))
                {
                    productTranslation = productNames[i];
                }

                //Get the category translation
                sqlCmd.CommandText = "SELECT * FROM CategoryTranslations WHERE CategoryId ='" + categoryIds[i] + "' AND LanguageId='" + languageId + "'";
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    categoryTranslation = reader.GetValue(2).ToString();
                }
                reader.Close();

                //If a translation cant be found, get the english category
                if (categoryTranslation.Equals(""))
                {
                    sqlCmd.CommandText = "SELECT * FROM Categories WHERE Id ='" + categoryIds[i] + "'";
                    reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categoryTranslation = reader.GetValue(1).ToString();
                    }
                    reader.Close();
                }

                //Create the product object and put it in a list
                tmpProduct.Name = productTranslation;
                tmpProduct.Category = categoryTranslation;
                tmpProduct.Id = productIds[i];

                products.Add(tmpProduct);
            }

            databaseConnection.Close();

            //If there are no products give an error message
            if (products.Count == 0)
                return BadRequest("There are no products!");
            return products;
        }

        [HttpGet("category")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(string categoryName)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT Id FROM Categories WHERE Category='" + categoryName + "'";
            sqlCmd.Connection = myConnection;

            //Check if the category exists or not
            int categoryId = 0;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                categoryId = Convert.ToInt32(reader.GetValue(0));
            }

            reader.Close();
            //If no such category exists send an error message
            if (categoryId == 0)
            {
                return BadRequest("There is no such category!");
            }

            sqlCmd.CommandText = "SELECT * FROM Products WHERE CategoryId='" + categoryId + "'";
            reader = sqlCmd.ExecuteReader();

            List<Product> products = new List<Product>();
            Product prd = null;
            while (reader.Read())
            {
                prd = new Product();
                prd.Id = Convert.ToInt32(reader.GetValue(0));
                prd.Name = reader.GetValue(1).ToString();
                prd.Category = categoryName;
                products.Add(prd);
            }

            reader.Close();
            //If there are no products of given category give an error message
            if (products.Count == 0)
                return BadRequest("There are no products of given category!");
            return products;
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<bool>> AddProduct(ProductDto productDto)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();

            //Check if a product with the given name already exists
            sqlCmd.CommandText = "SELECT Name FROM Products WHERE Name='" + productDto.Name + "'";
            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                if (productDto.Name.Equals(reader.GetValue(0).ToString())) return BadRequest("That product is already registered!");
            }
            reader.Close();

            //First try to find the given Category from the Categories table
            sqlCmd.CommandText = "SELECT Id FROM Categories WHERE Category='" + productDto.Category + "'";

            int categoryId = 0;
            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                categoryId = Convert.ToInt32(reader.GetValue(0));
            }
            reader.Close();
            //If the category doesn't exist, insert a new one 
            if (categoryId == 0)
            {
                sqlCmd.CommandText = "INSERT INTO Categories (Category) VALUES ('" + productDto.Category + "')";
                sqlCmd.ExecuteNonQuery();
                sqlCmd.CommandText = "SELECT Id FROM Categories WHERE Category='" + productDto.Category + "'";
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    categoryId = Convert.ToInt32(reader.GetValue(0));
                }
                reader.Close();
            }
            //Insert the product into the products table
            sqlCmd.CommandText = "INSERT INTO Products (Name, CategoryId) VALUES ('" + productDto.Name + "', '"+ categoryId + "')";
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            return true;
        }
    }
}