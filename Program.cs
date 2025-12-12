using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        List<GameCharacter> characters = new List<GameCharacter>();

        while (true)
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1 - Додати об'єкт");
            Console.WriteLine("2 - Переглянути всі об'єкти");
            Console.WriteLine("3 - Знайти об'єкт");
            Console.WriteLine("4 - Продемонструвати поведінку");
            Console.WriteLine("5 - Видалити об'єкт");
            Console.WriteLine("0 - Вийти");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCharacter(characters);
                    break;

                case "2":
                    ShowAll(characters);
                    break;

                case "3":
                    FindCharacter(characters);
                    break;

                case "4":
                    Demonstrate(characters);
                    break;

                case "5":
                    DeleteCharacter(characters);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("❌ Невірний вибір!");
                    break;
            }
        }
    }

    // ---------- MENU ACTIONS ----------

    static void AddCharacter(List<GameCharacter> list)
    {
        Console.WriteLine("\nСтворення нового персонажа");

        string name;
        while (true)
        {
            Console.Write("Ім’я (3–15): ");
            name = Console.ReadLine();

            try
            {
                if (name.Length >= 3 && name.Length <= 15) break;
                throw new Exception();
            }
            catch
            {
                Console.WriteLine("Помилка: ім’я повинно містити 3–15 символів.");
            }
        }

        int level;
        while (true)
        {
            Console.Write("Рівень (1–100): ");
            if (int.TryParse(Console.ReadLine(), out level) && level >= 1 && level <= 100)
                break;
            Console.WriteLine("Помилка!");
        }

        Console.WriteLine("\nОберіть клас персонажа:");

        int index = 0;
        foreach (var c in Enum.GetNames(typeof(CharacterClass)))
        {
            Console.WriteLine($"{index} - {c}");
            index++;
        }

        int classIndex;
        Console.Write("Ваш вибір: ");

        while (!int.TryParse(Console.ReadLine(), out classIndex) ||
               classIndex < 0 ||
               classIndex >= Enum.GetValues(typeof(CharacterClass)).Length)
        {
            Console.WriteLine("❌ Помилка! Введіть номер класу зі списку:");
        }

        CharacterClass classType = (CharacterClass)classIndex;

        GameCharacter newChar = new GameCharacter(name, level, classType);
        list.Add(newChar);

        Console.WriteLine("Персонажа додано!");
    }

    static void ShowAll(List<GameCharacter> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        foreach (var ch in list)
            ch.ShowStats();
    }

    static void FindCharacter(List<GameCharacter> list)
    {
        Console.Write("Введіть ім’я: ");
        string name = Console.ReadLine();

        foreach (var ch in list)
        {
            if (ch.Name == name)
            {
                ch.ShowStats();
                return;
            }
        }

        Console.WriteLine("Не знайдено.");
    }

    static void Demonstrate(List<GameCharacter> list)
    {
        Console.Write("Ім’я персонажа: ");
        string name = Console.ReadLine();

        var ch = list.Find(c => c.Name == name);

        if (ch == null)
        {
            Console.WriteLine("Не знайдено.");
            return;
        }

        Console.WriteLine("1 - Завдати шкоди");
        Console.WriteLine("2 - Зцілити");
        Console.WriteLine("3 - Додати XP");

        int act;
        if (!int.TryParse(Console.ReadLine(), out act))
        {
            Console.WriteLine("Помилка!");
            return;
        }

        switch (act)
        {
            case 1:
                Console.Write("Шкода: ");
                if (int.TryParse(Console.ReadLine(), out int dmg) && dmg >= 0 && dmg <= 100)
                    ch.TakeDamage(dmg);
                else Console.WriteLine("Некоректно!");
                break;

            case 2:
                Console.Write("Зцілити на: ");
                if (int.TryParse(Console.ReadLine(), out int heal) && heal >= 0 && heal <= 100)
                    ch.Heal(heal);
                else Console.WriteLine("Некоректно!");
                break;

            case 3:
                Console.Write("XP: ");
                if (double.TryParse(Console.ReadLine(), out double xp) && xp >= 0)
                    ch.GainExperience(xp);
                else Console.WriteLine("Некоректно!");
                break;

            default:
                Console.WriteLine("Помилка!");
                break;
        }
    }

    static void DeleteCharacter(List<GameCharacter> list)
    {
        Console.Write("Ім’я: ");
        string name = Console.ReadLine();

        var ch = list.Find(c => c.Name == name);

        if (ch == null)
        {
            Console.WriteLine("Не знайдено.");
            return;
        }

        list.Remove(ch);
        Console.WriteLine("Персонажа видалено.");
    }
}
