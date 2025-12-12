using System;

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
    private string _name;
    private int _level;
    private int _health;
    private double _experience;
    private CharacterClass _classType;
    private bool _isAlive;

    public string Description { get; set; } = "Новий персонаж";

    public bool IsMaxLevel => Level == 100;

    public string Name
    {
        get => _name;
        set
        {
            if (value.Length < 3 || value.Length > 15)
                throw new ArgumentException("Ім’я повинно містити 3–15 символів.");
            _name = value;
        }
    }

    public int Level
    {
        get => _level;
        private set
        {
            if (value < 1 || value > 100)
                throw new ArgumentOutOfRangeException("Рівень має бути у діапазоні 1–100.");
            _level = value;
        }
    }

    public int Health
    {
        get => _health;
        private set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException("Здоров’я має бути у діапазоні 0–100.");

            _health = value;
            CheckDeath();
        }
    }

    public double Experience
    {
        get => _experience;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Досвід не може бути від’ємним.");
            _experience = value;
        }
    }

    public bool IsAlive => _isAlive;

    public CharacterClass ClassType
    {
        get => _classType;
        private set => _classType = value;
    }

    public GameCharacter(string name, int level, CharacterClass classType)
    {
        Name = name;
        Level = level;
        ClassType = classType;

        Health = 100;
        Experience = 0;
        _isAlive = true;
    }

    // методи
    private void CheckDeath()
    {
        _isAlive = _health > 0;
    }

    private void ClampHealth()
    {
        if (_health < 0) _health = 0;
        if (_health > 100) _health = 100;
    }

    public void TakeDamage(int dmg)
    {
        if (dmg < 0)
            throw new ArgumentException("Шкода не може бути від’ємною!");

        Health -= dmg;
        ClampHealth();
        CheckDeath();
    }

    public void Heal(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Лікування не може бути від’ємним!");

        Health += amount;
        ClampHealth();
    }

    public void GainExperience(double xp)
    {
        if (xp < 0)
            throw new ArgumentException("XP не може бути від’ємним!");

        Experience += xp;
    }

    public void ShowStats()
    {
        Console.WriteLine("\n--- Характеристики персонажа ---");
        Console.WriteLine($"Ім’я: {Name}");
        Console.WriteLine($"Клас: {ClassType}");
        Console.WriteLine($"Рівень: {Level}");
        Console.WriteLine($"Здоров’я: {Health}");
        Console.WriteLine($"Досвід: {Experience}");
        Console.WriteLine($"Статус: {(IsAlive ? "Живий" : "Мертвий")}");
        Console.WriteLine($"Опис: {Description}");
        Console.WriteLine($"Досяг макс. рівня: {(IsMaxLevel ? "Так" : "Ні")}");
        Console.WriteLine("----------------------------------\n");
    }
}
