using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Bibliotecario
{
    public class Create : PageModel
    {
        [BindProperty]
        public BibliotecarioModel BibliotecarioModel { get; set; } = new();
        public Create(){}

        public async Task<IActionResult> OnPostAsync(int id){
                if(!ModelState.IsValid){
                    return Page();
                }
                
                var httpClient = new HttpClient();
                var url = "http://localhost:5185/Bibliotecario/Create";
                var bibliotecarioJson = JsonConvert.SerializeObject(BibliotecarioModel);
                var content = new StringContent(bibliotecarioJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                
                if(response.IsSuccessStatusCode){
                    return RedirectToPage("/Bibliotecario/Index");
                } else {
                    return Page();
                }
        }
    }
}