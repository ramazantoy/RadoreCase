using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadoreCaseRamazan.Pages;

public class AccountSettings : PageModel
{
    private readonly ILogger<AccountSettings> _logger;

    public AccountSettings(ILogger<AccountSettings> logger)
    {
        _logger = logger;
    }
    
    public IActionResult OnPostCreateAccount()
    {
        
        string hostDomainName = Request.Form["hostingDomainName"];
        string hostingPackage = Request.Form["hostingPackage"];
        
        if (RadoreApiController.RadoreApiController.Instance.IsAccountsContainThisHosting(hostDomainName))
        {
            TempData["Message"] = "Zaten mevcut";
        }
        else
        {
            TempData["Message"] = "Başarıyla eklendi";
        }
        return Page();
    }
}