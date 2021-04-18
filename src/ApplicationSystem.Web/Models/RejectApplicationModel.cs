using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Reject application model.
    /// </summary>
    public class RejectApplicationModel
    {
        /// <summary>
        /// Reject comments.
        /// </summary>
        [Required]
        public string RejectComments { get; set; }
    }
}
