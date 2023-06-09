using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

class GraphHelper
{
    // Settings object
    private static Settings? _settings;
    // App-ony auth token credential
    private static ClientSecretCredential? _clientSecretCredential;
    // Client configured with app-only authentication
    private static GraphServiceClient? _appClient;

    public static void InitializeGraphForAppOnlyAuth(Settings settings)
    {
        _settings = settings;

        // Ensure settings isn't null
        _ = settings ??
            throw new System.NullReferenceException("Settings cannot be null");

        _settings = settings;

        if (_clientSecretCredential == null)
        {
            _clientSecretCredential = new ClientSecretCredential(
                _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
        }

        if (_appClient == null)
        {
            _appClient = new GraphServiceClient(_clientSecretCredential,
                // Use the default scope, which will request the scopes
                // configured on the app registration
                new[] { "https://graph.microsoft.com/.default" });
        }
    }
    public static async Task<string> GetAppOnlyTokenAsync()
    {
        // Ensure credential isn't null
        _ = _clientSecretCredential ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

        // Request token with given scopes
        var context = new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" });
        var response = await _clientSecretCredential.GetTokenAsync(context);
        return response.Token;
    }
    public static Task<UserCollectionResponse?> GetUsersAsync()
    {
        // Ensure client isn't null
        _ = _appClient ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

        return _appClient.Users.GetAsync((config) =>
        {
            // Only request specific properties
            config.QueryParameters.Select = new[] { "displayName", "id", "mail" };
            // Get at most 25 results
            config.QueryParameters.Top = 25;
            // Sort by display name
            config.QueryParameters.Orderby = new[] { "displayName" };
        });
    }
    public static Task<EventCollectionResponse?> GetCalenderEvents(string userID)
    {
        _ = _appClient ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");
        
        return _appClient.Users[userID].Calendar.Events.GetAsync();

    }
    public async static Task CreateCalenderEvent(String subject, String content, String StartTime, String EndTime, String Location, String userID)
    {
        _ = _appClient ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");
        
        var requestBody = new Event {

            Subject = subject,
            Body = new ItemBody {
		        ContentType = BodyType.Html,
		        Content = content,
            },
            Start = new DateTimeTimeZone {
                DateTime = StartTime,
                TimeZone = "Europe/Berlin"
            },
            End = new DateTimeTimeZone {
                DateTime = EndTime,
                TimeZone = "Europe/Berlin"
            },
            Location = new Location {
                DisplayName = Location,
            },
            Attendees = new List<Attendee> {
		        new Attendee {
			        EmailAddress = new EmailAddress {
				        Address = "stefan.joebstl@edu.fh-joanneum.at",
				        Name = "Stefan Joebstl",
			        },
			        Type = AttendeeType.Required,
		        },
	        }
        };
        var result = await _appClient.Users[userID].Calendar.Events.PostAsync(requestBody);
    }
}