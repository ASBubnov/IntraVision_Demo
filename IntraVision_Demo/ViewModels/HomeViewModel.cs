using IntraVision_Demo.Models;

namespace IntraVision_Demo.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; } = null!;
        public IEnumerable<Coin> ActiveCoins { get; set; } = null!;
        public int UserBalance { get; set; }
    }
}
