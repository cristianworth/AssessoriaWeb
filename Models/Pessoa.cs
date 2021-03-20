using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int pes_id { get; set; }
        public string pes_nome { get; set; }
        public string pes_cpf { get; set; }
        public DateTime pes_datanascimento { get; set; }
        public string pes_telefone { get; set; }
        public char pes_sexo { get; set; }
        public string pes_login { get; set; }
        public string pes_senha { get; set; }
        public int pes_tipo { get; set; }
    }
}