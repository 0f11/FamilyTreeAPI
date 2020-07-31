using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Family: DomainEntity
    {
        [MaxLength(64)]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<Relationships> Relationships { get; set; }
    }
}