using IntraVision_Demo.Models;
using IntraVision_Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IntraVision_Demo.ViewModels;

namespace IntraVision_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVendingMachineService _vendingMachineService;
        private readonly String SecretKey;
        private readonly int VendingMachineId;
        public HomeController(IVendingMachineService vendingMachineService, IOptions<VendingMachineSettings> options)
        {
            _vendingMachineService = vendingMachineService;
            SecretKey = options.Value.AdminKey;
            VendingMachineId = options.Value.Id;
        }

        public IActionResult Index()
        {
            var obj = new HomeViewModel()
            {
                Drinks = _vendingMachineService.GetAllActualDrinks(VendingMachineId),
                ActiveCoins = _vendingMachineService.GetAllCoins(VendingMachineId),
                UserBalance = _vendingMachineService.GetUserBalance(VendingMachineId),
            };
            return View(obj);
        }

        [HttpPost]
        public IActionResult BuyDrink([FromQuery] int drinkId)
        {            
            return Ok(_vendingMachineService.BuyDrink(VendingMachineId, drinkId));
        }

        [HttpPost]
        public IActionResult EditDrink([FromForm] Drink drink)
        {
            _vendingMachineService.EditDrink(drink);
            return Redirect($"~/Home/Privacy?AdminKey={SecretKey}");
        }

        [HttpPost]
        public IActionResult EditCoin([FromForm] Coin coin)
        {
            _vendingMachineService.EditCoin(coin);
            return Redirect($"/Home/Privacy?AdminKey={SecretKey}");
        }

        [HttpPost]
        public IActionResult SetUserBalance([FromQuery] int coinId)
        {
            var model = _vendingMachineService.SetUserBalance(VendingMachineId, coinId);
            return Ok(model);
        }
        public IActionResult Privacy([FromQuery] string adminKey)
        {
            if(adminKey != SecretKey)
            {
                return Problem(
                    type: "/docs/errors/forbidden",
                    title: "Authenticated user is not authorized.",
                    detail: "AdminKeyIsNotValid",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }
            var obj = new PrivacyViewModel()
            {
                Coins = _vendingMachineService.GetAllCoins(VendingMachineId),
                Drinks = _vendingMachineService.GetAllDrinks(VendingMachineId),
                VendingMachineBalance = _vendingMachineService.GetMachineBalance(VendingMachineId)
            };
            return View(obj);
        }


    }
}