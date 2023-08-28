using IntraVision_Demo.DBConnector;
using IntraVision_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace IntraVision_Demo.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        T? FindByID(int id);
        T Create(T obj);
        T Update(T obj);
        void Delete(int id);
    }

    


    public class VendingMachineRepository : IRepository<VendingMachine>
    {
        private DBMyContext _context;
        public VendingMachineRepository(DBMyContext context)
        {
            _context = context;
        }
        public VendingMachine Create(VendingMachine obj)
        {
            _context.VendingMachines.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete(int id)
        {
            var obj = _context.VendingMachines.FirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _context.VendingMachines.Remove(obj);
                _context.SaveChanges();
            }
        }

        public IEnumerable<VendingMachine> Find(Func<VendingMachine, bool> predicate)
        {
            return _context.VendingMachines.Where(predicate);
        }

        public VendingMachine? FindByID(int id)
        {
            return _context.VendingMachines.FirstOrDefault(x => x.Id == id);
        }

        public VendingMachine Update(VendingMachine obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }
}
