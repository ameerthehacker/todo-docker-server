using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using todoserver;

namespace todo_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private TodoService _todoService;

        public TodosController() {
            this._todoService = new TodoService();
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<IList<Todo>> GetTodos()
        {
            return Ok(this._todoService.GetTodos());
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Todo> AddTodo([FromBody] Todo newTodo)
        {
            var todo = this._todoService.AddTodo(newTodo);

            return Ok(todo);
        }

        [HttpPut]
        [Route("[action]/{id:int}")]
        public ActionResult<Todo> ToggleTodo(int id)
        {
            var todo = this._todoService.ToggleTodo(id);

            return Ok(todo);
        }
    }
}
