using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            ShowUsage();
            return;
        }

        string sourceDirectory = args[0];
        string destinationDirectory = args[1];

        if (!Directory.Exists(sourceDirectory))
        {
            Console.WriteLine("Помилка: Вихiдний каталог не iснує.");
            return;
        }

        if (!Directory.Exists(destinationDirectory))
        {
            Console.WriteLine("Помилка: Каталог призначення не iснує.");
            return;
        }

        string[] files = Directory.GetFiles(sourceDirectory);

        if (files.Length == 0)
        {
            Console.WriteLine("Помилка: Вихiдний каталог не мiстить файлiв.");
            return;
        }

        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destinationPath = Path.Combine(destinationDirectory, fileName);
            FileAttributes attributes = File.GetAttributes(file);

            if (attributes.HasFlag(FileAttributes.Hidden))
            {
                Console.WriteLine("Пропускаю прихований файл: " + fileName);
                continue;
            }

            if (attributes.HasFlag(FileAttributes.ReadOnly))
            {
                Console.WriteLine("Пропускаю файл з обмеженнями на запис: " + fileName);
                continue;
            }

            if (attributes.HasFlag(FileAttributes.Archive))
            {
                Console.WriteLine("Копiюю файл: " + fileName);
                File.Copy(file, destinationPath, true);
            }
        }

        Console.WriteLine("Копiювання завершено успiшно.");
    }

    static void ShowUsage()
    {
        Console.WriteLine("Використання:");
        Console.WriteLine("  FileCopyUtility <вихiдний каталог> <каталог призначення>");
    }
}
