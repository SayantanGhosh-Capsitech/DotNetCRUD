namespace WebApiDemo.Models
{
    public interface IStudentDatabaseSettings
    {
        string ConnectionURI { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
