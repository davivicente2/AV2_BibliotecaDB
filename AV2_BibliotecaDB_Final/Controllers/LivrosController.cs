using AV2_Biblioteca_Final.Model.Models;
using AV2_Biblioteca_Final.Model.Repositories;
using AV2_BibliotecaDB_Final.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AV2_BibliotecaDB_Final.View.Controllers
{ 
    public class LivrosController : DefalutController
    {
        private readonly RepositoryLivros _repositoryLivros;
        private readonly RepositoryAutores _repositoryAutores;

        public LivrosController(RepositoryLivros repositoryLivros, RepositoryAutores repositoryAutores)
        {
            _repositoryLivros = repositoryLivros;
            _repositoryAutores = repositoryAutores;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var listaLivros = await _repositoryLivros.SelecionarTodosAsync();
            return View(listaLivros);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Autores = new SelectList(await _repositoryAutores.SelecionarTodosAsync(), "AutorId", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Livros livro)
        {
            if (ModelState.IsValid)
            {
                await _repositoryLivros.IncluirAsync(livro);
                return RedirectToAction("Index", "Livros");
            }

            ViewBag.Autores = new SelectList(await _repositoryAutores.SelecionarTodosAsync(), "AutorId", "Nome");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var livro = await _repositoryLivros.SelecionarPkAsync(id);
            if (livro == null)
            {
                return NotFound(); 
            }
            ViewBag.Autores = new SelectList(await _repositoryAutores.SelecionarTodosAsync(), "AutorId", "Nome");

            return View(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Livros livro)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _repositoryLivros.AlterarAsync(livro);
                if (resultado == null)
                {
                    return NotFound(); 
                }
                return RedirectToAction("Index", "Livros", new RepositoryLivros());
            }
            return View(livro);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var detalhes = await _repositoryLivros.SelecionarPkAsync(id);
            if (detalhes == null)
            {
                return NotFound(); 
            }
            return View(detalhes);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var detalhes = await _repositoryLivros.SelecionarPkAsync(id);

            return View(detalhes);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Livros livros)
        {
            var oLivro = await _repositoryLivros.SelecionarPkAsync(livros.LivroId);
            await _repositoryLivros.ExcluirAsync(oLivro);
            return RedirectToAction("Index");
        }
    }
}
