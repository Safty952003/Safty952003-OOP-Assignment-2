namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }
    protected Enemy(string modelData)
    {
        _modelData = modelData;
    }

    public abstract Enemy Clone();

}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(string modelData) : base(modelData)
    {
    }

    public override Enemy Clone()
    {
        Orc copy = new Orc(ModelId);

        copy.Name = Name;
        copy.Health = Health;

        copy.Weapon = new Weapon
        {
            Name = Weapon.Name,
            Damage = Weapon.Damage
        };

        copy.Abilities = new List<string>(Abilities);

        return copy;
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }
    private Elf(string modelData) : base(modelData)
    {
    }
    public override Enemy Clone()
    {
        Elf copy = new Elf(ModelId);

        copy.Name = Name;
        copy.Health = Health;

        copy.Weapon = new Weapon
        {
            Name = Weapon.Name,
            Damage = Weapon.Damage
        };

        copy.Abilities = new List<string>(Abilities);

        return copy;
    }
}

public class EnemyRegistry
{
    private Dictionary<string, Enemy> _prototypes = new();

    public void Register(string name, Enemy prototype)
    {
        _prototypes[name] = prototype;
    }

    public Enemy Create(string name)
    {
        if (!_prototypes.ContainsKey(name))
            throw new KeyNotFoundException("Prototype not found.");

        return _prototypes[name].Clone();
    }
}