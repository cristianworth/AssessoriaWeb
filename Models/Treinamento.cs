using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Treinamento")]
    public class Treinamento
    {
        [Key]
        [Display(Name = "Treinamento")]
        public int tre_id { get; set; }
        [Display(Name = "Data"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime tre_data { get; set; }
        [Display(Name = "Hora")]
        public TimeSpan tre_hora { get; set; }
        [Display(Name = "Valor")]
        public decimal tre_valor { get; set; }
        [Display(Name = "Descrição")]
        public string tre_descricao { get; set; }

        [Display(Name = "Assessor")]
        public int ass_id { get; set; }
        [Display(Name = "Assessor")]
        public Assessor Assessor { get; set; }

        [Display(Name = "Atleta")]
        public int atl_id { get; set; }
        [Display(Name = "Atleta")]
        public Atleta Atleta { get; set; }


        [Display(Name = "Atividades")]
        public ICollection<AtividadeTreinamento> AtividadeTreinamentos { get; set; }
        public Treinamento()
        {
            AtividadeTreinamentos = new HashSet<AtividadeTreinamento>();
        }
    }
}