using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("TipoPessoa")]
    public class TipoPessoa
    {
        [Key]
        public int tpp_id { get; set; }
        [Display(Name = "Tipo")]
        public string tpp_descricao { get; set; }
        public ICollection<Pessoa> Pessoas { get; set; }
        public TipoPessoa()
        {
            Pessoas = new HashSet<Pessoa>();
        }
    }
}