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
        private Armazenar db = new Armazenar();

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

            if (!results.IsValid) return (false,results.Errors.Select(m => m.ErrorMessage).ToList());

            db = Arquivos<Armazenar>.Recuperar(db,"Professores");

            if (!db.Prfessores.Exists(e => e.Documento.Equals(_professor.Documento)))
            {
                db.Prfessores.Add(_professor);
                Arquivos<Armazenar>.Salvar(db, "Professores");
                return _professor;
            }
                return  "Já existe um professor com esse documento com esse ID." ;

        }
        public dynamic BuscarId(string Id)
        {
           
            db = Arquivos<Armazenar>.Recuperar(db, "Professores");

            if (db.Prfessores.Exists(e => e.Id.Equals(Id))) return db.Prfessores.Single(e => e.Id.Equals(Id));
         
            return "Não existe nenhum professor com esse ID por favor insira um id válido.";
        }

        public dynamic BuscarTodos()
        {

            db = Arquivos<Armazenar>.Recuperar(db, "Professores");

            if (db.Prfessores.Any()) return db.Prfessores;

            return "Não existe nenhum professor cadastrado, Por favor cadastre.";
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
