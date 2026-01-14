using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DeletegateEx
{
    // LogDel delegate will be a type safe pointer to a function that returns void (nothing)
    // LogDel delegate = new LogDel(); 
    class UserProfile
    {
        public string Name {get; set;}
        public string favHobby {get; set;}
        public char favChar {get; set;}
        public int favNum {get; set;}
    }
    delegate void LogDel(UserProfile profile);
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"Project path : {Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../.."))}");
            
            Logging classInstance = new Logging();

            LogDel del_1 = new LogDel(classInstance.LogTextToScreenInstanceMethod);   // del variable refers to LogtextToFile method(pointer)
            LogDel del_2 = new LogDel(classInstance.LogtextToFileInstanceMethod);
            
            UserProfile prof = initData();

            System.Console.WriteLine("Please enter your name : ");
            prof.Name = Console.ReadLine();
            
            del_1.Invoke(prof);
        }

        static void LogTextToScreen(UserProfile profile)
        {
            System.Console.WriteLine($"({DateTime.Now}) LOGS : ");
            System.Console.WriteLine($"Name : {profile.Name}");
            System.Console.WriteLine($"Hobby : {profile.favHobby}");
            System.Console.WriteLine($"Fav letter : {profile.favChar}");
            System.Console.WriteLine($"Fav number : {profile.favNum}");
        }
        static void LogtextToFile(UserProfile profile)
        {
            var projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../.."));
            using(StreamWriter sw = new StreamWriter(Path.Combine(projectRootPath, "log.txt")))
            {
                sw.WriteLine(profile.Name);                
                sw.WriteLine(profile.favHobby);                
                sw.WriteLine(profile.favNum);                
                sw.WriteLine(profile.favChar);                
            }
        }

        static UserProfile initData()
        {
            List<string> hobbies = new List<string>
            {
                "Swimming",
                "Dancing",
                "Music",
                "Running",
                "Reading"  
            };

            var random = new Random();
            return new UserProfile
            {
                Name = "",
                favHobby = hobbies[(int)random.Next(1, 6)],
                favChar = (char)random.Next('A', 'Z' + 1),
                favNum = (int)random.Next(1, 11)
            };
        }

        public class Logging
        {
            public void LogTextToScreenInstanceMethod(UserProfile profile)
            {
                System.Console.WriteLine($"({DateTime.Now}) LOGS : ");
                System.Console.WriteLine($"Name : {profile.Name}");
                System.Console.WriteLine($"Hobby : {profile.favHobby}");
                System.Console.WriteLine($"Fav letter : {profile.favChar}");
                System.Console.WriteLine($"Fav number : {profile.favNum}");
            }
            public void LogtextToFileInstanceMethod(UserProfile profile)
            {
                var projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../.."));
                using(StreamWriter sw = new StreamWriter(Path.Combine(projectRootPath, "log.txt")))
                {
                    sw.WriteLine(profile.Name);                
                    sw.WriteLine(profile.favHobby);                
                    sw.WriteLine(profile.favNum);                
                    sw.WriteLine(profile.favChar);                
                }
            }
        }
    }
}