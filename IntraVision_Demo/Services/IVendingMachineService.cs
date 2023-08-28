using IntraVision_Demo.DTOs;
using IntraVision_Demo.Interfaces;
using IntraVision_Demo.Models;

namespace IntraVision_Demo.Services
{
    public interface IVendingMachineService
    {
        bool CanBuy(Drink drink, int money);
        BuyDrinkResponse BuyDrink(int vmId, int drinkId);
        IEnumerable<Coin> GetActiveCoins(int id);
        IEnumerable<Coin> GetAllCoins(int id);
        void EditCoin(Coin coin);
        int GetMachineBalance(int id);
        int GetUserBalance(int id);
        int SetUserBalance(int vmId,int id);
        void ReplenishUserBalance(int vmId, int coinId);
        IEnumerable<Drink> GetAllDrinks(int id);
        IEnumerable<Drink> GetAllActualDrinks(int id);
        void EditDrink(Drink drink);
    }

    public class VendingMachineService : IVendingMachineService
    {
        private readonly IRepository<VendingMachine> _repository;
        public VendingMachineService( IRepository<VendingMachine> repository)
        {
            _repository = repository;
        }

        public BuyDrinkResponse BuyDrink(int vmId, int drinkId)
        {
            var vm = _repository.FindByID(vmId);
            var drink = vm!.DrinkSetting.FirstOrDefault(x => x.Id == drinkId)!;
            var userBalance = GetUserBalance(vmId);
            var response = new BuyDrinkResponse()
            {
                Drink = drink                
            };
            if (CanBuy(drink, userBalance))
            {
                
                foreach(var coin in vm.UserBalance)
                {
                    vm.CoinSetting.FirstOrDefault(x => x.Id == coin.Id)!.Count += coin.Count;
                    coin.Count = 0;
                }
                var change = userBalance - drink.Price;
                foreach(var coin in vm.CoinSetting.OrderByDescending(x => x.Amount))
                {
                    int tmp = change / coin.Amount;
                    if(tmp > 0)
                    {
                        if(tmp <= coin.Count)
                        {                            
                            response.Change.Add(new CoinResponse() { Count = tmp, Amount = coin.Amount });
                            change -= tmp * coin.Amount;
                            coin.Count -= tmp;
                        }
                        else
                        {
                            response.Change.Add(new CoinResponse() { Count = coin.Count, Amount = coin.Amount });
                            change -= coin.Count * coin.Amount;
                            coin.Count = 0;
                        }                        
                    }
                }
                if(change == 0)
                {
                    response.Success = true;
                    drink!.Count -= 1;
                    _repository.Update(vm);
                }                
            }
            return response;
        }

        public bool CanBuy(Drink drink, int money)
        {
            return drink.Price <= money && drink.Count > 0;
        }

        public IEnumerable<Coin> GetActiveCoins(int id)
        {
            return _repository.FindByID(id)!.CoinSetting.Where(x => x.IsActive);
        }
        public IEnumerable<Coin> GetAllCoins(int id)
        {
            return _repository.FindByID(id)!.CoinSetting;
        }

        public void EditCoin(Coin coin)
        {
            var vm = _repository.FindByID(coin.VendingMachineId)!;
            var vmCoin = vm.CoinSetting.FirstOrDefault(x => x.Id == coin.Id)!;
            vmCoin.Name = coin.Name;
            vmCoin.Amount = coin.Amount;
            vmCoin.Count = coin.Count;
            vmCoin.IsActive = coin.IsActive;
            _repository.Update(vm);
        }

        public IEnumerable<Drink> GetAllActualDrinks(int id)
        {
            return _repository.FindByID(id)!.DrinkSetting.Where(x => x.Count > 0);
        }

        public IEnumerable<Drink> GetAllDrinks(int id)
        {
            return _repository.FindByID(id)!.DrinkSetting;
        }

        public void EditDrink(Drink drink)
        {
            var vm = _repository.FindByID(drink.VendingMachineId)!;
            var drinkVM = vm.DrinkSetting.FirstOrDefault(x => x.Id == drink.Id)!;
            drinkVM.Name = drink.Name;
            drinkVM.PictureURL = drink.PictureURL;
            drinkVM.Price = drink.Price;
            drinkVM.Count = drink.Count;
            _repository.Update(vm);
        }

        public int GetMachineBalance(int id)
        {
            return _repository.FindByID(id)!.CoinSetting.Sum(x => x.Amount * x.Count);
        }

        public int GetUserBalance(int id)
        {
            return _repository.FindByID(id)!.UserBalance.Sum(x => x.Amount * x.Count);
        }
        public int SetUserBalance(int vmId,int id)
        {
            var obj = _repository.FindByID(vmId);
            var coin = obj!.UserBalance.FirstOrDefault(x => x.Id == id)!;
            coin.Count ++;
            _repository.Update(obj);            
            return obj.UserBalance.Sum(x => x.Amount * x.Count);
        }
        public void ReplenishUserBalance(int vmId, int coinId)
        {
            var obj = _repository.FindByID(vmId)!.UserBalance;
        }


    }
    
}
