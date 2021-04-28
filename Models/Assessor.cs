using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Assessor")]
    public class Assessor
    {
        [Key]
        [Display(Name = "Assessor")]
        public int ass_id { get; set; }
        [Display(Name = "Tipo")]
        public string ass_tipo { get; set; }
        [Display(Name = "Pessoa")]
        public int pes_id { get; set; }
        [Display(Name = "Pessoa")]
        public Pessoa Pessoa { get; set; }
        public ICollection<Treinamento> Treinamentos { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public Assessor()
        {
            Treinamentos = new HashSet<Treinamento>();
            Turmas = new HashSet<Turma>();
        }
    }
}