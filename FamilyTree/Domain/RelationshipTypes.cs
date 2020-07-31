using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class RelationshipTypes: DomainEntity
    {
        [MaxLength(64)]
        [MinLength(2)]
        public string RelationshipTypeValues { get; set; }

        public ICollection<Relationships>? Relationships { get; set; }
    }
}