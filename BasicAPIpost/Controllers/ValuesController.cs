using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicAPIpost.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<TodoItem> lst = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Buy groceries", IsDone = true },
            new TodoItem { Id = 2, Title = "Pay bills", IsDone = false },
            new TodoItem { Id = 3, Title = "Walk the dog", IsDone = true },
            new TodoItem { Id = 4, Title = "Go to the gym", IsDone = false },
            new TodoItem { Id = 5, Title = "Finish the project", IsDone = false },
            new TodoItem { Id = 6, Title = "Prepare dinner", IsDone = false },
        };
        private static int idAI = 7;

        [HttpPost]
        [ActionName("GetAll")]
        public List<TodoItem> GetAll()
        {
            return lst;
        }

        [HttpPost]
        [ActionName("GetOneByID")]
        public TodoItem GetOneByID([FromBody] int id)
        {
            TodoItem item = lst.Find(x => x.Id == id);
            return item;
        }

        [HttpPost]
        [ActionName("AddOne")]
        public void AddOne([FromBody] TodoItem item)
        {
            item.Id = idAI;
            idAI++;
            lst.Add(item);
        }

        [HttpPost]
        [ActionName("UpdateOneToIsDone")]
        public void UpdateOneToIsDone([FromBody] TodoItem item)
        {
            lst.Single(x => x.Id == item.Id).IsDone = item.IsDone;
        }

        [HttpPost]
        [ActionName("DeleteOne")]
        public void DeleteOne([FromBody] int id)
        {
            lst.RemoveAll(x => x.Id == id);
        }

        [HttpPost]
        [ActionName("AddTodoWith2Parameters")]
        public string AddTodoWith2Parameters([FromBody] TodoItem item, [FromQuery] string title)
        {
            item.Id = idAI;
            item.Title = title;
            idAI++;
            lst.Add(item);
            return $"Added item with ID {item.Id} and custom title: {title}";
        }

        [HttpPost("GetItem/{id}")]
        [ActionName("AddTodoWith3Parameters")]
        public string AddTodoWith3Parameters([FromRoute] int id,[FromQuery] string title, [FromQuery] bool IsDone)
        {
            TodoItem item = new TodoItem();
            item.Id = id;
            item.Title = title;
            item.IsDone = IsDone;
            lst.Add(item);
            return $"Added item with ID {item.Id} and custom title: {title}";
        }

    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
