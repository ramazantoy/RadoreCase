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
            TempData["Message"] = "Bu hesap zaten mevcut";
            return Page();
        }

        RadoreApiController.RadoreApiController.Instance.AddNewAccount(hostDomainName, hostingPackage);
        Thread.Sleep(300);
        
        if (RadoreApiController.RadoreApiController.Instance.IsAccountsContainThisHosting(hostDomainName))
        {
            TempData["Message"] = "Basariyla Eklendi";
        }
        return Page(); 
    }
}
