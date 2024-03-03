using FluentValidation;
using Microservices.UI.Models.Catalog.Inputs;

namespace Microservices.UI.Validators
{
    public class CourseUpdateInputValidator : AbstractValidator<
        CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş olamaz.");
            RuleFor(x => x.Picture).NotEmpty().WithMessage("Resim alanı boş olamaz.");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre alanı boş olamaz.");
            // first 2 is after comma, 6 is total number count. $$$$.$$ is okey. $$$$.$ is not
            RuleFor(x => x.Price).ScalePrecision(2, 6).NotEmpty().WithMessage("Fiyat alanı boş olamaz. Hatalı Format");
        }
    }
}
