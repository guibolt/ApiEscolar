using System;

namespace Model
{
    public class Base
    {
        public string Nome { get; set; }
        public string Id = Guid.NewGuid().ToString().Substring(0, 6);
        public string Documento { get; set; }
        public int Idade { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Endereco Endereco { get; set; } = new Endereco();
    }
}
