using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ApiEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
          [HttpPost]
        public async Task<IActionResult> Post([FromBody] Turma turma)
        {
            var Core = new TurmaCore(turma).Cadastrar();
            return (Core is false) ? BadRequest(Core) : Ok(Core);

        }
        [HttpPost("{idTurma}/alunos/{idAluno}")]
        public async Task<IActionResult> CadastarUmAluno([FromQuery] int idTurma, [FromQuery]string idAluno)
        {
            var Core = new TurmaCore().CadastrarAlunoTurma(idTurma,idAluno);
            return (Core is false) ? BadRequest(Core) : Ok(Core);
        }

        [HttpPost("{idTurma}/professores/{idProfessor}")]
        public async Task<IActionResult> CadastarUmProfessor([FromQuery] int idTurma, [FromQuery]string idProfessor)
        {
            var Core = new TurmaCore().CadastrarAlunoProfessor(idTurma, idProfessor);
           return (Core is false) ? BadRequest(Core) : Ok(Core);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Core = new TurmaCore().BuscarId(id);
            return Ok(Core);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(new TurmaCore().BuscarTodos());

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Turma turma, int id)
        {
            var Core = new TurmaCore().Atualizar(id, turma);
            return (Core is false) ? BadRequest(Core) : Ok(Core);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Core = new TurmaCore().Deletar(id);
            return (Core is false) ? BadRequest(Core) : Ok(Core);
        }
    }
}