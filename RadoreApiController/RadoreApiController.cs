using Newtonsoft.Json;
using RestSharp;

namespace RadoreCaseRamazan.RadoreApiController;

public class RadoreApiController
{
    private static readonly Lazy<RadoreApiController> _instance = new Lazy<RadoreApiController>(() => new RadoreApiController());

    public static RadoreApiController Instance => _instance.Value;
    
    private RestClient _client;

 

    private RadoreApiController()
    {
        _client = new RestClient("https://intestapi.radore.com/api");
    
        // Bağlantı ayarları veya diğer ilgili kodlar buraya gelebilir
    }

    public string? GetAllAccounts(string endpoint)
    {
        var request = new RestRequest(endpoint);
        
        string? allMembers = _client.Execute(request).Content;
        
        Console.WriteLine(allMembers);
        
        return allMembers;
    }

    public List<string>? GetAllAccountsToList(string endpoint)
    {
        var request = new RestRequest(endpoint);
        
        string? allMembers = _client.Execute(request).Content;


        return allMembers != null ? JsonConvert.DeserializeObject<List<string>>(allMembers) : null;
    }



}