using Microsoft.EntityFrameworkCore;
using IntraVision_Demo.Models;


namespace IntraVision_Demo.DBConnector
{
    public class DBMyContext : DbContext
    {
        public DbSet<VendingMachine> VendingMachines { get; set; }
        public DBMyContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendingMachine>(x => {
            x.HasData(
                new VendingMachine
                {
                    Id = 1
                });
            x.OwnsMany(y => y.CoinSetting).HasData(
            new List<Coin>() {
                new Coin { Id = 1, Name = "Один", Amount = 1, IsActive = true, Count = 0, VendingMachineId = 1 },
                new Coin { Id = 2, Name = "Два", Amount = 2, IsActive = true, Count = 0, VendingMachineId = 1 },
                new Coin { Id = 3, Name = "Пять", Amount = 5, IsActive = true, Count = 0, VendingMachineId = 1 },
                new Coin { Id = 4, Name = "Десять", Amount = 10, IsActive = true, Count = 0, VendingMachineId = 1 }
            });

            x.OwnsMany(y => y.DrinkSetting).HasData(
                new List<Drink> {
                    new Drink { Id = 1, Name = "Coca-cola", Price = 50, Count = 10, PictureURL ="https://smartloadusa.com/wp-content/uploads/53418-9.jpg",VendingMachineId = 1},
                    new Drink { Id = 2, Name = "Fanta", Price = 45, Count = 5, PictureURL ="https://ostrov-shop.by/upload/resize_cache/webp/iblock/1f9/wl9aesf7o0nai5y7vfs7kqaj74b2fhr2.webp",VendingMachineId = 1},
                    new Drink { Id = 3, Name = "Sprite", Price = 44, Count = 1, PictureURL ="https://dakcii.katalog-ceny.ru/image/cache/catalog/dixyall/napitok-sprite-zhb-033-l-500x500.jpg",VendingMachineId = 1},
                    new Drink { Id = 4, Name = "Pepsi", Price = 41, Count = 3, PictureURL ="https://prima-porte.ru/image/cache/catalog/product/Voda_Soki_Napitki/Napitki/pepsi_0-33l-500x500.jpg",VendingMachineId = 1}
                });
            x.OwnsMany(y => y.UserBalance).HasData(
                new List<Coin>() {
                new Coin { Id = 1, Name = "Один", Amount = 1, IsActive = true, Count = 0,  VendingMachineId = 1 },
                new Coin { Id = 2, Name = "Два", Amount = 2, IsActive = true, Count = 0,  VendingMachineId = 1 },
                new Coin { Id = 3, Name = "Пять", Amount = 5, IsActive = true, Count = 0,  VendingMachineId = 1 },
                new Coin { Id = 4, Name = "Десять", Amount = 10, IsActive = true, Count = 0, VendingMachineId = 1 }
                });
            });
            
        }
    }
}
