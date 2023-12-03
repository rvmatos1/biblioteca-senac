using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Cliente
{
     public class Edit : PageModel
    {
        [BindProperty]
        public ClienteModel ClienteModel { get; set; } = new();
        public Edit(){
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Cliente/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            ClienteModel = JsonConvert.DeserializeObject<ClienteModel>(content)!;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Cliente/Edit/{id}";
            var clienteJson = JsonConvert.SerializeObject(ClienteModel);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return Page();
            }

            return RedirectToPage("/Cliente/Index");
        }
    }
}