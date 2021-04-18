using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Add user to authority model.
    /// </summary>
    public class AddUserToAuthorityModel
    {
        /// <summary>
        /// User Id.
        /// </summary>
        [Required]
        public int? UserId { get; set; }
    }
}
