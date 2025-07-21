using WebApiDemo.Models;
using MongoDB.Driver;

namespace WebApiDemo.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _course;

        public CourseService(ICourseDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _course = database.GetCollection<Course>(settings.CourseCollectionName);
        }

        public Course Create(Course course)
        {
            _course.InsertOne(course);
            return course;
        }

        public List<Course> Get()
        {
            return _course.Find(course => true).ToList();

        }

        public Course Get(string id)
        {
            return _course.Find(course => course.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _course.DeleteOne(course => course.Id == id);
        }

        public void Update(string id, Course course)
        {
            course.Id = id;  
            _course.ReplaceOne(c => c.Id == id, course);
        }
    }
}

