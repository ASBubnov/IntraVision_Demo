using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;


namespace IntraVision_Demo.Models
{
    public class VendingMachine
    {
        public int Id { get; set; }
        public IEnumerable<Drink> DrinkSetting { get; set; } = null!;
        public IEnumerable<Coin> CoinSetting { get; set; } = null!;
        public IList<Coin> UserBalance { get; set; } = new List<Coin>();
    }
}
