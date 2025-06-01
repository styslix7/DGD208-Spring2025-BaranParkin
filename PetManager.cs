public class PetManager
{
    private readonly List<Pet> pets = new();
    private bool isRunning = false;

    public IReadOnlyList<Pet> Pets => pets;

    public void AddPet(Pet pet)
    {
        pet.PetDied += OnPetDied;
        pets.Add(pet);
    }

    public void RemovePet(Pet pet)
    {
        pets.Remove(pet);
    }

    private void OnPetDied(Pet pet)
    {
        Console.WriteLine($"{pet.Name} has died 😢");
        RemovePet(pet);
    }

    public async Task StartDecayLoop()
    {
        isRunning = true;
        while (isRunning)
        {
            foreach (var pet in pets.ToList()) pet.DecreaseStats();
            await Task.Delay(5000);
        }
    }

    public void StopDecayLoop()
    {
        isRunning = false;
    }
}
