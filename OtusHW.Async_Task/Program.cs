using System;
using System.Collections.Generic;
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
            string[] files = { "C:\\OtusHW.Source\\file1.txt", "C:\\OtusHW.Source\\file2.txt", "C:\\OtusHW.Source\\file3.txt" };

            Task<int>[] tasks = files.Select(file => FileReader(file)).ToArray();

            int[] results = await Task.WhenAll(tasks);

            int totalSpaces = results.Sum();

            Console.WriteLine($"Total spaces: {totalSpaces}");
        }
        static async Task<int> FileReader(string path)
        {
            int spaceCount = 0;
            using(StreamReader sr = new StreamReader(path))
            {
                string content = await sr.ReadToEndAsync();
                spaceCount += content.Count(c => c == ' ');
            }
            return spaceCount;
        }
        static async Task<int> FolderReader(string path)
        {
            string[] files = Directory.GetFiles(path);

            Task<int>[] tasks = files.Select(file => FileReader(file)).ToArray();

            int[] results = await Task.WhenAll(tasks);

            return results.Sum();
        }
    }
}
