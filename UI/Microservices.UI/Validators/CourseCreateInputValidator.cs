using FluentValidation;
using Microservices.UI.Models.Catalog.Inputs;

namespace Microservices.UI.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<
        CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş olamaz.");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre alanı boş olamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori alanı seçiniz.");

            // first 2 is after comma, 6 is total number count. $$$$.$$ is okey. $$$$.$ is not
            RuleFor(x => x.Price).ScalePrecision(2, 6).NotEmpty().WithMessage("Fiyat alanı boş olamaz. Hatalı Format");
        }
    }
}
