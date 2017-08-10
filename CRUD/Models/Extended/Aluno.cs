using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    [MetadataType(typeof(AlunoMetadata))]
    public partial class Aluno
    {

    }
    public class AlunoMetadata
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = "Preencha seu nome")]
        public string Nome { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha seu sobrenome")]
        public string Sobrenome { get; set; }
    }
}