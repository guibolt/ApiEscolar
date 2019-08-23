using Core.Util;
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
            Db = Arquivos<Armazenar>.Recuperar(Db);
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
             Db = Arquivos<Armazenar>.Recuperar(Db);
            if (Db == null) Db = new Armazenar();
        }

        public dynamic Cadastrar()
        {
            var results = Validate(_professor);

            if (!results.IsValid) return (false,results.Errors.Select(m => m.ErrorMessage).ToList());

      

            if (!Db.Prfessores.Exists(e => e.Documento.Equals(_professor.Documento)))
            {
                Db.Prfessores.Add(_professor);
                Arquivos<Armazenar>.Salvar(Db);
                return _professor;
            }
                return  "Já existe um professor com esse documento com esse ID." ;

        }
        public dynamic BuscarId(string Id)
        {
            if (Db.Prfessores.Exists(e => e.Id.Equals(Id))) return Db.Prfessores.Single(e => e.Id.Equals(Id));
         
            return "Não existe nenhum professor com esse ID por favor insira um id válido.";
        }

        public dynamic BuscarTodos()
        {

            if (Db.Prfessores.Any()) return Db.Prfessores;

            return "Não existe nenhum professor cadastrado, Por favor cadastre.";
        }
        public dynamic Atualizar(string id, Professor professor)
        {
            if (!Db.Prfessores.Any(a => a.Id == id))
                return "Não há um professor com este Id";

            var umProfessor = Db.Prfessores.Find(a => a.Id == id);


            if (professor.Nome != null)
                umProfessor.Nome = professor.Nome;

            if (professor.Idade != 0)
                umProfessor.Idade = professor.Idade;

            if (professor.Genero != null)
                umProfessor.Genero = professor.Genero;

            if (professor.Endereco.NumeroCasa != 0)
                umProfessor.Endereco.NumeroCasa = professor.Endereco.NumeroCasa;

            if (professor.Endereco.Complemento != null)
                umProfessor.Endereco.Complemento = professor.Endereco.Complemento;

            if (professor.Endereco.Cep != null)
                umProfessor.Endereco.Cep = professor.Endereco.Cep;

            if (professor.Endereco.Bairro != null)
                umProfessor.Endereco.Bairro = professor.Endereco.Bairro;

            if (professor.Email != null)
                umProfessor.Email = professor.Email;

            if (!professor.Materias.Any())
                umProfessor.Materias = professor.Materias;

            if (professor.Salario == 0.0)
                umProfessor.Salario = professor.Salario;

            Arquivos<Armazenar>.Salvar(Db);

            return umProfessor;
        }
        public dynamic Deletar(string Id)
        {
            if (Db.Prfessores.Exists(e => e.Id.Equals(Id))) {
                Db.Prfessores.Remove(Db.Prfessores.Single(e => e.Id.Equals(Id)));
                Arquivos<Armazenar>.Salvar(Db);
                return "Professor deletado com Sucesso.";
            }else if (Db.Prfessores.Any()) return "Não existe nenhum Professor Cadastrado para poder ser deletado.";

            return "Não existe nenhum Professor com esse ID por favor tente novamente com um ID válido.";
        }
    }
}
