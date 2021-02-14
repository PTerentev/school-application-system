using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Creator user Id.
        /// </summary>
        public int CreatorUserId { get; set; }

        /// <summary>
        /// Creator user.
        /// </summary>
        public User CreatorUser { get; set; }

        /// <summary>
        /// Reply of the application.
        /// </summary>
        public string Reply { get; set; }

        /// <summary>
        /// Application state.
        /// </summary>
        public ApplicationState State { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Edit comments.
        /// </summary>
        public string EditComments { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        public int AuthorityId { get; set; }

        /// <summary>
        /// Authority.
        /// </summary>
        public Authority Authority { get; set; }

        /// <summary>
        /// Close date.
        /// </summary>
        public DateTime? CloseDate { get; set; }
    }

    /// <summary>
    /// Application state.
    /// </summary>
    public enum ApplicationState
    {
        NotProceed = 0,
        Rejected = 1,
        Sent = 2,
        ReplyReady = 3,
        EditNeeds = 4,
        Published = 5
    }
}
