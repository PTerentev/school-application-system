using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.UseCases.Authority.Common
{
    /// <summary>
    /// Authority request base.
    /// </summary>
    public abstract class AuthorityRequestBase
    {
        /// <summary>
        /// Authority Id.
        /// </summary>
        [Required]
        public int AuthorityId { get; set; }
    }
}
