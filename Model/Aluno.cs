using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class Aluno: Base
    {
        public string NomeMae { get; set; }
        public string NomePai { get; set; }

        public Aluno() { }
        public void TurmaAluno(Aluno aluno)
        {
            if (aluno.Nome != null)
                Nome = aluno.Nome;

            if (aluno.NomeMae != null)
                NomeMae = aluno.NomeMae;

            if (aluno.NomePai != null)
                NomePai = aluno.NomePai;

            if (aluno.Idade != 0)
                Idade = aluno.Idade;

            if (aluno.Genero != null)
                Genero = aluno.Genero;

            if (aluno.Endereco != null)
                Endereco = aluno.Endereco;

            if (aluno.Endereco.NumeroCasa != 0)
                Endereco.NumeroCasa = aluno.Endereco.NumeroCasa;

            if (aluno.Endereco.Complemento != null)
                Endereco.Complemento = aluno.Endereco.Complemento;

            if (aluno.Endereco.Cep != null)
                Endereco.Cep = aluno.Endereco.Cep;

            if (aluno.Endereco.Bairro != null)
                Endereco.Bairro = aluno.Endereco.Bairro;

            if (aluno.Email != null)
                Email = aluno.Email;
        }
    }
}
 