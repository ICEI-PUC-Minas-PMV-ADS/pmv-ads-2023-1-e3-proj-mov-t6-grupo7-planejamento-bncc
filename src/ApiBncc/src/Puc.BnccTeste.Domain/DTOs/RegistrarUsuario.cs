namespace Puc.BnccTeste.Api.DTOs
{
    public class RegistrarUsuario
    {
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string Tipo { get; set; } = "Cliente";
        public bool Ativo { get; set; } = false;

    }
}
