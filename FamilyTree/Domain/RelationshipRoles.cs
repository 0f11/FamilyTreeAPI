using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class RelationshipRoles: DomainEntity
    {
        [MaxLength(64)]
        [MinLength(2)]
        public string RelationshipRolesValue { get; set; }
        [InverseProperty(nameof(Domain.Relationships.RoleOne))]
        public ICollection<Relationships>? RelationshipsOne { get; set; }
        [InverseProperty(nameof(Domain.Relationships.RoleTwo))]
        public ICollection<Relationships>? RelationshipsTwo { get; set; }

    }
}