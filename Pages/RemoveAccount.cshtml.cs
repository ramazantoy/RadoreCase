using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadoreCaseRamazan.Pages;

public class RemoveAccount : PageModel
{
    public void OnGet()
    {
        
    }
    
    private readonly ILogger<RemoveAccount> _logger;

    public RemoveAccount(ILogger<RemoveAccount> logger)
    {
        _logger = logger;
    }
    public IActionResult OnPostRemoveAccount()
    {

        string hostDomainName = Request.Form["hostingDomainName"];
        
//        Console.WriteLine(hostDomainName);

        if (!RadoreApiController.RadoreApiController.Instance.IsAccountsContainThisHosting(hostDomainName))
        {
            TempData["Message"] = "Silme islemi basarisiz. boyle bir hesap yok.";
            return Page();
        }
        
        RadoreApiController.RadoreApiController.Instance.RemoveAccount(hostDomainName); 
       Thread.Sleep(300);

        if (!RadoreApiController.RadoreApiController.Instance.IsAccountsContainThisHosting(hostDomainName))
        {
            TempData["Message"] = "Basariyla Silindi";
        }
       
        
        return Page(); 
    }
}