using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IParkingServices
    {
        List<Parking> Get();
        Parking Get(string id);

        Parking Create(Parking parking);

        void Update(string id, Parking parkingIn);

        void Remove(string id);


    }
}
