using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("AtividadeTreinamento")]
    public class AtividadeTreinamento
    {
        [Key]
        [Display(Name = "AtividadeTreinamento")]
        public int atr_id { get; set; }
        [Display(Name = "Atividade")]
        public int ati_id { get; set; }
        [Display(Name = "Treinamento")]
        public int tre_id { get; set; }
        public AtividadeTreinamento()
        {
            
        }
    }
}