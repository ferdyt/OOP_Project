using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class UserRepository : IUserRepository
    {
        public static string userPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/UserDB.json";

        public static void WriteSeance(string userLogin)
        {
            string seance = JsonSerializer.Serialize(userLogin);
            File.WriteAllText("C:/Users/Csgo2/Source/Repos/OOP_Project/OOP_Project/Databases/Current.json", seance);
        }

        public static string? GetSeance()
        {
            if (!File.Exists("C:/Users/Csgo2/Source/Repos/OOP_Project/OOP_Project/Databases/Current.json"))
            {
                return null;
            }

            string seance = File.ReadAllText("C:/Users/Csgo2/Source/Repos/OOP_Project/OOP_Project/Databases/Current.json");
            return JsonSerializer.Deserialize<string>(seance);
        }

        public static bool DelUser(string login)
        {

            if (!File.Exists(userPath)) return false;

            string existingData = File.ReadAllText(userPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            var userToRemove = users.Find(u => u.login == login);
            if (userToRemove == null) return false;

            users.Remove(userToRemove);

            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userPath, jsonString);

            return true;
        }

        public static bool AddUser(User user)
        {
            List<User> users = new List<User>();

            if (string.IsNullOrWhiteSpace(user.login) || user.login.Length < 2) return false;
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.MiddleName)) return false;
            if (user.Name.Length < 2 || user.LastName.Length < 2 || user.MiddleName.Length < 2) return false;
            if (user.password.Length < 4 || string.IsNullOrWhiteSpace(user.password)) return false;

            if (File.Exists(userPath))
            {
                string existingData = File.ReadAllText(userPath);
                if (!string.IsNullOrEmpty(existingData))
                {
                    try
                    {
                        users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Помилка при десеріалізації: {ex.Message}");
                        return false;
                    }
                }
            }

            if (users.Exists(u => u.login == user.login))
            {
                throw new Exception("Користувач вже існує");
            }

            users.Add(user);

            try
            {
                string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(userPath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при запису в файл: {ex.Message}");
                return false;
            }

            return true;
        }

        public static User? GetUserByLogin(string login)
        {
            string existingUsers = File.ReadAllText(userPath);
            List<User> users = new List<User>();

            if (!string.IsNullOrEmpty(existingUsers))
            {
                users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
            }

            var user = users.FirstOrDefault(u => u.login == login);

            return user;
        }

        public static List<User> GetUsers()
        {
            var users = new List<User>();

            if (!File.Exists(userPath)) return users;

            string existingData = File.ReadAllText(userPath);
            users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            return users;
        }
    }
}
