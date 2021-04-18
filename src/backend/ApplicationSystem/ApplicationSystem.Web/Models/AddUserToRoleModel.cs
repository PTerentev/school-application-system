using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Add user to role model.
    /// </summary>
    public class AddUserToRoleModel
    {
        /// <summary>
        /// Role.
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
