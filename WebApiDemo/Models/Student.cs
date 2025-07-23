using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace WebApiDemo.Models
{

    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }

        [BsonElement("courses")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string[]? Courses { get; set; }  // No [BsonId] here!

        [BsonElement("gender")]
        public string Gender { get; set; } = String.Empty;

        [BsonElement("age")]
        public int Age { get; set; }

    }

    public class CourseDetailsResponseList: Student
    {
        [BsonElement("coursedetails")]
        public List<CourseDetail> CourseDetail { get; set; }
    }

    public class CourseDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
