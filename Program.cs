using System;
using System.Collections.Generic;

namespace OOP_Lab2
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Введіть максимальну кількість персонажів: ");
            int maxCount;

            while (!int.TryParse(Console.ReadLine(), out maxCount) || maxCount <= 0)
            {
                Console.WriteLine("Помилка! Введіть число > 0:");
            }

            List<GameCharacter> characters = new List<GameCharacter>();
            int choice;

            do
            {
                Console.WriteLine("\n===== МЕНЮ =====");
                Console.WriteLine("1 - Додати об'єкт");
                Console.WriteLine("2 - Переглянути всі об'єкти");
                Console.WriteLine("3 - Знайти об'єкт");
                Console.WriteLine("4 - Продемонструвати поведінку");
                Console.WriteLine("5 - Видалити об'єкт");
                Console.WriteLine("0 - Вийти");
                Console.Write("Ваш вибір: ");

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        AddCharacter(characters, maxCount);
                        break;
                    case 2:
                        ShowAll(characters);
                        break;
                    case 3:
                        FindCharacter(characters);
                        break;
                    case 4:
                        DemonstrateBehavior(characters);
                        break;
                    case 5:
                        DeleteCharacter(characters);
                        break;
                    case 0:
                        Console.WriteLine("Вихід з програми...");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

            } while (choice != 0);
        }

        // 1. Додавання персонажа
        static void AddCharacter(List<GameCharacter> list, int max)
        {
            if (list.Count >= max)
            {
                Console.WriteLine("Досягнуто максимальної кількості персонажів!");
                return;
            }

            try
            {
                Console.Write("Ім’я (3–15): ");
                string name = Console.ReadLine();

                Console.Write("Рівень (1–100): ");
                int level = int.Parse(Console.ReadLine());

                Console.Write("Здоров’я (0–100): ");
                int health = int.Parse(Console.ReadLine());

                Console.Write("Досвід: ");
                double xp = double.Parse(Console.ReadLine());

                Console.WriteLine("Оберіть клас:");
                int i = 0;
                foreach (var cls in Enum.GetNames(typeof(CharacterClass)))
                {
                    Console.WriteLine($"{i} - {cls}");
                    i++;
                }

                int classIndex = int.Parse(Console.ReadLine());

                GameCharacter ch = new GameCharacter(
                    name,
                    level,
                    health,
                    xp,
                    (CharacterClass)classIndex
                );

                list.Add(ch);
                Console.WriteLine("Персонажа додано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка введення: {ex.Message}");
            }
        }

        // 2. Перегляд усіх
        static void ShowAll(List<GameCharacter> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список порожній!");
                return;
            }

            Console.WriteLine("\nНомер| Імʼя        | Клас        | Рівень | HP  | XP");
            Console.WriteLine("-----------------------------------------------------");

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(
                    $" {i + 1}| {list[i].Name,-10} | {list[i].ClassType,-10} | {list[i].Level,6} | {list[i].Health,3} | {list[i].Experience}");
            }
        }

        // 3. Пошук
        static void FindCharacter(List<GameCharacter> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список порожній!");
                return;
            }

            Console.WriteLine("Шукати за:");
            Console.WriteLine("1 - Імʼям");
            Console.WriteLine("2 - Класом");
            Console.Write("Ваш вибір: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Невірний вибір!");
                return;
            }

            List<GameCharacter> found = new List<GameCharacter>();

            if (option == 1)
            {
                Console.Write("Введіть імʼя: ");
                string name = Console.ReadLine();
                found = list.FindAll(c => c.Name == name);
            }
            else if (option == 2)
            {
                Console.WriteLine("Оберіть клас:");
                int i = 0;
                foreach (var cls in Enum.GetNames(typeof(CharacterClass)))
                {
                    Console.WriteLine($"{i} - {cls}");
                    i++;
                }

                int clsIndex;
                if (!int.TryParse(Console.ReadLine(), out clsIndex) ||
                    clsIndex < 0 ||
                    clsIndex >= Enum.GetValues(typeof(CharacterClass)).Length)
                {
                    Console.WriteLine("Невірний клас!");
                    return;
                }

                found = list.FindAll(c => c.ClassType == (CharacterClass)clsIndex);
            }
            else
            {
                Console.WriteLine("Невірний варіант!");
                return;
            }

            if (found.Count == 0)
            {
                Console.WriteLine("Нічого не знайдено!");
                return;
            }

            Console.WriteLine("\nНомер| Імʼя        | Клас        | Рівень | HP  | XP");
            Console.WriteLine("-----------------------------------------------------");

            for (int j = 0; j < found.Count; j++)
            {
                Console.WriteLine(
                    $" {j + 1}| {found[j].Name,-10} | {found[j].ClassType,-10} | {found[j].Level,6} | {found[j].Health,3} | {found[j].Experience}");
            }
        }

        // 4. Демонстрація поведінки
        static void DemonstrateBehavior(List<GameCharacter> list)
        {
            Console.Write("Ім’я персонажа: ");
            string name = Console.ReadLine();

            var ch = list.Find(c => c.Name == name);

            if (ch == null)
            {
                Console.WriteLine("Персонажа не знайдено!");
                return;
            }

            Console.WriteLine("1 - Завдати шкоди");
            Console.WriteLine("2 - Зцілити");
            Console.WriteLine("3 - Додати XP");

            int act = int.Parse(Console.ReadLine());

            switch (act)
            {
                case 1:
                    Console.Write("Шкода: ");
                    ch.TakeDamage(int.Parse(Console.ReadLine()));
                    break;
                case 2:
                    Console.Write("Зцілити на: ");
                    ch.Heal(int.Parse(Console.ReadLine()));
                    break;
                case 3:
                    Console.Write("XP: ");
                    ch.GainExperience(double.Parse(Console.ReadLine()));
                    break;
                default:
                    Console.WriteLine("Невірна дія!");
                    break;
            }
        }

        // 5. Видалення
        static void DeleteCharacter(List<GameCharacter> list)
        {
            Console.Write("Ім’я персонажа: ");
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
}
