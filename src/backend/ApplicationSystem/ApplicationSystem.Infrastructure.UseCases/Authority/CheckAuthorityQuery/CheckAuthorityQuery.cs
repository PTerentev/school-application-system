using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.CheckAuthorityQuery
{
    /// <summary>
    /// Check authority query.
    /// </summary>
    public class CheckAuthorityQuery
    {
        /// <summary>
        /// Authority Id.
        /// </summary>
        [Required]
        public int AuthorityId { get; set; }
    }
}
