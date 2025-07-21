using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public interface ICourseService
    {
        List<Course> Get();
        Course Get(string id);
        Course Create(Course student);
        void Update(string id, Course student);
        void Remove(string id);
    }
}
