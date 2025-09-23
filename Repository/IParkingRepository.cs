using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public interface IParkingRepository: IRepository<Parking>
    {
        Task<IEnumerable<Parking>> GetByLocationAsync(string location);
    }
}
