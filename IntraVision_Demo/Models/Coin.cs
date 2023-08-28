using System.ComponentModel.DataAnnotations.Schema;

namespace IntraVision_Demo.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
        public bool IsActive { get; set; }
        public int Count { get; set; }
        [ForeignKey("VendingMachine")]
        public int VendingMachineId { get; set; }
    }
}
