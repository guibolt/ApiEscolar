using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interface
{
    public interface ICore<T>

    {
        T Cadastrar();
        T BuscarId();
        T BuscarTodos();
        void Deletar();
        T Atualizar();

    }
}
