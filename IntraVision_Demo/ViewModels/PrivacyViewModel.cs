using IntraVision_Demo.Models;

namespace IntraVision_Demo.ViewModels
{
    public class PrivacyViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; } = null!;
        public IEnumerable<Coin> Coins { get; set; } = null!;
        public int VendingMachineBalance { get; set; }
    }
}
