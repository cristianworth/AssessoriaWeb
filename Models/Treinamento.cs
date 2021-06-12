using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessoriaWeb.Models
{
    [Table("Treinamento")]
    public class Treinamento
    {
        [Key]
        [Display(Name = "Treinamento")]
        public int tre_id { get; set; }
        [Display(Name = "Data"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Remote(action: "ValidateDate", controller: "Treinamentoes", AdditionalFields = nameof(tre_hora) + "," + nameof(atl_id) + "," + nameof(tre_id))]
        public DateTime tre_data { get; set; }
        [Display(Name = "Hora")]
        [Remote(action: "ValidateHour", controller: "Treinamentoes")]
        public TimeSpan tre_hora { get; set; }
        [Display(Name = "Valor")]
        public decimal tre_valor { get; set; }
        [Display(Name = "Descrição")]
        public string tre_descricao { get; set; }

        [Display(Name = "Assessor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int ass_id { get; set; }
        [Display(Name = "Assessor")]
        public Assessor Assessor { get; set; }

        [Display(Name = "Atleta")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int atl_id { get; set; }
        public Atleta Atleta { get; set; }


        [Display(Name = "Atividades")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public ICollection<AtividadeTreinamento> AtividadeTreinamentos { get; set; }
        public Treinamento()
        {
            AtividadeTreinamentos = new HashSet<AtividadeTreinamento>();
        }
    }
}