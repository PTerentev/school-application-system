using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Add user to role model.
    /// </summary>
    public class AddUserToRoleModel
    {
        /// <summary>
        /// Role Id.
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}
