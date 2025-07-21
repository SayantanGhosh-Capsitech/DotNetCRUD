namespace WebApiDemo.Models
{
    public class CourseDatabaseSettings: ICourseDatabaseSettings
    {
        public string ConnectionURI { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string CourseCollectionName { get; set; } = string.Empty;
    }
}
