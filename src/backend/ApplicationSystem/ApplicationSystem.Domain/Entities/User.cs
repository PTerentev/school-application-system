using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// User.
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public User()
        {
            Authorities = new List<Authority>();
            Applications = new List<Application>();
        }

        /// <summary>
        /// First name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        /// <summary>
        /// Middle name.
        /// </summary>
        [MaxLength(255)]
        public string MiddleName { get; set; }

        /// <summary>
        /// User's post.
        /// </summary>
        [MaxLength(255)]
        public string Post { get; set; }

        /// <summary>
        /// Authorities.
        /// </summary>
        public ICollection<Authority> Authorities { get; set; }

        /// <summary>
        /// Applications.
        /// </summary>
        public ICollection<Application> Applications { get; set; }
    }
}
