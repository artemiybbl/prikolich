using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class ContactDatabase : IDatabaseConnection
{
    private readonly string _filePath;

    public ContactDatabase(string filename)
    {
        _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filename);
        Console.WriteLine($"Путь к файлу: {_filePath}");

        if (_filePath.EndsWith(".db") && !File.Exists(_filePath))
        {
            SQLiteConnection.CreateFile(_filePath);
            using var connection = new SQLiteConnection($"Data Source={_filePath};Version=3;");
            connection.Open();
            using var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Contacts (Name TEXT, Surname TEXT, Phone TEXT, Email TEXT);", connection);
            command.ExecuteNonQuery();
        }
    }

    public void SaveContacts(List<Contact> contacts)
    {
        if (_filePath.EndsWith(".db")) // SQLite
        {
            using var connection = new SQLiteConnection($"Data Source={_filePath};Version=3;");
            connection.Open();

            // Проверка существования таблицы
            using var checkTableCommand = new SQLiteCommand("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Contacts';", connection);
            var tableExists = Convert.ToInt32(checkTableCommand.ExecuteScalar()) > 0;

            if (tableExists)
            {
                using (var deleteCommand = new SQLiteCommand("DELETE FROM Contacts;", connection))
                {
                    deleteCommand.ExecuteNonQuery();
                }
            }
            else
            {
                using (var createCommand = new SQLiteCommand("CREATE TABLE Contacts (Name TEXT, Surname TEXT, Phone TEXT, Email TEXT);", connection))
                {
                    createCommand.ExecuteNonQuery();
                }
            }

            foreach (var contact in contacts)
            {
                using var insertCommand = new SQLiteCommand("INSERT INTO Contacts (Name, Surname, Phone, Email) VALUES (@Name, @Surname, @Phone, @Email);", connection);
                insertCommand.Parameters.AddWithValue("@Name", contact.Name);
                insertCommand.Parameters.AddWithValue("@Surname", contact.Surname);
                insertCommand.Parameters.AddWithValue("@Phone", contact.Phone);
                insertCommand.Parameters.AddWithValue("@Email", contact.Email);
                insertCommand.ExecuteNonQuery();
            }
        }
        else if (_filePath.EndsWith(".json")) // JSON
        {
            var jsonData = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
        else if (_filePath.EndsWith(".xml")) // XML
        {
            var formatter = new XmlSerializer(typeof(List<Contact>));
            using var fs = new FileStream(_filePath, FileMode.Create);
            formatter.Serialize(fs, contacts);
        }
        else
        {
            Console.WriteLine("Неподдерживаемый формат файла.");
        }
    }


    public List<Contact> LoadContacts(string filePath)
    {
        List<Contact> contacts = new List<Contact>();

        if (Path.GetExtension(_filePath) == ".db") // SQLite
        {
            string tableName = "Contacts"; // Имя таблицы для SQLite

            if (File.Exists(filePath))
            {
                string connectionString = $"Data Source={filePath};Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM {tableName};";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contact contact = new Contact
                                {
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Email = reader["Email"].ToString()
                                };

                                contacts.Add(contact);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        else if (Path.GetExtension(filePath) == ".xml") // XML
        {
            if (File.Exists(filePath))
            {
                var formatter = new XmlSerializer(typeof(List<Contact>));
                using var fs = new FileStream(filePath, FileMode.Open);
                contacts = (List<Contact>)formatter.Deserialize(fs);
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        else
        {
            Console.WriteLine("Неподдерживаемый формат файла.");
        }

        return contacts;
    }
}

