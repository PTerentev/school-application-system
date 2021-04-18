using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Resend to authority model.
    /// </summary>
    public class ResendToAuthorityModel
    {
        /// <summary>
        /// Edit comments.
        /// </summary>
        [Required]
        public string EditComments { get; set; }
    }
}
