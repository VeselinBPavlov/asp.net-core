namespace Panda.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    
    public class Receipt
    {
        public Receipt()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual PandaUser Recipient { get; set; }

        [Required]
        public string PackageId { get; set; }

        public virtual Package Package { get; set; }
    }
}
