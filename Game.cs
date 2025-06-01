private PetManager petManager = new();
private string playerName = "Baran Parkın";
private string studentNumber = "2305041025";

// Baran Parkın, 2305041025, DGD208 final project

public class Game
{
    private bool _isRunning;
    
    public async Task GameLoop()
    {
        // Initialize the game
        Initialize();
        
        // Main game loop
        _isRunning = true;
        while (_isRunning)
        {
            // Display menu and get player input
            string userChoice = GetUserInput();
            
            // Process the player's choice
            await ProcessUserChoice(userChoice);
        }
        
        Console.WriteLine("Thanks for playing!");
    }
    
    private void Initialize()
    {
        // Use this to initialize the game
        _ = petManager.StartDecayLoop();
    }
    
    private string GetUserInput()
    {
        // Use this to display appropriate menu and get user inputs
        Console.WriteLine("\n--- Pet Simulator Menu ---");
        Console.WriteLine("1. Display Creator Info");
        Console.WriteLine("2. Adopt a Pet");
        Console.WriteLine("3. View Pet Stats");
        Console.WriteLine("4. Use an Item on a Pet");
        Console.WriteLine("0. Exit Game");
        Console.Write("Enter your choice: ");
        return Console.ReadLine()!;
    }
    
private async Task ProcessUserChoice(string choice)
{
    switch (choice)
    {
        case "1":
            Console.WriteLine($"Created by: {playerName}, {studentNumber}");
            break;

        case "2":
            await AdoptPet();
            break;

        case "3":
            ViewPets();
            break;

        case "4":
            await UseItemOnPet();
            break;

        case "0":
            petManager.StopDecayLoop();
            _isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

private async Task AdoptPet()
{
    Console.Write("Enter a name for your pet: ");
    string? name = Console.ReadLine();

    Console.WriteLine("Choose a pet type:");
    foreach (var type in Enum.GetValues(typeof(PetType)))
        Console.WriteLine($"- {type}");

    string? input = Console.ReadLine();
    if (Enum.TryParse<PetType>(input, out var petType))
    {
        var newPet = new Pet(name!, petType);
        petManager.AddPet(newPet);
        Console.WriteLine($"{newPet.Name} the {newPet.Type} has been adopted!");
    }
    else
    {
        Console.WriteLine("Invalid pet type.");
    }
}

private void ViewPets()
{
    if (!petManager.Pets.Any())
    {
        Console.WriteLine("No pets adopted yet.");
        return;
    }

    Console.WriteLine("\n--- Pet Stats ---");
    foreach (var pet in petManager.Pets)
    {
        Console.WriteLine(pet);
    }
}

private async Task UseItemOnPet()
{
    if (!petManager.Pets.Any())
    {
        Console.WriteLine("No pets available.");
        return;
    }

    Console.WriteLine("Choose a pet to use an item on:");
    for (int i = 0; i < petManager.Pets.Count; i++)
        Console.WriteLine($"{i + 1}. {petManager.Pets[i].Name}");

    if (!int.TryParse(Console.ReadLine(), out int petIndex) || petIndex < 1 || petIndex > petManager.Pets.Count)
    {
        Console.WriteLine("Invalid pet selection.");
        return;
    }

    var pet = petManager.Pets[petIndex - 1];
    var items = ItemDatabase.GetAll();

    Console.WriteLine("Choose an item:");
    for (int i = 0; i < items.Count; i++)
        Console.WriteLine($"{i + 1}. {items[i].Name} ({items[i].Type}, +{items[i].Value})");

    if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > items.Count)
    {
        Console.WriteLine("Invalid item selection.");
        return;
    }

    var item = items[itemIndex - 1];
    Console.WriteLine($"Using {item.Name} on {pet.Name}...");
    await item.UseAsync();
    pet.UseItem(item);
    Console.WriteLine($"{item.Name} used successfully.");
}
