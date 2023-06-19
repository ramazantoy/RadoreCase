
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadoreCaseRamazan.Models;

namespace RadoreCaseRamazan.Pages;

public class GetAccountData : PageModel
{

 
    public AccountData? AccountData = null;
 
    public IActionResult OnPostGetAccount()
    {
        string hostDomainName = Request.Form["hostingDomainName"];

        if (hostDomainName.Length <= 0)
        {
            TempData["Message"] = "domain ismi girmelisiniz";
            return Page(); 
        }

        AccountData = RadoreApiController.RadoreApiController.Instance.GetAccountData(hostDomainName);

        if (AccountData == null || AccountData.Message.Equals(hostDomainName + " NOT FOUND."))
        {
            TempData["Message"] = "boyle bir hesap bulunamadi.";
            AccountData = null;
        }
        
        return Page(); 
    }
 
}

