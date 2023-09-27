using FluentValidation;
using Nudes.Paginator.Core;

namespace Nudes.Paginator.FluentValidation;

public class PageRequestValidator<T> : AbstractValidator<T> where T : PageRequest
{
    public PageRequestValidator()
    {
        RuleFor(d => d.Page).GreaterThan(0);
        RuleFor(d => d.PageSize).GreaterThan(0);
        RuleForEach(d => d.Sorting)
            .ChildRules(sorting =>
            {
                sorting.RuleFor(s => s.Field).NotEmpty();
                sorting.RuleFor(s => s.SortDirection).IsInEnum();
            });


    }
}

