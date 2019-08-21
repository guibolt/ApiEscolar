using Core.Util;
using FluentValidation;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class ProfessorCore : AbstractValidator<Professor> 
    {
        private Professor _professor { get; set; }
        public ProfessorCore(Professor professor)
        {
            _professor = professor;

            RuleFor(e => e.Nome).MinimumLength(3).NotNull().WithMessage("O nome deve ser preenchido e deve ter o mínimo de 3 caracteres.");
            RuleFor(e => e.Genero).NotNull().MinimumLength(5).WithMessage("O genero não pode ser nulo e deve conter no mínimo 3 caracteres.");
            RuleFor(e => e.Email).NotNull().EmailAddress().WithMessage("E-mail não pode ser nulo e deve ser um endereço de e-mail válido.");
            RuleFor(e => e.Documento).NotNull().WithMessage("O documento não pode ser nulo.");
            //RuleFor(a => a.Endereco.Bairro).MinimumLength(6).NotNull().WithMessage("Bairro inválido!");
            //RuleFor(a => a.Endereco.NumeroCasa).GreaterThan(0).NotNull().WithMessage("Endereço da casa inválido.");
            //RuleFor(a => a.Endereco.Cep).Length(8, 8).NotNull().WithMessage("CEP Inválido! é necessario ter 8 digitos.");
        }

        public ProfessorCore(){}

        public dynamic Cadastrar()
        {
            var results = Validate(_professor);

            if (!results.IsValid) return results.Errors.Select(m => m.ErrorMessage).ToList();

            var db = new Armazenar();
            Arquivos<Armazenar>.Recuperar(db,"Professores");
            if (!db.Prfessores.Exists(e => e.Id.Equals(_professor.Id)))
            {
                db.Prfessores.Add(_professor);
                Arquivos<Armazenar>.Salvar(db, "Professores");
                return _professor;
            }
            else
                return  "Já existe um cliente com esse ID." ;

        }
        public dynamic BuscarId()
        {
            throw new System.NotImplementedException();
        }

        public dynamic BuscarTodos()
        {
            throw new System.NotImplementedException();
        }
        public dynamic Atualizar()
        {
            throw new System.NotImplementedException();
        }  
        public void Deletar()
        {
            throw new System.NotImplementedException();
        }
    }
}
