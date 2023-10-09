using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ADODOTNET
{
    class Program
    {
        static string constring = @"Data Source=.\SQLEXPRESS;Initial Catalog=ProductDatabase;Integrated Security=true;TrustServerCertificate=True";

        static void InsertProductDetails(int ProductID, string ProductName, double ProductPrice)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductID, ProductName, ProductPrice) VALUES (@ProductID, @ProductName, @ProductPrice)", con);

                cmd.Parameters.AddWithValue("ProductID", ProductID);
                cmd.Parameters.AddWithValue("ProductName",ProductName);
                cmd.Parameters.AddWithValue("ProductID", ProductPrice);

                cmd.ExecuteNonQuery(); // insert
                Console.WriteLine("The record is added");
            }
        }

        static void SelectAllProducts()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Product", con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ProductID: {reader["ProductID"]}, ProductName: {reader["ProductName"]}, ProductPrice: {reader["ProductPrice"]}");
                    }
                }
            }
        }

        static void SelectProductByID(int ProductID)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("ProductID", ProductID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ProductID: {reader["ProductID"]}, ProductName: {reader["ProductName"]}, ProductPrice: {reader["ProductPrice"]}");
                    }
                }
            }
        }

        static void UpdateProductPrice(int ProductID, double NewProductPrice)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductPrice = @NewProductPrice WHERE ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("NewProductPrice", NewProductPrice);
                cmd.Parameters.AddWithValue("ProductID", ProductID);

                cmd.ExecuteNonQuery(); // update
                Console.WriteLine("Product price updated");
            }
        }

        static void DeleteProductByID(int ProductID)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("ProductID", ProductID);

                cmd.ExecuteNonQuery(); // delete
                Console.WriteLine("Product deleted");
            }
        }

        static bool PromptYesOrNo(string message)
        {
            Console.Write($"{message} (yes/no): ");
            string input = Console.ReadLine().ToLower();
            return input == "yes" || input == "y";
        }

        static void Main(string[] args)
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.WriteLine("Enter option:");
                Console.WriteLine("1. Insert product");
                Console.WriteLine("2. Select all products");
                Console.WriteLine("3. Select product by ID");
                Console.WriteLine("4. Update product price");
                Console.WriteLine("5. Delete product by ID");
                Console.WriteLine("6. Exit");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter the ProductID");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the ProductName");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Enter the ProductPrice");
                        double Price = Convert.ToDouble(Console.ReadLine());
                        InsertProductDetails(ID, Name, Price);
                        break;
                    case 2:
                        SelectAllProducts();
                        break;
                    case 3:
                        Console.WriteLine("Enter the ProductID");
                        int selectedID = Convert.ToInt32(Console.ReadLine());
                        SelectProductByID(selectedID);
                        break;
                    case 4:
                        Console.WriteLine("Enter the ProductID");
                        int updateID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the new ProductPrice");
                        double newPrice = Convert.ToDouble(Console.ReadLine());
                        UpdateProductPrice(updateID, newPrice);
                        break;
                    case 5:
                        Console.WriteLine("Enter the ProductID");
                        int deleteID = Convert.ToInt32(Console.ReadLine());
                        DeleteProductByID(deleteID);
                        break;
                    case 6:
                        continueProgram = PromptYesOrNo("Do you want to exit?");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                if (continueProgram)
                {
                    bool continueResponse = PromptYesOrNo("Do you want to continue?");
                    if (!continueResponse)
                    {
                        continueProgram = false;
                    }
                    Console.Clear();
                }
            }
        }
    }
}
