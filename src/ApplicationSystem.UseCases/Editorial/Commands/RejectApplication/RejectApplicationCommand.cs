﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.UseCases.Editorial.Commands.RejectApplication
{
    /// <summary>
    /// Reject application command.
    /// </summary>
    public class RejectApplicationCommand : IRequest
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Reject comments.
        /// </summary>
        [Required]
        public string RejectComments { get; set; }
    }
}
