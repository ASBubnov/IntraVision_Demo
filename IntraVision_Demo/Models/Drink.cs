using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntraVision_Demo.Models
{
    public class Drink
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Count { get; set; }        
        public string PictureURL { get; set; } = null!;
        [ForeignKey("VendingMachine")]
        public int VendingMachineId { get; set; }
    }
}
