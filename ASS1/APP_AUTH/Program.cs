Console.WriteLine(".NET Graph App-only Tutorial\n");

var settings = Settings.LoadSettings();

// Initialize Graph
InitializeGraph(settings);

int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Display access token");
    Console.WriteLine("2. List users");
    Console.WriteLine("3. List calender events");
    Console.WriteLine("4. Create a calender event");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    switch(choice)
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            // Display access token
            await DisplayAccessTokenAsync();
            break;
        case 2:
            // List users
            await ListUsersAsync();
            break;
        case 3:
            // List calender events
            await ListCalenderEvents();
            break;
        case 4:
            // Create a calender event
            await CreateCalenderEvent();
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}

void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForAppOnlyAuth(settings);
}

async Task DisplayAccessTokenAsync()
{
    try
    {
        var appOnlyToken = await GraphHelper.GetAppOnlyTokenAsync();
        Console.WriteLine($"App-only token: {appOnlyToken}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting app-only access token: {ex.Message}");
    }
}

async Task ListUsersAsync()
{
    try
    {
        var userPage = await GraphHelper.GetUsersAsync();

        if (userPage?.Value == null)
        {
            Console.WriteLine("No results returned.");
            return;
        }

        // Output each users's details
        foreach (var user in userPage.Value)
        {
            Console.WriteLine($"User: {user.DisplayName ?? "NO NAME"}");
            Console.WriteLine($"  ID: {user.Id}");
            Console.WriteLine($"  Email: {user.Mail ?? "NO EMAIL"}");
        }

        // If NextPageRequest is not null, there are more users
        // available on the server
        // Access the next page like:
        // var nextPageRequest = new UsersRequestBuilder(userPage.OdataNextLink, _appClient.RequestAdapter);
        // var nextPage = await nextPageRequest.GetAsync();
        var moreAvailable = !string.IsNullOrEmpty(userPage.OdataNextLink);

        Console.WriteLine($"\nMore users available? {moreAvailable}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting users: {ex.Message}");
    }
}

async Task ListCalenderEvents()
{
    try {

        Console.Write("Enter the user ID: ");
        var userID = Console.ReadLine();

        if (string.IsNullOrEmpty(userID)) {
            Console.WriteLine("Wrong ID");
            return;
        }

        var events = await GraphHelper.GetCalenderEvents(userID);

        if (events?.Value == null)
        {
            Console.WriteLine("No events returned.");
            return;
        }

        foreach (var ivent in events.Value)
        {
            Console.WriteLine($"Subject: {ivent.Subject}");
            Console.WriteLine($"Start: {ivent.Start.DateTime}");
            Console.WriteLine($"End: {ivent.End.DateTime}");
            Console.WriteLine($"Body: {ivent.BodyPreview}");
            Console.WriteLine($"Online: {ivent.IsOnlineMeeting}");
            Console.WriteLine($"Calender: {ivent.Calendar}");
        }

    } 
    catch (Exception ex) {
        Console.WriteLine($"Error recieving calender events: {ex.Message}");
    }
}
async Task CreateCalenderEvent()
{
    try {

        Console.Write("Enter the user ID: ");
        var userID = Console.ReadLine();

        if (string.IsNullOrEmpty(userID)) {
            Console.WriteLine("Wrong ID");
            return;
        }

        await GraphHelper.CreateCalenderEvent(
            "Testing",
            "Test of ASS1-APP-AUTH Client",
            "2023-05-05T14:00:00",
            "2023-05-05T15:00:00",
            "FH Joanneum",
            userID
        );

        Console.WriteLine("Created event");
    } catch (Exception ex) {
        Console.WriteLine($"Error creating event: {ex.Message}");
    }

}