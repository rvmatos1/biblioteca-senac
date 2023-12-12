using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Cliente
{
    public class Create : PageModel
    {
        [BindProperty]
        public ClienteModel ClienteModel { get; set; } = new();
        public Create(){}

        public async Task<IActionResult> OnPostAsync(int id){
                if(!ModelState.IsValid){
                    return Page();
                }
                
                var httpClient = new HttpClient();
                var url = "http://localhost:5185/Cliente/Create";
                var clienteJson = JsonConvert.SerializeObject(ClienteModel);
                var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                
                if(response.IsSuccessStatusCode){
                    return RedirectToPage("/Cliente/Index");
                } else {
                    return Page();
                }
        }
    }
}