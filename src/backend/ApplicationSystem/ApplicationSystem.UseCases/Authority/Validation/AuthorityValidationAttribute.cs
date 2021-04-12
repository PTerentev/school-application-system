using System.ComponentModel.DataAnnotations;
using System.Linq;
using Saritasa.Tools.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;

namespace ApplicationSystem.UseCases.Authority.Validation
{
    /// <summary>
    /// Authority validation.
    /// Check if the current user has access to the requested authority.
    /// </summary>
    public class AuthorityValidationAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetRequiredService<ApplicationDbContext>();
            var accessUserPrincipalService = validationContext.GetRequiredService<IAccessUserPrincipalService>();

            if (value is AuthorityRequestBase request)
            {
                return IsValid(request, dbContext, accessUserPrincipalService);
            }
            else
            {
                throw new DomainException($"The {typeof(AuthorityValidationAttribute).Name} attribute cannot be used with {value.GetType().Name} model.");
            }
        }

        private static ValidationResult IsValid(AuthorityRequestBase request, ApplicationDbContext dbContext, IAccessUserPrincipalService accessUserPrincipalService)
        {
            var userId = accessUserPrincipalService.GetUserId();

            var authority = dbContext.Authorities.Where(a => a.Id == request.AuthorityId).Single();
            if (!authority.Users.Any(u => u.Id == userId))
            {
                return new ValidationResult("The user does not have access to the authority.");
            }

            return ValidationResult.Success;
        }
    }
}
