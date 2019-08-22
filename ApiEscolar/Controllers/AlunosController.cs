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
            return Ok(Core);

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
        public async Task<IActionResult> Put([FromBody]Aluno aluno, string id) => Ok(new AlunoCore().Atualizar(id,aluno));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Accepted(new AlunoCore().Deletar(id));
    }
}