using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Bibliotecario
{
    public class Index : PageModel
    {
        public List<BibliotecarioModel> BibliotecarioList { get; set; } = new();
        public Index(){
        }

        public async Task<IActionResult> OnGetAsync(){
            var httpClient = new HttpClient();
            var url = "http://localhost:5185/Bibliotecario";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            BibliotecarioList = JsonConvert.DeserializeObject<List<BibliotecarioModel>>(content)!;
            
            return Page();
        }
    }
}