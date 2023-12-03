using System.Text;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Biblioteca.Pages.Models;
using System.Globalization;

namespace Biblioteca.Pages.Pages.Emprestimo
{
    public class Create : PageModel
    {
        [BindProperty]
        public EmprestimoModel EmprestimoModel { get; set; } = new();
        public string? DataEmprestimo { get; set; }
        public string? DataPrevisaoDevolucao { get; set; }
        public List<BibliotecarioModel> BibliotecarioList { get; set; } = new();
        public List<LivroModel> LivroList { get; set; } = new();
        public Create(){}

        public void setDate(){
            DateTime date = DateTime.Today;
            string Text = date.ToString("yyyy-MM-dd");
            DataEmprestimo = Text;
            DateTime date2 = DateTime.Now;
            date2 = date2.AddDays(7);
            string Text2 = date2.ToString("yyyy-MM-dd");
            DataPrevisaoDevolucao = Text2;
        }
        public async Task<IActionResult> OnGetAsync(){
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
            setDate();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id){
                if(!ModelState.IsValid){
                    return Page();
                }
                
                var httpClient = new HttpClient();
                var url = "http://localhost:5185/Emprestimo/Create";
                var emprestimoJson = JsonConvert.SerializeObject(EmprestimoModel);
                var content = new StringContent(emprestimoJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                
                if(response.IsSuccessStatusCode){
                    return RedirectToPage("/Emprestimo/Index");
                } else {
                    return Page();
                }
        }
    }
}