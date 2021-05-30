using System.Collections.Generic;

namespace ApplicationSystem.Infrastructure.Common.Dtos
{
    /// <summary>
    /// User DTO.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// User's post.
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public IEnumerable<string> UserRoles { get; set; }
    }
}
