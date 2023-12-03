using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;

namespace Biblioteca.Pages.Pages.Emprestimo
{
     public class Edit : PageModel
    {
        [BindProperty]
        public EmprestimoModel EmprestimoModel { get; set; } = new();
        public List<BibliotecarioModel> BibliotecarioList { get; set; } = new();
        public List<LivroModel> LivroList { get; set; } = new();
        public Edit(){}

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Emprestimo/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            EmprestimoModel = JsonConvert.DeserializeObject<EmprestimoModel>(content)!;

            var httpClientBibliotecario = new HttpClient();
            var urlBibliotecario = "http://localhost:5185/Bibliotecario";
            var requestMessageBibliotecario = new HttpRequestMessage(HttpMethod.Get, urlBibliotecario);
            var responseBibliotecario = await httpClientBibliotecario.SendAsync(requestMessageBibliotecario);
            var contentBibliotecario = await responseBibliotecario.Content.ReadAsStringAsync();

            BibliotecarioList = JsonConvert.DeserializeObject<List<BibliotecarioModel>>(contentBibliotecario)!;
            
            var httpClientLivro = new HttpClient();
            var urlLivro = "http://localhost:5185/Livro";
            var requestMessageLivro = new HttpRequestMessage(HttpMethod.Get, urlLivro);
            var responseLivro = await httpClientLivro.SendAsync(requestMessageLivro);
            var contentLivro = await responseLivro.Content.ReadAsStringAsync();

            LivroList = JsonConvert.DeserializeObject<List<LivroModel>>(contentLivro)!;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5185/Emprestimo/Edit/{id}";
            var emprestimoJson = JsonConvert.SerializeObject(EmprestimoModel);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(emprestimoJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return Page();
            }

            return RedirectToPage("/Emprestimo/Index");
        }
    }
}