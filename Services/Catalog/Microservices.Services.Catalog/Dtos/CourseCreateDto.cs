﻿namespace Microservices.Services.Catalog.Dtos
{
    public class CourseCreateDto
    {
        public int Price { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public FeatureDto FeatureDto { get; set; }

        public string CategoryId { get; set; }

    }
}
