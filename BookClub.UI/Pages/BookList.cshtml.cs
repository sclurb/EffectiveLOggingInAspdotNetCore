using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookClub.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookClub.UI.Pages
{
    public class BookListModel : PageModel
    {
        private readonly ILogger _logger;
        public List<Book> Books;

        public BookListModel(ILogger<BookListModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var userId = User.Claims.FirstOrDefault(async => async.Type == "sub")?.Value;
            _logger.LogInformation(message:"{UserName} - ({UserId}) Is About to call API to get book list. {Claims}",
                User.Identity.Name, userId, User.Claims);
            using (var http = new HttpClient(new StandardHttpMessageHandler(HttpContext)))
            {
                Books = await http.GetFromJsonAsync<List<Book>>("https://localhost:44322/api/Book");
            }
        }
    }
}