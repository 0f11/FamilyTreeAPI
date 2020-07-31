using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Gender: DomainEntity
    {
        [MaxLength(64)]
        [MinLength(2)]
        public string GenderValue { get; set; }
    }
}