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
        [Key,Display(Name = "Pessoa")]
        public int pes_id { get; set; }
        [Display(Name = "Nome")]
        public string pes_nome { get; set; }
        [Display(Name = "CPF")]
        public string pes_cpf { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime pes_datanascimento { get; set; }
        [Display(Name = "Telefone")]
        public string pes_telefone { get; set; }
        [Display(Name = "Sexo")]
        public char pes_sexo { get; set; }
        [Display(Name = "Login")]
        public string pes_login { get; set; }
        [Display(Name = "Senha")]
        public string pes_senha { get; set; }
        public int tpp_id { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public ICollection<Atleta> Atletas { get; set; }
        public ICollection<Assessor> Assessores { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }

        public Pessoa()
        {
            Atletas = new HashSet<Atleta>();
            Assessores = new HashSet<Assessor>();
            Enderecos = new HashSet<Endereco>();
        }
    }
}