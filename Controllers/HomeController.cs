using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BetaLottery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BetaLottery.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
  

    private MyContext _context;

    // here we can "inject" our context service into the constructor
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    
    public IActionResult PromoterRegister(){
        if (HttpContext.Session.GetInt32("PromoterUserId") == null)
        {
            return View();
        }
        return RedirectToAction("IndexPromoter"); 
    }

    [HttpPost]
    public IActionResult PromoterRegister(PromoterUser promoteruser)
    {
        if (ModelState.IsValid)
        {
            if (_context.PromoterUsers.Any(u => u.UserName == promoteruser.UserName))
            {
                ModelState.AddModelError("Username", "UserName is already in use!");

                return View("PromoterRegister");
            }

            PasswordHasher<PromoterUser> Hasher = new PasswordHasher<PromoterUser>();
            promoteruser.Password = Hasher.HashPassword(promoteruser, promoteruser.Password);
            _context.PromoterUsers.Add(promoteruser);
            _context.SaveChanges();
            PromoterUser PromoterUserdb = _context.PromoterUsers.FirstOrDefault(u => u.UserName == promoteruser.UserName);

            HttpContext.Session.SetInt32("PromoterUserId", PromoterUserdb.PromoterUserId);
            int IntVariable = (int)HttpContext.Session.GetInt32("PromoterUserId");

            return RedirectToAction("IndexPromoter");
        }
        else
        {
            return View("PromoterRegister");
        }
    }

    


    

    public IActionResult IndexClient(){
       int clientId = (int)HttpContext.Session.GetInt32("ClientId");
        if (HttpContext.Session.GetInt32("ClientId") != null)
        {
            ViewBag.MyProductsParticipate=_context.Participates.Include(e=>e.MyProduct).Where(e=>e.ClientId==clientId).ToList();
            ViewBag.ProductsList=_context.Products.ToList();

            return View();
        }
        return RedirectToAction("IndexClient"); 
    }

    public IActionResult IndexPromoter(){
        if (HttpContext.Session.GetInt32("PromoterUserId") != null)
        {
            ViewBag.ProductsList=_context.Products.Where(e=>e.PromoterUserId==HttpContext.Session.GetInt32("PromoterUserId")  ).ToList();
            return View();
        }
        return RedirectToAction("IndexPromoter"); 
    }

    public IActionResult ClientRegister(){
        if (HttpContext.Session.GetInt32("ClientId") == null)
        {
            return View();
        }
        return RedirectToAction("IndexClient"); 
    }

    [HttpPost]
    public IActionResult ClientRegister(Client client)
    {
        if (ModelState.IsValid)
        {
            if (_context.Clients.Any(u => u.UserName == client.UserName))
            {
                ModelState.AddModelError("Username", "UserName is already in use!");

                return View("ClientRegister");
            }

            PasswordHasher<Client> Hasher = new PasswordHasher<Client>();
            client.Password = Hasher.HashPassword(client, client.Password);
            _context.Clients.Add(client);
            _context.SaveChanges();
            Client Clientdb = _context.Clients.FirstOrDefault(u => u.UserName == client.UserName);

            HttpContext.Session.SetInt32("ClientId", Clientdb.ClientId);
            int IntVariable = (int)HttpContext.Session.GetInt32("ClientId");

            return RedirectToAction("IndexClient");
        }
        else
        {
            return View("ClientRegister");
        }
    }
    public IActionResult ClientLogin()
    {
        return View();
    }

public IActionResult PromoterLogin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ClientLogin(LoginUser user)
    {
        if (ModelState.IsValid)
        {

            var userInDb1 = _context.Clients.FirstOrDefault(u => u.UserName == user.UserName);

             if (userInDb1 == null)
            {
                ModelState.AddModelError("Username", "Invalid UserName/Password");
                return View("ClientRegister");
            }else{
                 var hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(user, userInDb1.Password, user.Password);

            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("ClientRegister");
            }
            HttpContext.Session.SetInt32("ClientId", userInDb1.ClientId);
            
            return RedirectToAction("IndexClient");
            }  
        }
        return View("ClientRegister");
    }

    [HttpPost]

    public IActionResult PromoterLogin(LoginUser user)
    {
        if (ModelState.IsValid)
        {


            var userInDb = _context.PromoterUsers.FirstOrDefault(u => u.UserName == user.UserName);

            if (userInDb == null)
            {
                ModelState.AddModelError("Username", "Invalid UserName/Password");
                return View("PromoterRegister");
            }else{
                 var hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);

            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("PromoterRegister");
            }
            HttpContext.Session.SetInt32("PromoterUserId", userInDb.PromoterUserId);
            return RedirectToAction("IndexPromoter");
            }
        }
        return View("PromoterRegister");
    }


    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return View("Index");
    }


    public IActionResult ProductAdd()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProductAdd(Product product)
    {
        int PromoterId = (int) HttpContext.Session.GetInt32("PromoterUserId");
        // string StringFileName = UploadFile(product);
        
        // var post = new Product(){
        //     NameOfProduct = product.NameOfProduct,
        //     Description = product.Description,
        //     PromoterUserId=PromoterId
        // };
        product.PromoterUserId=PromoterId;
            _context.Products.Add(product);
            _context.SaveChanges();
        return RedirectToAction("IndexPromoter");
        
    }
//   private string UploadFile(Product marrNgaView)
//     {
//        string fileName = null;
//        if(marrNgaView.Image!=null){
//         string Uploaddir = Path.Combine(WebHostEnvironment.WebRootPath,"Images");
//         fileName = Guid.NewGuid().ToString() + "-" + marrNgaView.Image.FileName;
//         string filePath = Path.Combine(Uploaddir,fileName);
//         using (var filestream = new FileStream(filePath,FileMode.Create))
//         {
//                 marrNgaView.Image.CopyTo(filestream);
//         }
//        }
//        return fileName;
//     }

  

    public IActionResult ShowProduct(int id){

            ViewBag.ProductShow= _context.Products.FirstOrDefault(e=>e.ProductId==id);
            return View();
        
        
    }
    
    
    public IActionResult Participate(int id)
    {int ClientId = (int) HttpContext.Session.GetInt32("ClientId");
      Participate MyParticipate = new Participate()
      {
        ProductId = id,
        ClientId =  ClientId
      };
        _context.Participates.Add(MyParticipate);
            _context.SaveChanges();
     return RedirectToAction("IndexClient");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
