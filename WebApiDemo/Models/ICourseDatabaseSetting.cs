namespace WebApiDemo.Models
{
    public interface ICourseDatabaseSettings
    {
        string ConnectionURI { get; set; }
        string DatabaseName { get; set; }
        string CourseCollectionName { get; set; }
    }
}
