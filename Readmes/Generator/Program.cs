﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Readme.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================Generate Extensions Readmes Start========================");
            GenerateExtensionsReadmes();
            Console.WriteLine("========================Generate Extensions Readmes End========================");
            Console.WriteLine("========================Generate Heleper Readmes Start========================");
            GenerateHeleperReadmes();
            Console.WriteLine("========================Generate Heleper Readmes End========================");
        }

        private static void GenerateExtensionsReadmes()
        {
            var classes = Assembly.Load("LBON.Extensions").GetTypes().Where(a => a.Name.EndsWith("Extensions")).ToList();
            foreach (var item in classes)
            {
                var dirPath = "E:\\Codes\\LBON\\Readmes\\Extensions";
                var filePath = Path.Combine(dirPath, $"{item.Name.ToUpper()}_README.md");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    string content = $"# {item.Name}";
                    var methods = item.GetMethods().Where(a => a.IsPublic && a.IsStatic).ToList();
                    foreach (var method in methods)
                    {
                        content += @$"
- <code>{method.Name}</code> {method.GetCustomAttribute<DescriptionAttribute>()?.Description}";
                    }
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    fileStream.Write(data, 0, data.Length);
                }
                Console.WriteLine($"已生成 {item.Name.ToUpper()}_README.md");
            }
        }

        private static void GenerateHeleperReadmes()
        {
            var classes = Assembly.Load("LBON.Helper").GetTypes().Where(a => a.Name.EndsWith("Helper")).ToList();
            foreach (var item in classes)
            {
                var dirPath = "E:\\Codes\\LBON\\Readmes\\Helper";
                var filePath = Path.Combine(dirPath, $"{item.Name.ToUpper()}_README.md");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    string content = $"# {item.Name}";
                    var methods = item.GetMethods().Where(a => a.IsPublic && a.IsStatic).ToList();
                    foreach (var method in methods)
                    {
                        content += @$"
- <code>{method.Name}</code>";
                    }
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    fileStream.Write(data, 0, data.Length);
                }
                Console.WriteLine($"已生成 {item.Name.ToUpper()}_README.md");
            }
        }
    }
}
