using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        [Key]
        public int ava_id { get; set; }
        [Display(Name = "Data")]
        public DateTime ava_data { get; set; }
        [Display(Name = "Peso")]
        public decimal ava_peso { get; set; }
        [Display(Name = "Gordura Visceral")]
        public decimal ava_gorduraVisceral { get; set; }
        [Display(Name = "Gordura Corporal")]
        public decimal ava_gorduraCorporal { get; set; }
        [Display(Name = "Bioimpedancia")]
        public decimal ava_bioimpedancia { get; set; }
        [Display(Name = "Atleta")]
        public int atl_id { get; set; }
        [Display(Name = "Atleta")]
        public Atleta Atleta { get; set; }
    }
}