using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Atleta")]
    public class Atleta
    {
        [Key]
        [Display(Name = "Atleta")]
        public int atl_id { get; set; }
        [Display(Name = "Categoria")]
        public string atl_categoria { get; set; }
        [Display(Name = "Observação")]
        public string atl_observacao { get; set; }
        [Display(Name = "Pessoa")]
        public int pes_id { get; set; }
        [Display(Name = "Pessoa")]
        public Pessoa Pessoa { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public ICollection<Treinamento> Treinamentos { get; set; }
        public ICollection<PlanoAlimentar> PlanosAlimentares { get; set; }




        [Display(Name = "Turmas")]
        public ICollection<AtletaTurma> AtletaTurmas { get; set; }
        public Atleta()
        {
            Avaliacoes = new HashSet<Avaliacao>();
            Treinamentos = new HashSet<Treinamento>();
            PlanosAlimentares = new HashSet<PlanoAlimentar>();
            AtletaTurmas = new HashSet<AtletaTurma>();
        }
    }
}