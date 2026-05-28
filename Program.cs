using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

class Program
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public virtual void Display()
        {
            Console.WriteLine($"  Name : {Name}");
            Console.WriteLine($"  Age  : {Age}");
            Console.WriteLine($"  City : {City}");
        }
    }

    public class AdminUser : User
    {
        public int AdminLevel { get; set; }
        public string Department { get; set; }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"  Level: {AdminLevel}");
            Console.WriteLine($"  Dept : {Department}");
        }
    }

    public class RegularUser : User
    {
        public string SubscriptionPlan { get; set; }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"  Plan : {SubscriptionPlan}");
        }
    }

    public class Moderator : User
    {
        public string ModeratedSection { get; set; }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"  Section: {ModeratedSection}");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("  JSON + XML Example — .NET / Newtonsoft  ");
        Console.WriteLine("===========================================\n");

        Task1_ReadXmlExample();
        Task2_AddEntriesToJson();
        Task3_DeserializeAllUsers();
        Task4_DeserializeUserTypes();
    }

    static void Task1_ReadXmlExample()
    {
        Console.WriteLine("--- TASK 1: XML Reader Example ---");
        string xmlPath = "users.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        XmlNodeList users = doc.GetElementsByTagName("User");
        foreach (XmlNode user in users)
        {
            Console.WriteLine($"  Name: {user["Name"].InnerText}, " +
                              $"Age: {user["Age"].InnerText}, " +
                              $"City: {user["City"].InnerText}");
        }
        Console.WriteLine();
    }

    static void Task2_AddEntriesToJson()
    {
        Console.WriteLine("--- TASK 2: Add New Entries to JSON ---");
        string filePath = "users.json";
        string json = File.ReadAllText(filePath);
        JArray usersArray = JArray.Parse(json);

        var newUser = new JObject
        {
            ["Name"] = "Maria Garcia",
            ["Age"] = 27,
            ["City"] = "Miami"
        };
        usersArray.Add(newUser);

        File.WriteAllText(filePath, usersArray.ToString(Formatting.Indented));
        Console.WriteLine("  Added: Maria Garcia, 27, Miami");
        Console.WriteLine($"  Total entries now: {usersArray.Count}");
        Console.WriteLine();
    }

    static void Task3_DeserializeAllUsers()
    {
        Console.WriteLine("--- TASK 3: Deserialize All Users (Loop) ---");
        string filePath = "users.json";
        string json = File.ReadAllText(filePath);
        List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

        int index = 1;
        foreach (User user in users)
        {
            Console.WriteLine($"  [{index++}]");
            user.Display();
        }
        Console.WriteLine();
    }

    static void Task4_DeserializeUserTypes()
    {
        Console.WriteLine("--- TASK 4: Deserialize user_types.json (Inheritance) ---");
        string filePath = "user_types.json";
        string json = File.ReadAllText(filePath);
        JArray array = JArray.Parse(json);

        int index = 1;
        foreach (JObject obj in array)
        {
            string userType = obj["UserType"]?.ToString();
            User user;

            switch (userType)
            {
                case "Admin":
                    user = obj.ToObject<AdminUser>();
                    Console.WriteLine($"  [{index++}] ADMIN");
                    break;
                case "RegularUser":
                    user = obj.ToObject<RegularUser>();
                    Console.WriteLine($"  [{index++}] REGULAR USER");
                    break;
                case "Moderator":
                    user = obj.ToObject<Moderator>();
                    Console.WriteLine($"  [{index++}] MODERATOR");
                    break;
                default:
                    user = obj.ToObject<User>();
                    Console.WriteLine($"  [{index++}] USER");
                    break;
            }

            user.Display();
        }

        Console.WriteLine();
        Console.WriteLine("===========================================");
        Console.WriteLine("  All tasks completed successfully.");
        Console.WriteLine("===========================================");
    }
}
