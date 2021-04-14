using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Send to authority model.
    /// </summary>
    public class SendToAuthorityModel
    {
        /// <summary>
        /// Authority Id.
        /// </summary>
        [Required]
        public int? AuthorityId { get; set; }
    }
}
