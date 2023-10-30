﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManager.Core.Domain.Entities
{
    /// <summary>
    /// Person Domain model Class
    /// </summary>
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        [StringLength(40)] //nvarchar(40)
        [Required]
        public string? PersonName { get; set; }

        [StringLength(40)] //nvarchar(40)
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)] //nvarchar(40)
        public string? Gender { get; set; }

        //uniqueidentifier
        public Guid? CountryID { get; set; }
        [StringLength(100)] //nvarchar(40)
        public string? Address { get; set; }
        //bits
        public bool ReceiveNewsLetters { get; set; }

        public string? TIN { get; set; } //Tax identification number

        [ForeignKey("CountryID")]
        public Country? Country { get; set; }

    }
}
