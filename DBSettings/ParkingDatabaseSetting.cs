namespace ParkingSystem.DBSettings
{
    public class ParkingDatabaseSetting : IParkingDatabase
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public string CollectionName { get; set; } = string.Empty;

        public string UserCollectionName {  get; set; } = string.Empty;
    }
}
