using System.Collections.Generic;

namespace Model
{
    public class Turma
    {
        public int Id { get; set; }
        public List<Professor> Professores { get; set; } = new List<Professor>();
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}