using System;
using System.IO;

namespace questBot
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=', 2,
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;
                Console.WriteLine($"{parts[0]} has value\n{parts[1]}");
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}