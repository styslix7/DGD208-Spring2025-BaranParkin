public class Pet
{
    public string Name { get; }
    public PetType Type { get; }
    public int Hunger { get; private set; } = 50;
    public int Sleep { get; private set; } = 50;
    public int Fun { get; private set; } = 50;

    public event Action<Pet>? PetDied;

    public Pet(string name, PetType type)
    {
        Name = name;
        Type = type;
    }

    public void DecreaseStats()
    {
        Hunger = Math.Max(0, Hunger - 1);
        Sleep = Math.Max(0, Sleep - 1);
        Fun = Math.Max(0, Fun - 1);

        if (Hunger == 0 || Sleep == 0 || Fun == 0)
        {
            PetDied?.Invoke(this);
        }
    }

    public void UseItem(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Food:
                Hunger = Math.Min(100, Hunger + item.Value);
                break;
            case ItemType.Bed:
                Sleep = Math.Min(100, Sleep + item.Value);
                break;
            case ItemType.Toy:
                Fun = Math.Min(100, Fun + item.Value);
                break;
        }
    }

    public override string ToString()
    {
        return $"{Name} ({Type}) - Hunger: {Hunger}, Sleep: {Sleep}, Fun: {Fun}";
    }
}
