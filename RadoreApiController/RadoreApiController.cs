using System.Text.Json.Nodes;
using Newtonsoft.Json;
using RadoreCaseRamazan.Models;
using RestSharp;

namespace RadoreCaseRamazan.RadoreApiController;

public class RadoreApiController
{
    private static readonly Lazy<RadoreApiController> _instance = new Lazy<RadoreApiController>(() => new RadoreApiController());

    public static RadoreApiController Instance => _instance.Value;
    
    private RestClient _client;
    

    private string? _addAccountEndPoint;

    public List<string>? AccountsList => GetAllAccountsToList("/account/get-all-accounts");

    private RadoreApiController()
    {
        _client = new RestClient("https://intestapi.radore.com/api");

        _addAccountEndPoint = "/account/create-account";
    }

 

    private string? GetAllAccountsToString(string endpoint)
    {
        var request = new RestRequest(endpoint);
        
        string? allMembers =_client.Execute(request).Content;
        
        Console.WriteLine(allMembers);
        
        return allMembers;
    }

    private List<string>? GetAllAccountsToList(string endpoint)
    {
        var request = new RestRequest(endpoint);
        
        string? allMembers = _client.Execute(request).Content;


       // Console.WriteLine(allMembers);
        return allMembers != null ? JsonConvert.DeserializeObject<List<string>>(allMembers) : null;
    }

    public bool IsAccountsContainThisHosting(string hostingName)
    {
        List<string>? _accountsList = GetAllAccountsToList("/account/get-all-accounts");
        
        if (_accountsList == null) return false;
        
        foreach (var accountHostingName in _accountsList)
        {
            if (accountHostingName.Equals(hostingName)) return true;
        }

        return false;
    }

    public async Task AddNewAccount(string hostingName, string hostingPackage)
    {

        string endpoint = "/account/create-account";
        var request = new RestRequest(endpoint,Method.Post);

        request.AddJsonBody(new
        {
            HostingDomainName = hostingName,
            HostingPackage = hostingPackage
        });

        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
           // Console.WriteLine("İstek hatalı" + response.StatusCode);
        }

        await RemoveAccount("test4");
        /*else
        {
          string jsonResponse = response.Content;
            
            Console.WriteLine(jsonResponse);
        }*/


    }

    public async Task RemoveAccount(string hostingName)
    {
        string endpoint = "/account/delete-account";
        var request = new RestRequest(endpoint,Method.Post);
        
        request.AddJsonBody(new
        {
            HostingDomainName = hostingName,
        });
        
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
           // Console.WriteLine("İstek hatalı" + response.StatusCode);
        }
    }

    public AccountData? GetAccountData(string hostingDomainName)
    {
        string endpoint = "/account/get-account?hostingDomainName="+hostingDomainName;
        var request = new RestRequest(endpoint,Method.Get);

        var response = _client.Execute(request);
        var content = response.Content;
        if (content != null)
        {
            var accountData = JsonConvert.DeserializeObject<AccountData>(content);
            return accountData;
        }

        return null;
    }




}