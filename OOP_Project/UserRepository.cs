using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class UserRepository : UserRepositoryBase
    {
        public static void WriteSeance(string userLogin)
        {
            string seance = JsonSerializer.Serialize(userLogin);
            File.WriteAllText(Path.currentPath, seance);
        }

        public static string? GetSeance()
        {
            if (!File.Exists(Path.currentPath))
            {
                return null;
            }

            string seance = File.ReadAllText(Path.currentPath);
            return JsonSerializer.Deserialize<string>(seance);
        }

        public override bool DelUser(string login)
        {
            if (!File.Exists(Path.userPath)) return false;

            string existingData = File.ReadAllText(Path.userPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            Predicate<User> predicate = u => u.login == login;
            var userToRemove = users.Find(predicate);

            if (userToRemove == null) return false;

            users.Remove(userToRemove);

            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.userPath, jsonString);

            return true;
        }

        public override bool AddUser(User user)
        {
            List<User> users = new List<User>();

            if (string.IsNullOrWhiteSpace(user.login) || user.login.Length < 2) return false;
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.MiddleName)) return false;
            if (user.Name.Length < 2 || user.LastName.Length < 2 || user.MiddleName.Length < 2) return false;
            if (user.password.Length < 4 || string.IsNullOrWhiteSpace(user.password)) return false;

            if (File.Exists(Path.userPath))
            {
                string existingData = File.ReadAllText(Path.userPath);
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
                File.WriteAllText(Path.userPath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при запису в файл: {ex.Message}");
                return false;
            }

            return true;
        }

        public override User? GetUserByLogin(string login)
        {
            string existingUsers = File.ReadAllText(Path.userPath);
            List<User> users = new List<User>();

            if (!string.IsNullOrEmpty(existingUsers))
            {
                users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
            }

            Func<User, bool> predicate = u => u.login == login;
            var user = users.FirstOrDefault(predicate);

            return user;
        }

        public override List<User> GetUsers()
        {
            var users = new List<User>();

            if (!File.Exists(Path.userPath)) return users;

            string existingData = File.ReadAllText(Path.userPath);
            users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            return users;
        }
    }
}
