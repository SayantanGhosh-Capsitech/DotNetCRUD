using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public class StudentService   : IStudentService
    {
        private readonly IMongoCollection<Student> _student;

        public StudentService(IStudentDatabaseSettings settings, IMongoClient mongoClient) { 

        var database = mongoClient.GetDatabase(settings.DatabaseName);
         _student  = database.GetCollection<Student>(settings.CollectionName);
        
        }
        public Student Create(Student student)
        {
            _student.InsertOne(student);
            return student;
        }

        public List<Student> Get()
        {
          return  _student.Find(student=>true).ToList();

        }

        public Student Get(string id)
        {
            return _student.Find(student => student.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _student.DeleteOne(student => student.Id == id);
        }

        public void Update(string id, Student student)
        {
            _student.ReplaceOne(student => student.Id == id, student);
        }
        //public void Update(string id, Student updatedStudent)
        //{
        //    var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
        //    var update = Builders<Student>.Update
        //        .Set(s => s.Name, updatedStudent.Name)
        //        .Set(s => s.Age, updatedStudent.Age)
        //        .Set(s => s.Gender, updatedStudent.Gender)
        //        .Set(s => s.IsGraduated, updatedStudent.IsGraduated)
        //        .Set(s => s.Courses, updatedStudent.Courses); // 👈 this replaces course IDs with new array

        //    _student.UpdateOne(filter, update);
        //}

        public List<CourseDetailsResponseList> GetStudentsWithCourseDetails()
        {
            var pipeline = new BsonDocument[]
            {
        new BsonDocument("$unwind", new BsonDocument
        {
            { "path", "$courses" },
            { "preserveNullAndEmptyArrays", true }
        }),
        new BsonDocument("$lookup", new BsonDocument
        {
            { "from", "Courses" },
            { "localField", "courses" },
            { "foreignField", "_id" },
            { "as", "courseDetail" }
        }),
        new BsonDocument("$set", new BsonDocument
        {
            { "courseDetail", new BsonDocument("$arrayElemAt", new BsonArray { "$courseDetail", 0 }) }
        }),
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", "$_id" },
            { "name", new BsonDocument("$first", "$name") },
            { "graduated", new BsonDocument("$first", "$graduated") },
            { "gender", new BsonDocument("$first", "$gender") },
            { "age", new BsonDocument("$first", "$age") },
            { "coursedetails", new BsonDocument("$push", "$courseDetail") }
        })
            };

            var studentDocs = _student.Aggregate<BsonDocument>(pipeline).ToList();

            var result = studentDocs.Select(b => new CourseDetailsResponseList
            {
                Name = b.GetValue("name", "").AsString,
                IsGraduated = b.GetValue("graduated", false).AsBoolean,
                Gender = b.GetValue("gender", "").AsString,
                Age = b.GetValue("age", 0).AsInt32,
                Id = b.GetValue("_id", "").ToString(),
                CourseDetail = b.GetValue("coursedetails", new BsonArray())
                   .AsBsonArray
                   .Select(c => new CourseDetail
                   {
                       Id = c["_id"].ToString(),
                       Name = c["name"].AsString
                   }).ToList()
            }).ToList();

            return result;
        }
    }



}

