using AutoMapper;
using Course.Shared.Dtos;
using CourseServices.Catalog.Dtos;
using CourseServices.Catalog.Models;
using CourseServices.Catalog.Settings;
using MongoDB.Driver;

namespace CourseServices.Catalog.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly IMongoCollection<Courses> _coursesCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CoursesService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _coursesCollection = database.GetCollection<Courses>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _coursesCollection.Find(c => true).ToListAsync();
            
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Courses>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _coursesCollection.Find<Courses>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Category not found", 404);
            }
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _coursesCollection.Find<Courses>(x => x.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Courses>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourses = _mapper.Map<Courses>(courseCreateDto);
            newCourses.CreatedDate= DateTime.Now;
            await _coursesCollection.InsertOneAsync(newCourses);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourses),200);

        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Courses>(courseUpdateDto);

            var result = await _coursesCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);
            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _coursesCollection.DeleteOneAsync(x=> x.Id == id);
            if (result.DeletedCount>0 )
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found", 404);

            }
        }

    }
}
