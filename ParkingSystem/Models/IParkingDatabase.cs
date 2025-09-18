namespace ParkingSystem.Models
{
    public interface IParkingDatabase
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string CollectionName { get; set; }

    }
}
