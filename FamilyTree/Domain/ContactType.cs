using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class ContactType: DomainEntity
    {
        [MaxLength(64)]
        [MinLength(2)]
        public string ContactTypeValue { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}