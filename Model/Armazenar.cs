using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Armazenar
    {
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public List<Professor> Prfessores { get; set; } = new List<Professor>();
        public List<Turma> Turmas { get; set; } = new List<Turma>();
    }
}
