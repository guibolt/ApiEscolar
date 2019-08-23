using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Professor : Base

    {
        public double Salario { get; set; }
        public List<Materia> Materias { get; set; } = new List<Materia>();

        public Professor() { }
        public void TurmaProf(Professor professor)
        {
            if (professor.Nome != null)
                Nome = professor.Nome;

            if (professor.Idade != 0)
                Idade = professor.Idade;

            if (professor.Genero != null)
                Genero = professor.Genero;

            if (professor.Endereco != null)
                Endereco = professor.Endereco;

            if (professor.Endereco.NumeroCasa != 0)
                Endereco.NumeroCasa = professor.Endereco.NumeroCasa;

            if (professor.Endereco.Complemento != null)
                Endereco.Complemento = professor.Endereco.Complemento;

            if (professor.Endereco.Cep != null)
                Endereco.Cep = professor.Endereco.Cep;

            if (professor.Endereco.Bairro != null)
                Endereco.Bairro = professor.Endereco.Bairro;

            if (professor.Email != null)
                Email = professor.Email;

            if (!professor.Materias.Any())
                Materias = professor.Materias;

            if (professor.Salario == 0.0)
                Salario = professor.Salario;
        }
    }
}
