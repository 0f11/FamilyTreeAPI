using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Contact: DomainEntity
    {
        [MaxLength(64)]
        [MinLength(2)]
        public string ContactValue { get; set; }

        public Guid ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
        
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}