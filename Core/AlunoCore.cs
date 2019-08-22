
using Core.Util;
using FluentValidation;
using Model;
using System;
using System.Linq;

namespace Core
{

    public class AlunoCore : AbstractValidator<Aluno>

    {
        private Aluno _aluno;
        public Armazenar db { get; set; }
        public AlunoCore(Aluno Aluno)
        {
            _aluno = Aluno;

            db = Arquivos<Armazenar>.Recuperar(db, "Alunos");
            if (db == null) db = new Armazenar();

            RuleFor(a => a.Nome).MinimumLength(3).NotNull().WithMessage("Nome inválido! é necesario ter no minimo tres letras! e nao pode ser nulo");
            RuleFor(a => a.Idade).GreaterThanOrEqualTo(7).NotEmpty().WithMessage("Idade inválida! é necessario ser maior que sete anos.");
            RuleFor(a => a.Documento).Length(9, 9).NotNull();
            RuleFor(a => a.Email).EmailAddress().NotNull().WithMessage("Email inválido!");
            RuleFor(a => a.Endereco).NotNull().WithMessage("Endereco Não pode ser nulo");
            RuleFor(a => a.Endereco.Bairro).MinimumLength(7).NotNull().WithMessage("Bairro inválido!");
            RuleFor(a => a.Endereco.NumeroCasa).GreaterThan(0).NotEmpty().WithMessage("Endereco da casa inválido.");
            RuleFor(a => a.Endereco.Cep).Length(8, 8).NotNull().WithMessage("Cep inválido! é necessario ter 8 digitos.");
            RuleFor(a => a.Genero.ToUpper()).NotNull().Must(a => a == "MASCULINO" || a == "FEMININO").WithMessage($"Campo {_aluno.Genero.GetType()} não pode ser nulo");
            RuleFor(a => a.NomePai).MinimumLength(3).NotNull().WithMessage("Nome inválido! é necesario ter no minimo tres letras! e nao pode ser nulo");
            RuleFor(a => a.NomeMae).MinimumLength(3).NotNull().WithMessage("Nome Inválido! é necesario ter no minimo tres letras! e nao pode ser nulo");
        }
        public AlunoCore()
        {

            db = Arquivos<Armazenar>.Recuperar(db, "Alunos");
            if (db == null) db = new Armazenar();
        }

        public dynamic Cadastrar()
        {
            var validar = Validate(_aluno);

            if (!validar.IsValid) return validar.Errors.Select(c => c.ErrorMessage).ToList();

            if (db.Alunos.Any(a => a.Documento == _aluno.Documento))
                return "Esse aluno ja está cadastrado!";


            var umAluno = _aluno;
            db.Alunos.Add(umAluno);

            Arquivos<Armazenar>.Salvar(db, "Alunos");
            return _aluno;

        }

        public dynamic Deletar(string id)
        {

            if (!db.Alunos.Any(a => a.Id == id))
                return "Esse aluno não está registrado";

            var umAluno = db.Alunos.Find(c => c.Id == id);

            db.Alunos.Remove(umAluno);
            Arquivos<Armazenar>.Salvar(db, "Alunos");
            return "Aluno removido!";
        }

        public dynamic BuscarTodos() => db.Alunos.OrderBy(a => a.Nome);


        public dynamic BuscarId(string id)
        {
            if (!db.Alunos.Any(a => a.Id == id))
                return "Não há um aluno com este Id";

            return db.Alunos.Find(a => a.Id == id);

        }


        public dynamic Atualizar(string id, Aluno aluno)
        {
            if (!db.Alunos.Any(a => a.Id == id))
                return "Não há um aluno com este Id";

            var umAluno = db.Alunos.Find(a => a.Id == id);


            if (aluno.Nome != null)
                umAluno.Nome = aluno.Nome;

            if (aluno.NomeMae != null)
                umAluno.NomeMae = aluno.NomeMae;

            if (aluno.NomePai != null)
                umAluno.NomePai = aluno.NomePai;

            if (aluno.Idade != 0)
                umAluno.Idade = aluno.Idade;

            if (aluno.Genero != null)
                umAluno.Genero = aluno.Genero;

            if (aluno.Endereco.NumeroCasa != 0)
                umAluno.Endereco.NumeroCasa = aluno.Endereco.NumeroCasa;

            if (aluno.Endereco.Complemento != null)
                umAluno.Endereco.Complemento = aluno.Endereco.Complemento;

            if (aluno.Endereco.Cep != null)
                umAluno.Endereco.Cep = aluno.Endereco.Cep;

            if (aluno.Endereco.Bairro != null)
                umAluno.Endereco.Bairro = aluno.Endereco.Bairro;

            if (aluno.Email != null)
                umAluno.Email = aluno.Email;


            Arquivos<Armazenar>.Salvar(db, "Alunos");

            return umAluno;
        }
    }
}
