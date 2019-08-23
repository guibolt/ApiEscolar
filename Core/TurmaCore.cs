
using Core.Util;
using FluentValidation;
using Model;
using System;
using System.Linq;

namespace Core
{
    public class TurmaCore: AbstractValidator<Turma>
    {
        private Turma _turma;
        public Armazenar db { get; set; }
        public TurmaCore()
        {
            db = Arquivos.Recuperar(db);
            if (db == null) db = new Armazenar();

            RuleFor(c => c.Alunos).NotNull().NotEmpty();
            RuleFor(c => c.Professores).NotNull();
            RuleFor(c => c.Id).NotEmpty();
        }
        public TurmaCore(Turma turma)
        {
            _turma = turma;
            db = Arquivos.Recuperar(db);
            if (db == null) db = new Armazenar();
        }


        public dynamic Cadastrar()
        {
            var validar = Validate(_turma);

            if (!validar.IsValid) return (false, validar.Errors.Select(c => c.ErrorMessage).ToList());

            if (db.Turmas.Any(a => a.Id == _turma.Id))
                return (false, "Essa turma ja está cadastrada!");


            db.Turmas.Add(_turma);

            Arquivos.Salvar(db);

            return (true, _turma);
        }

        public dynamic CadastrarAlunoProfessor(int idTurma, string idProfessor)
        {
            if (!db.Turmas.Any(t => t.Id == idTurma))
                return (false, "Não há uma turma registrada com este Id");

            var umaTurma = db.Turmas.Find(t => t.Id == idTurma);

            if(!db.lstProfessores.Any(p => p.Id == idProfessor))
                return (false, "Não há um professor registrado com este Id");

            var umProfessor = db.lstProfessores.Find(p => p.Id == idProfessor);

            umaTurma.Professores.Add(umProfessor);
            Arquivos.Salvar(db);

            return (true, umaTurma);

        }

        public dynamic CadastrarAlunoTurma(int idTurma, string idAluno)
        {
        

            if (!db.Turmas.Any(t => t.Id == idTurma))
                return (false, "Não há uma turma registrada com este Id");

            var umaTurma = db.Turmas.Find(t => t.Id == idTurma);


            if (!db.Alunos.Any(a => a.Id == idAluno))
                return (false, "Não há um aluno registrado com este id");

            var umAluno = db.Alunos.Find(a => a.Id == idAluno);


            umaTurma.Alunos.Add(umAluno);

            Arquivos.Salvar(db);

            return (true, umaTurma);
        }

        public dynamic BuscarId(int id)
        {
            if (!db.Turmas.Any(a => a.Id == id))
                return (false, "Não há uma turma  com este Id");

            return (true, db.Turmas.Find(a => a.Id == id));
        }

        public dynamic BuscarTodos() => db.Turmas.OrderBy(c => c.Id).ToList();


        public dynamic Atualizar(int id , Turma turma)
        {

            if (!db.Turmas.Any(a => a.Id == id))
                return (false, "Não há uma turma  com este Id");


            var umaTurma = db.Turmas.Find(i => i.Id == id);


            if (turma.Professores != null)
                umaTurma.Professores = turma.Professores;
            if (turma.Alunos != null)
                umaTurma.Alunos = turma.Alunos;

            Arquivos.Salvar(db);
            return (true, umaTurma);

        }

        public dynamic Deletar(int id)
        {

            if (!db.Turmas.Any(c => c.Id == id))
                return (false, "Não há uma turma com este Id");

            var umaTurma = db.Turmas.Find(t => t.Id == id);


            db.Turmas.Remove(umaTurma);


            Arquivos.Salvar(db);
            return (true, umaTurma);


        }
    }
}
