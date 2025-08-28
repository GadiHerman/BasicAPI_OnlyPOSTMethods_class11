using System.Net.Http.Json;

namespace ConsoleUnitTest
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Executing API POST calls...");

            // 1. Add some items
            await AddOne(new TodoItem { Title = "Buy groceries", IsDone = false });
            await AddOne(new TodoItem { Title = "Learn C#", IsDone = false });

            Console.WriteLine("\n--- After adding items ---");

            // 2. Get all items
            await GetAll();
            Console.WriteLine("\n-------------------");

            // 3. Get one by ID
            await GetOneById(1);
            Console.WriteLine("\n-------------------");

            // 4. Update one item
            await UpdateOneToIsDone(new TodoItem { Id = 1, IsDone = true });
            Console.WriteLine("\n--- After updating item 1 ---");
            await GetAll();
            Console.WriteLine("\n-------------------");

            // 5. Delete one item
            await DeleteOne(2);
            Console.WriteLine("\n--- After deleting item 2 ---");
            await GetAll();
            Console.WriteLine("\n-------------------");

            // 6. Call the new function with two parameters
            await AddTodoWithTitle(new TodoItem { Title = "Build a rocket", IsDone = false }, "Project Mars");
            Console.WriteLine("\n--- After adding with two parameters ---");
            await GetAll();
            Console.WriteLine("\n-------------------");

            // 6. Call the new function with 3 parameters
            await AddTodoWith3Parameters(13,"Build a rocket", false);
            Console.WriteLine("\n--- After adding with 3 parameters ---");
            await GetAll();

            Console.ReadKey();
        }


        public static async Task<List<TodoItem>> GetAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                Console.WriteLine("Calling 'GetAll'...");
                string URL = "https://localhost:7033/api/Values/GetAll";
                var response = await client.PostAsJsonAsync<object>(URL, null);
                response.EnsureSuccessStatusCode();
                List<TodoItem> items = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
                Console.WriteLine("Received all Todo items:");
                foreach (var item in items)
                {
                    Console.WriteLine($"- ID: {item.Id}, Title: {item.Title}, IsDone: {item.IsDone}");
                }
                return items;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
                return null;
            }
        }

        public static async Task<TodoItem> GetOneById(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7033/api/Values/GetOneByID";
                Console.WriteLine($"Calling 'GetOneByID' with ID {id}...");
                var response = await client.PostAsJsonAsync(URL, id);
                response.EnsureSuccessStatusCode();
                TodoItem item = await response.Content.ReadFromJsonAsync<TodoItem>();

                if (item != null)
                {
                    Console.WriteLine($"Received item: ID={item.Id}, Title={item.Title}, IsDone={item.IsDone}");
                }
                else
                {
                    Console.WriteLine("Item not found.");
                }
                return item;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
                return null;
            }
        }

        public static async Task AddOne(TodoItem item)
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7033/api/Values/AddOne";
                Console.WriteLine($"Calling 'AddOne' with title '{item.Title}'...");
                var response = await client.PostAsJsonAsync(URL, item);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Item added successfully.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }

        public static async Task UpdateOneToIsDone(TodoItem item)
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7033/api/Values/UpdateOneToIsDone";
                Console.WriteLine($"Calling 'UpdateOneToIsDone' for ID {item.Id}...");
                var response = await client.PostAsJsonAsync(URL, item);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Item updated successfully.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }

        public static async Task DeleteOne(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7033/api/Values/DeleteOne";
                Console.WriteLine($"Calling 'DeleteOne' for ID {id}...");
                var response = await client.PostAsJsonAsync(URL, id);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Item deleted successfully.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }

        public static async Task AddTodoWithTitle(TodoItem item, string title)
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7033/api/Values/AddTodoWithTitle";
                Console.WriteLine($"Calling 'AddTodoWithTitle' with title '{item.Title}' and custom title '{title}'...");
                var response = await client.PostAsJsonAsync(URL + $"?title={title}", item);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Result from API: {result}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }

        public static async Task AddTodoWith3Parameters(int id, string title, bool IsDone)
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7033/api/Values/AddTodoWith3Parameters/GetItem";
                Console.WriteLine($"Calling 'AddTodoWith3Parameters' with id '{id}' title '{title}' and IsDone '{IsDone}'...");
                var response = await client.PostAsJsonAsync<object>(URL + $"/{id}?title={title}&IsDone={IsDone}", null);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Result from API: {result}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }

    }
}

