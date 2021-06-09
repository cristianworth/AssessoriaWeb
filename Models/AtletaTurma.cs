using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("AtletaTurma")]
    public class AtletaTurma
    {
        [Display(Name = "Atleta")]
        public int atl_id { get; set; }
        [Display(Name = "Turma")]
        public int trm_id { get; set; }
        public AtletaTurma()
        {
            
        }
    }
}