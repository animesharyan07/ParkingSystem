using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IParkingServices
    {
        List<Parking> Get();
        Parking? Get(string id);
        Parking Create(Parking parking);
        void Update(string id, Parking parkingIn);
        void Remove(string id);
    }
}
