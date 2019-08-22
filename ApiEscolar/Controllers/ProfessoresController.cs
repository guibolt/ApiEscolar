using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ApiForSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Professor professor)
        {
            var cadastro = new ProfessorCore(professor).Cadastrar();
            return (cadastro is string || cadastro is List<string>) ? BadRequest(cadastro) : Ok(cadastro);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Professor = new ProfessorCore().BuscarId(id);
            return (Professor is string) ? BadRequest(Professor) : Ok(Professor);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Todos = new ProfessorCore().BuscarTodos();
            return (Todos.GetType() == typeof(string)) ? BadRequest(Todos) : Ok(Todos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Professor professor, string id)
        {
            var atualizar = new ProfessorCore().Atualizar(id, professor);
            return (atualizar is string)? BadRequest(atualizar) : Ok(atualizar);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var delete = new ProfessorCore().Deletar(id);
            return (delete != "Professor deletado com Sucesso.") ? BadRequest(delete) : Ok(delete);
        }
    }
}