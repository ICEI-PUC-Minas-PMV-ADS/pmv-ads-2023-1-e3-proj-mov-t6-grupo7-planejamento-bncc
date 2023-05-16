using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Domain.Entidade
{
    public class Usuario
    {
        [Key]
        public int Usuario_Id { get; set; }
        public string Nome { get; set; } = "";
        public string Tipo { get; set; } = "Cliente";
        public bool Ativo { get; set; } = false;
        public string Email { get; set; } = "";
        public string Senha { get; set; } = "";
   
    }
}
