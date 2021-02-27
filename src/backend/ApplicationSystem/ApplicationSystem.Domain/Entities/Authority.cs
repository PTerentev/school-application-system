using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Authority.
    /// </summary>
    public class Authority
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Authority()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Main user Id.
        /// </summary>
        public int MainUserId { get; set; }

        /// <summary>
        /// Main user.
        /// </summary>
        public User MainUser { get; set; }

        /// <summary>
        /// Users.
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
