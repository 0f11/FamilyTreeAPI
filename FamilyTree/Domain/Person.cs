using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Person: DomainEntity
    {
        [MaxLength(64)] 
        [MinLength(2)]
        public string FirstName { get; set; } = default!;
        [MaxLength(64)]
        [MinLength(2)]
        public string LastName { get; set; } = default!;
        public string FirstLastName => FirstName + " " + LastName;
        //needs to be calculated on js
        public int Age { get; set; }
        public Guid GenderId { get; set; } = default!; 
        public Gender? Gender { get; set; }
        public Guid AppUserId { get; set; } = default!; 
        public AppUser? AppUser { get; set; }
        
        public ICollection<Contact>? Contacts { get; set; }
        [InverseProperty(nameof(Domain.Relationships.PersonOne))]
        public ICollection<Relationships>? RelationshipsOne { get; set; }
        [InverseProperty(nameof(Domain.Relationships.PersonTwo))]
        public ICollection<Relationships>? RelationshipsTwo { get; set; }
    }
}