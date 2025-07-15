namespace WebApiDemo.Models
{
    public class StudentDatabaseSettings : IStudentDatabaseSettings
    {
      public  string ConnectionURI { get; set; } = String.Empty;
      public  string DatabaseName { get; set; } = String.Empty ;
      public  string CollectionName { get; set; } = string.Empty ;  
    }
}
