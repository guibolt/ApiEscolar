﻿using Core.Util;
using FluentValidation;
using Model;
using System.Linq;

namespace Core
{
    public class ProfessorCore : AbstractValidator<Professor> 
    {
        private Professor _professor { get; set; }
        private Armazenar Db { get; set; } 

        public ProfessorCore(Professor professor)
        {
            _professor = professor;
            Db = Arquivos.Recuperar(Db);
            if (Db == null) Db = new Armazenar();
            RuleFor(e => e.Nome).MinimumLength(3).NotNull().WithMessage("O nome deve ser preenchido e deve ter o mínimo de 3 caracteres.");
            RuleFor(e => e.Genero).NotNull().MinimumLength(5).WithMessage("O genero não pode ser nulo e deve conter no mínimo 3 caracteres.");
            RuleFor(e => e.Email).NotNull().EmailAddress().WithMessage("E-mail não pode ser nulo e deve ser um endereço de e-mail válido.");
            RuleFor(e => e.Documento).NotNull().WithMessage("O documento não pode ser nulo.");
            RuleFor(a => a.Endereco.Bairro).MinimumLength(6).NotNull().WithMessage("Bairro inválido!");
            RuleFor(a => a.Endereco.NumeroCasa).GreaterThan(0).NotNull().WithMessage("Endereço da casa inválido.");
            RuleFor(a => a.Endereco.Cep).Length(8, 8).NotNull().WithMessage("CEP Inválido! é necessario ter 8 digitos.");
            RuleFor(e => e.Materias).NotEmpty().WithMessage("Por favor insira ao menos uma matéria.");
        }

        public ProfessorCore(){
             Db = Arquivos.Recuperar(Db);
            if (Db == null) Db = new Armazenar();
        }

        public dynamic Cadastrar()
        {
            var results = Validate(_professor);

            if (!results.IsValid) return (false,results.Errors.Select(m => m.ErrorMessage).ToList());

      

            if (!Db.lstProfessores.Exists(e => e.Documento.Equals(_professor.Documento)))
            {
                Db.lstProfessores.Add(_professor);
                Arquivos.Salvar(Db);

                return _professor;
            }
                return  "Já existe um professor com esse documento com esse ID." ;

        }
        public dynamic BuscarId(string Id)
        {
            if (Db.lstProfessores.Exists(e => e.Id.Equals(Id))) return Db.lstProfessores.Single(e => e.Id.Equals(Id));
            return "Não existe nenhum professor com esse ID por favor insira um id válido.";
        }

        public dynamic BuscarTodos()
        {

            if (Db.lstProfessores.Any()) return Db.lstProfessores;

            return "Não existe nenhum professor cadastrado, Por favor cadastre.";
        }
        public dynamic Atualizar(string id, Professor professor)
        {
            if (!Db.lstProfessores.Any(a => a.Id == id))
                return "Não há um professor com este Id";

            var umProfessor = Db.lstProfessores.Find(a => a.Id == id);

            umProfessor.TurmaProf(professor);

            Arquivos.Salvar(Db);

            return umProfessor;
        }
        public dynamic Deletar(string Id)
        {

            if (Db.lstProfessores.Exists(e => e.Id.Equals(Id))) {
                Db.lstProfessores.Remove(Db.lstProfessores.Single(e => e.Id.Equals(Id)));
                Arquivos.Salvar(Db);

                return "Professor deletado com Sucesso.";
            }else if (Db.lstProfessores.Any(e => e.Id.Equals(Id))) return "Não existe nenhum Professor com esse ID por favor tente novamente com um ID válido.  ";

            return "Não existe nenhum Professor Cadastrado para poder ser deletado.";
        }
    }
}
