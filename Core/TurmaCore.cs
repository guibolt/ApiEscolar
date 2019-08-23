﻿
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

            RuleFor(c => c.Alunos).NotNull().NotEmpty().ForEach(d=>d.Must(ValidaAluno));
            RuleFor(c => c.Professores).NotNull().NotEmpty().ForEach(d => d.Must(ValidaProf));
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

            _turma.Alunos.ForEach(c => c.TurmaAluno(db.Alunos.Single(d => d.Id == c.Id)));
            _turma.Professores.ForEach(c => c.TurmaProf(db.Prfessores.Single(d => d.Id == c.Id)));

            db.Turmas.Add(_turma);

            Arquivos.Salvar(db);

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

        private bool ValidaProf(Professor professor)
        {
            if (db.Prfessores.SingleOrDefault(c => c.Id == professor.Id) == null) return false;
            return true;
        }
        private bool ValidaAluno(Aluno aluno)
        {
            if (db.Alunos.SingleOrDefault(c => c.Id == aluno.Id) == null) return false;
            return true;
        }
    }
}
