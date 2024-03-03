using FluentValidation;
using Microservices.UI.Models.Discount;

namespace Microservices.UI.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim kupon alanı boş olamaz.");
        }
    }
}
