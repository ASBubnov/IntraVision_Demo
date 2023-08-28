using IntraVision_Demo.Models;

namespace IntraVision_Demo.DTOs
{
    public class BuyDrinkResponse
    {
        public Drink Drink { get; set; } = null!;
        public IList<CoinResponse> Change {get; set;} = new List<CoinResponse>();
        public bool Success { get; set; }
    }
}
