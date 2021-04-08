using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key, Display(Name = "Endereco")]
        public int end_id { get; set; }
        [Display(Name = "Bairo")]
        public string end_bairro { get; set; }
        [Display(Name = "Número")]
        public string end_numero { get; set; }
        [Display(Name = "Logradouro")]
        public string end_logradouro { get; set; }
        [Display(Name = "Complemento")]
        public string end_complemento { get; set; }
        [Display(Name = "CEP")]
        public string end_cep { get; set; }
        [Display(Name = "Pessoa")]
        public int pes_id { get; set; }
        [Display(Name = "Pessoa")]
        public Pessoa Pessoa { get; set; }

        public Endereco()
        {
            
        }
    }
}