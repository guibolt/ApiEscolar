
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
            db = Arquivos<Armazenar>.Recuperar(db, "Turmas");
            if (db == null) db = new Armazenar();

            RuleFor(c => c.Alunos).NotNull().NotEmpty();
            RuleFor(c => c.Professores).NotNull();
            RuleFor(c => c.Id).NotEmpty();
        }
        public TurmaCore(Turma turma)
        {
            _turma = turma;
            db = Arquivos<Armazenar>.Recuperar(db, "Turmas");
            if (db == null) db = new Armazenar();
        }


        public dynamic Cadastrar()
        {
            var validar = Validate(_turma);

            if (!validar.IsValid) return (false, validar.Errors.Select(c => c.ErrorMessage).ToList());

            if (db.Turmas.Any(a => a.Id == _turma.Id))
                return (false, "Essa turma ja está cadastrada!");


            db.Turmas.Add(_turma);

            Arquivos<Armazenar>.Salvar(db, "Turmas");

            return (true, _turma);
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

            Arquivos<Armazenar>.Salvar(db, "Turmas");
            return (true, umaTurma);

        }
    }
}
