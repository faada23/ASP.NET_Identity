using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Identity.Models;
using IdentityPersistance.Models;
using IdentityPersistance.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using IdentityUtility.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Identity.Controllers;

[Area("User")]
public class AuthController : Controller
{   
    private readonly ILogger<AuthController> _logger;

    private readonly IRepository<Account> _accountRepository;

    private readonly JWTGenerator _jwtGenerator;


    public AuthController(ILogger<AuthController> logger, IRepository<Account> accountRepository, JWTGenerator jwtGenerator)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _jwtGenerator = jwtGenerator;
    }

    [HttpGet]
    public IActionResult Register()
    {   
        Account Account = new Account();
        return View(Account);
    }

    [HttpPost]
    public IActionResult Register(Account account)
    {   
        var HashPassword =  new PasswordHasher<Account>().HashPassword(account, account.Password);
        account.Password = HashPassword;

        _accountRepository.Add(account);
        _accountRepository.Save();
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {   
        Account Account = new Account();
        return View(Account);
    }

    [HttpPost]
    public IActionResult Login(Account account)
    {   
        var User = _accountRepository.GetAll().FirstOrDefault(u => u.Name == account.Name);
        if (User != null) 
        {
            var HashPassword = new PasswordHasher<Account>().VerifyHashedPassword(User, User.Password, account.Password);
            if (HashPassword == PasswordVerificationResult.Success)
            {   
                TempData["Message"] = "Успешная аутентификация!";
                var token = _jwtGenerator.GenerateToken(User);
                HttpContext.Response.Cookies.Append("MyCookie", token);
                ViewBag.Token = token; 
                return View(); 
            }
        }
        TempData["Error"] = "Ошибка аутентификации. Попробуйте еще раз.";
        return View();
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
