using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FamousPeople.Classes
{
    class StoreDB
    {
        public Person GetPerson(int personId)
        {
            Person person = null;
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM PersonsTable WHERE Id={personId}", connection);
                command.Parameters.AddWithValue("Id", personId);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    reader.Read();
                    person = new Person()
                    {
                        Name = reader[1].ToString() + ' '+ reader[2].ToString(),
                        Description = reader[3].ToString(),
                        ImageId = reader[4].ToString(),
                    };
                }
                catch (Exception e)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    throw e;
                }
                connection.Close();
                return person;
            }
        }

        public void UpdatePerson(Person person, int personId)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {

                string fName = person.Name.Split(' ')[0];
                string lName = person.Name.Split(' ')[1];
                SqlCommand command = new SqlCommand($"UPDATE PersonsTable SET FirstName={fName},LastName={lName},Description={person.Description} WHERE Id={personId}", connection);


                command.Parameters.AddWithValue("firstname",  person.Name.Split(' ')[0]);
                command.Parameters.AddWithValue("lastname", person.Name.Split(' ')[1]);
                command.Parameters.AddWithValue("description", person.Description);
                command.Parameters.AddWithValue("ID", personId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Person> GetAllPersons()
        {
            List<Person> books = new List<Person>();
            SqlDataReader reader = null;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM PersonsTable", connection);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Person book = new Person()
                        {
                            Name = reader[1].ToString() + ' '+ reader[2].ToString(),
                            Description = reader[3].ToString(),
                            ImageId = reader[4].ToString()
                        };
                        books.Add(book);
                    }
                }
                catch (Exception ex)
                {
                    reader.Close();
                    throw ex;
                }
                connection.Close();
            }
            return books;
        }
    }
}
