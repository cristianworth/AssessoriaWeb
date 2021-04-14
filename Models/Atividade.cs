using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Atividade")]
    public class Atividade
    {
        [Key]
        [Display(Name = "Atividade")]
        public int ati_id { get; set; }
        [Display(Name = "Descrição")]
        public string ati_descricao { get; set; }
        [Display(Name = "Observação")]
        public string atl_observacao { get; set; }
        public ICollection<Treinamento> Treinamentos { get; set; }
        public Atividade()
        {
            Treinamentos = new HashSet<Treinamento>();
        }
    }
}