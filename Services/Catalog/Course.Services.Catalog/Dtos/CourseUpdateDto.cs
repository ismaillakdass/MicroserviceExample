﻿namespace CourseServices.Catalog.Dtos
{
    public class CourseUpdateDto
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
