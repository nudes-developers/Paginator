using FluentValidation;
using Nudes.Paginator.Core;
using System;

namespace Nudes.Paginator.FluentValidation
{
    public class PageRequestValidator<T> : AbstractValidator<T> where T : PageRequest
    {
        public PageRequestValidator()
        {
            RuleFor(d => d.Page).GreaterThan(0);
            RuleFor(d => d.PageSize).GreaterThan(0);
            RuleFor(d => d.Field).NotEmpty().When(d => d.Field != null);
            RuleFor(d => d.SortDirection).IsInEnum().When(d => d.SortDirection != null);
        }
    }
}

