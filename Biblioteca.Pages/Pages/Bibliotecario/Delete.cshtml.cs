using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Bibliotecario
{
    public class Delete : PageModel {   
        [BindProperty]
        public BibliotecarioModel BibliotecarioModel { get; set; } = new();
        public Delete(){
        }
        
        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Bibliotecario/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            BibliotecarioModel = JsonConvert.DeserializeObject<BibliotecarioModel>(content)!;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id){
            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Bibliotecario/Delete/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Bibliotecario/Index");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}