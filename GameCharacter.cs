using System;

namespace OOP_Lab2
{
    public enum CharacterClass
    {
        Warrior,
        Mage,
        Archer,
        Healer,
        Assassin
    }

    public class GameCharacter
    {
        // private-поля
        private string _name;
        private int _level;
        private int _health;
        private double _experience;
        private CharacterClass _classType;

        // 3. Автовластивість з дефолтним значенням
        public string Description { get; set; } = "Новий персонаж";

        // 2. Властивості з валідацією
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length < 3 || value.Length > 15)
                    throw new ArgumentException("Імʼя має бути 3–15 символів");
                _name = value;
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                if (value < 1 || value > 100)
                    throw new ArgumentOutOfRangeException("Рівень 1–100");
                _level = value;
            }
        }

        public int Health
        {
            get => _health;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("Здоровʼя 0–100");
                _health = value;
                CheckDeath();
            }
        }

        public double Experience
        {
            get => _experience;
            set
            {
                if (value < 0)
                    throw new ArgumentException("XP не може бути відʼємним");
                _experience = value;
            }
        }

        // 5. Різні рівні доступу get / set
        public CharacterClass ClassType
        {
            get => _classType;
            private set => _classType = value;
        }

        // 4. Обчислювальні властивості
        public bool IsAlive => Health > 0;
        public bool IsMaxLevel => Level == 100;

        // Конструктор
        public GameCharacter(string name, int level, int health, double experience, CharacterClass classType)
        {
            Name = name;
            Level = level;
            Health = health;
            Experience = experience;
            ClassType = classType;
        }

        // 6. Private-метод
        private void CheckDeath()
        {
            if (_health == 0)
                Console.WriteLine($"⚠ {_name} загинув!");
        }

        // Поведінка
        public void TakeDamage(int dmg)
        {
            if (dmg < 0) return;
            Health = Math.Max(0, Health - dmg);
        }

        public void Heal(int amount)
        {
            if (amount < 0) return;
            Health = Math.Min(100, Health + amount);
        }

        public void GainExperience(double xp)
        {
            if (xp < 0) return;
            Experience += xp;
        }

        public void ShowStats()
        {
            Console.WriteLine(
                $"Імʼя: {Name}, Клас: {ClassType}, Рівень: {Level}, HP: {Health}, XP: {Experience}, Alive: {IsAlive}");
        }
    }
}
