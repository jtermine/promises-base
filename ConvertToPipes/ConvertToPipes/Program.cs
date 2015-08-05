using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToPipes
{
    class Program
    {
        private static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"C:\Users\Administrator\Desktop\x-journals");

            foreach (var fileName in files)
            {
                int counter = 0;
                string line;

                var newFileName = $"je-{Path.GetFileName(fileName)}";

                using (var newFile = File.CreateText(Path.Combine(@"C:\Users\Administrator\Desktop\y-journals", newFileName)))
                {
                    var file = new StreamReader(fileName);

                    while ((line = file.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            var substring = line.Substring(1, line.Length - 2);
                            substring = substring.Replace("\",\"", "|");
                            newFile.WriteLine(substring);
                        }
                        
                        counter++;
                    }

                    file.Close();

                    newFile.Flush();
                    newFile.Close();
                }

                // Read the file and display it line by line.
            }

            // Suspend the screen.
            Console.ReadLine();
        }

    }
}
