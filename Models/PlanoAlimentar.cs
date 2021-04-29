using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("PlanoAlimentar")]
    public class PlanoAlimentar
    {
        [Key]
        [Display(Name = "PlanoAlimentar")]
        public int pln_id { get; set; }
        [Display(Name = "Data Inicial")]
        public DateTime pln_datainicial { get; set; }
        [Display(Name = "Data Final")]
        public DateTime pln_datafinal { get; set; }
        [Display(Name = "Plano Alimentar")]
        public string pla_plano { get; set; }


        [Display(Name = "Atleta")]
        public int atl_id { get; set; }
        [Display(Name = "Atleta")]
        public Atleta Atleta { get; set; }


        [Display(Name = "Nutricionista")]
        public int nut_id { get; set; }
        [Display(Name = "Nutricionista")]
        public Nutricionista Nutricionista { get; set; }


        public PlanoAlimentar()
        {
        }
    }
}