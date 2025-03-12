using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Borrow.Models;


namespace Borrow.Repo
{
    public class RecRepo
    {
        private readonly string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=activity;Integrated Security=True;TrustServerCertificate=True";
       

        public List<Rec> GetRecs()
        {
            var Rec = new List<Rec>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM BorrowRecord Order by id ASC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rec rec = new Rec();
                                rec.id = reader.GetInt32(0);
                                rec.Book = reader.GetString(1);
                                rec.Genre = reader.GetString(2);
                                rec.Author = reader.GetString(3);
                                rec.Member = reader.GetString(4);
                                

                                Rec.Add(rec);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                return Rec;
            }
            
        }

        public Rec GetRec(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM BorrowRecord WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Rec rec = new Rec();
                                rec.id = reader.GetInt32(0);
                                rec.Book = reader.GetString(1);
                                rec.Genre = reader.GetString(2);
                                rec.Author = reader.GetString(3);
                                rec.Member = reader.GetString(4);
                                

                                return rec;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());               
            }
            
            return null;
        }


        public void CreateRec(Rec rec)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    {
                        string sql = "INSERT INTO Borrowrecord" + "(Book,Genre,Author,Member) VALUES " +
                            "(@Book,@Genre,@Author,@Member);";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Book", rec.Book);
                            command.Parameters.AddWithValue("@Genre", rec.Genre);
                            command.Parameters.AddWithValue("@Author", rec.Author);
                            command.Parameters.AddWithValue("@Member", rec.Member);
                            

                            command.ExecuteNonQuery();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }


        public void UpdateRec(Rec rec)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                        connection.Open();
                    
                        string sql = "UPDATE Borrowrecord SET Book = @Book, Genre = @Genre, Author = @Author, Member = @Member WHERE id = @id";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Book", rec.Book);
                            command.Parameters.AddWithValue("@Genre", rec.Genre);
                            command.Parameters.AddWithValue("@Author", rec.Author);
                            command.Parameters.AddWithValue("@Member", rec.Member);
                            command.Parameters.AddWithValue("@id", rec.id);
                            

                            command.ExecuteNonQuery();

                        }
                    

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }


        public void DeleteRec(int bookid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    {
                        string sql = "DELETE FROM Borrowrecord WHERE id=@id";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", bookid);
                            
                            command.ExecuteNonQuery();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
