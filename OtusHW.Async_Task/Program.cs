using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtusHW.Async_Task
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            string folderPath = ("C:\\OtusHW.Source\\");

            sw.Start();
            List<Task<int>> tasks = new List<Task<int>>();

            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                tasks.Add(Task.Run(() => CountSpaces(filePath)));
            }

            int[] results = await Task.WhenAll(tasks);
            sw.Stop();

            Console.WriteLine($"Total spaces: {results.Sum()}.");
            Console.WriteLine($"Time: {sw.Elapsed}");
        }
        static async Task<int> CountSpaces(string path)
        {
            int spaceCount = 0;
            Console.WriteLine($"Чтение файла");
            using (StreamReader sr = new StreamReader(path))
            {
                string content = await sr.ReadToEndAsync();
                spaceCount += content.Count(c => c == ' ');
            }
            return spaceCount;
        }
        static async Task<int> CountFolderSpaces(string path)
        {
            List<Task<int>> tasks = new List<Task<int>>();

            foreach (string filePath in Directory.GetFiles(path))
            {
                tasks.Add(Task.Run(() => CountSpaces(filePath)));
            }

            int[] results = await Task.WhenAll(tasks);

            return results.Sum();
        }
    }
}
