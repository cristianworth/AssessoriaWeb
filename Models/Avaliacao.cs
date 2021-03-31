using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssessoriaWeb.Models
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        [Key]
        public int ava_id { get; set; }
        public DateTime ava_data { get; set; }
        public decimal ava_peso { get; set; }
        public decimal ava_gorduraVisceral { get; set; }
        public decimal ava_gorduraCorporal { get; set; }
        public decimal ava_bioimpedancia { get; set; }
        public int atl_id { get; set; }
        public Atleta Atleta { get; set; }
    }
}