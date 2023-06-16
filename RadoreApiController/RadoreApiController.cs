using Newtonsoft.Json;
using RestSharp;

namespace RadoreCaseRamazan.RadoreApiController;

public class RadoreApiController
{
    private static readonly Lazy<RadoreApiController> _instance = new Lazy<RadoreApiController>(() => new RadoreApiController());

    public static RadoreApiController Instance => _instance.Value;
    
    private RestClient _client;

    private List<string>? _accountsList;

    public List<string>? AccountsList
    {
        get
        {
            return _accountsList;
        }
    }

    private RadoreApiController()
    {
        _client = new RestClient("https://intestapi.radore.com/api");
        
        UpdateAccountsList();
    }

    private void UpdateAccountsList()
    {
        _accountsList = GetAllAccountsToList("/account/get-all-accounts");
    }

    private string? GetAllAccountsToString(string endpoint)
    {
        var request = new RestRequest(endpoint);
        
        string? allMembers = _client.Execute(request).Content;
        
        Console.WriteLine(allMembers);
        
        return allMembers;
    }

    private List<string>? GetAllAccountsToList(string endpoint)
    {
        var request = new RestRequest(endpoint);
        
        string? allMembers = _client.Execute(request).Content;


        return allMembers != null ? JsonConvert.DeserializeObject<List<string>>(allMembers) : null;
    }
    



}