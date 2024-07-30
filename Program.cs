using System;
using System.Data.SQLite;

namespace ContactManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=contacts.db;Version=3;";
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            CreateDatabase(connection);
            AddContact(connection, "João", "joao@example.com", "912345678");
            AddContact(connection, "Maria", "maria@example.com", "987654321");
            ListContacts(connection);

            Console.WriteLine("Operação concluída.");
        }

        static void CreateDatabase(SQLiteConnection connection)
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Contacts (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    Phone TEXT NOT NULL
                );";

            using var command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        static void AddContact(SQLiteConnection connection, string name, string email, string phone)
        {
            string insertQuery = "INSERT INTO Contacts (Name, Email, Phone) VALUES (@Name, @Email, @Phone);";

            using var command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Phone", phone);
            command.ExecuteNonQuery();
        }

        static void ListContacts(SQLiteConnection connection)
        {
            string selectQuery = "SELECT * FROM Contacts;";

            using var command = new SQLiteCommand(selectQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Email: {reader["Email"]}, Phone: {reader["Phone"]}");
            }
        }
    }
}
