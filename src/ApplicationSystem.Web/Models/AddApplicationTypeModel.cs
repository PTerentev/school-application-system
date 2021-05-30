using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Add application type model.
    /// </summary>
    public class AddApplicationTypeModel
    {
        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
