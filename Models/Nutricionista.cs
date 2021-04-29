using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Nutricionista")]
    public class Nutricionista
    {
        [Key]
        [Display(Name = "Nutricionista")]
        public int nut_id { get; set; }
        [Display(Name = "CRN")]
        public string nut_crn { get; set; }
        [Display(Name = "CNPJ")]
        public string nut_cnpj { get; set; }


        [Display(Name = "Pessoa")]
        public int pes_id { get; set; }
        [Display(Name = "Pessoa")]
        public Pessoa Pessoa { get; set; }


        public ICollection<PlanoAlimentar> PlanosAlimentares { get; set; }
        public Nutricionista()
        {
            PlanosAlimentares = new HashSet<PlanoAlimentar>();
        }
    }
}