using Puc.BnccTeste.Api.DTOs;
using Puc.BnccTeste.Domain.Entidade;
using Puc.BnccTeste.Domain.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Service.Interface
{
    public interface IUsuarioService : IDisposable
    {
        IEnumerable<Usuario> ListarUsuariosAtivos();
        bool Inserir(Usuario usuario);
        bool Atualizar(Usuario usuario);
        dynamic Login(LoginUsuario usuario);
        dynamic Registrar(Usuario usuario);
        bool Deletar(int id);

    }
}
