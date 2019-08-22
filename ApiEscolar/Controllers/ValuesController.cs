using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ApiEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var Todos = new ProfessorCore().BuscarTodos();
 
            return (Todos.GetType() == typeof(string)) ? BadRequest(Todos) : Ok(Todos);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var Professor = new ProfessorCore().BuscarId(id);
            return (Professor.GetType() == typeof(string)) ? BadRequest(Professor) : Ok(Professor);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            var cadastro = new ProfessorCore(professor).Cadastrar();
            return (cadastro.GetType() == typeof(string)) ? BadRequest(cadastro) : Ok(cadastro);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
