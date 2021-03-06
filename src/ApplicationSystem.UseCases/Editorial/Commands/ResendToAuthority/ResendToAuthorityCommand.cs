﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.UseCases.Editorial.Commands.ResendToAuthority
{
    /// <summary>
    /// Resend to authority command.
    /// </summary>
    public class ResendToAuthorityCommand : IRequest
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Edit comments.
        /// </summary>
        [Required]
        public string EditComments { get; set; }
    }
}
