using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Relationships: DomainEntity
    {
        [ForeignKey(nameof(PersonOne))]
        public Guid? PersonOneId { get; set; }
        public Person PersonOne { get; set; }

        [ForeignKey(nameof(PersonTwo))]
        public Guid? PersonTwoId { get; set; }
        public Person PersonTwo { get; set; }

        [ForeignKey(nameof(RoleOne))]
        public Guid? RoleOneId { get; set; }
        public RelationshipRoles RoleOne { get; set; }
        
        [ForeignKey(nameof(RoleTwo))]
        public Guid? RoleTwoId { get; set; }
        public RelationshipRoles RoleTwo { get; set; }

        public Guid FamilyId { get; set; }
        public Family Family { get; set; }

        public Guid RelationshipTypesId { get; set; }
        public RelationshipTypes RelationshipTypes { get; set; }
        
    }
}