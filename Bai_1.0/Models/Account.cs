using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bai_1._0.Models
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        [Column("ID_Acc")]
        public Guid IdAcc { get; set; }
        [Required]
        [Column("User_Acc")]
        [StringLength(15)]
        public string UserAcc { get; set; }
        [Required]
        [Column("Pass_Acc")]
        [StringLength(15)]
        public string PassAcc { get; set; }
        [Column("Sex_Acc")]
        public bool SexAcc { get; set; }
        [Column("YearOfBirth_Acc")]
        public int YearOfBirthAcc { get; set; }
        [Column("Status_Acc")]
        public bool StatusAcc { get; set; }
    }
}
