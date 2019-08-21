using Core.Util;
using FluentValidation;
using Model;
using System;
using System.Linq;

namespace Core
{
    public class AlunoCore :  AbstractValidator<Aluno>

    {
        private Aluno _aluno;
        public AlunoCore(Aluno Aluno)
        {
            _aluno = Aluno;

            RuleFor(a => a.Nome).MinimumLength(3).NotNull().WithMessage("Nome inválido! é necesario ter no minimo tres letras! e nao pode ser nulo");
            RuleFor(a => a.Idade).GreaterThanOrEqualTo(6).NotEmpty().WithMessage("Idade inválida! é necessario ser maior que sete anos.");
            RuleFor(a => a.Documento).Length(9, 9).NotNull();
            RuleFor(a => a.Email).EmailAddress().NotNull().WithMessage("Email inválido!");
            RuleFor(a => a.Endereco.Bairro).MinimumLength(6).NotNull().WithMessage("Bairro inválido!");
            RuleFor(a => a.Endereco.NumeroCasa).GreaterThan(0).NotNull().WithMessage("Endereco da casa inválido.");
            RuleFor(a => a.Endereco.Cep).Length(8, 8).NotNull().WithMessage("Cep inválido! é necessario ter 8 digitos.");
            RuleFor(a => a.Genero.ToUpper()).NotNull().Must(a => a == "MASCULINO" || a == "FEMININO").WithMessage($"Campo {_aluno.Genero.GetType()} não pode ser nulo");
            RuleFor(a => a.NomePai).MinimumLength(3).NotNull().WithMessage("Nome inválido! é necesario ter no minimo tres letras! e nao pode ser nulo");
            RuleFor(a => a.NomeMae).MinimumLength(3).NotNull().WithMessage("Nome Inválido! é necesario ter no minimo tres letras! e nao pode ser nulo");
        }
        public AlunoCore() {}

        public dynamic Cadastrar()
        {

            var validar = Validate(_aluno);

            if (!validar.IsValid) return validar.Errors.Select(c => c.ErrorMessage).ToList();

            var Alunos = new Armazenar().Alunos;
          Arquivos<Aluno>.Recuperar(Alunos,"Alunos");


            if (Alunos.Any(a => a.Documento == _aluno.Documento))
                return 
                
            



        }

        public void Deletar()
        {
            throw new NotImplementedException();
        }

        public Aluno Atualizar()
        {
            throw new NotImplementedException();
        }

        public Aluno BuscarId()
        {
            throw new NotImplementedException();
        }

        public Aluno BuscarTodos()
        {
            throw new NotImplementedException();
        }

     
    }
}
