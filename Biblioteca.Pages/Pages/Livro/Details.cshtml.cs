using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Livro
{
    public class Details : PageModel
    {
        public LivroModel LivroModel { get; set; } = new();
        public Details(){}

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Livro/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            LivroModel = JsonConvert.DeserializeObject<LivroModel>(content)!;
            
            return Page();
        }
    }
}