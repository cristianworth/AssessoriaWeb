using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Turma")]
    public class Turma
    {
        [Key]
        [Display(Name = "Turma")]
        public int trm_id { get; set; }
        [Display(Name = "Descrição")]
        public string trm_descricao { get; set; }
        [Display(Name = "Observação")]
        public string trm_observacao { get; set; }
        [Display(Name = "Hora Inicial")]
        public TimeSpan trm_HoraInicial { get; set; }
        [Display(Name = "Hora Final")]
        public TimeSpan trm_HoraFinal { get; set; }

        [Display(Name = "Assessor")]
        public int ass_id { get; set; }
        [Display(Name = "Assessor")]
        public Assessor Assessor { get; set; }




        [Display(Name = "Atletas")]
        public ICollection<AtletaTurma> AtletaTurmas { get; set; }
        public Turma()
        {
            AtletaTurmas = new HashSet<AtletaTurma>();
        }
    }
}