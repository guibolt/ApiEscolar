using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ApiForSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Aluno Aluno)
        {
            var Core = new AlunoCore(Aluno).Cadastrar();
            return (Core is false) ? BadRequest(Core) : Ok(Core);
         }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Core = new AlunoCore().BuscarId(id);
            return Ok(Core);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(new AlunoCore().BuscarTodos());

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Aluno aluno, string id)
        {
            var Core = new AlunoCore().Atualizar(id, aluno);
            return (Core is false) ? BadRequest(Core) : Ok(Core);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Core = new AlunoCore().Deletar(id);
            return (Core is false) ? BadRequest(Core) : Ok(Core);
        }
    }
}