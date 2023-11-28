using AV2_Biblioteca_Final.Model.Models;
using AV2_Biblioteca_Final.Model.Repositories;
using AV2_BibliotecaDB_Final.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AV2_BibliotecaDB_Final.View.Controllers
{
    public class AutoresController : DefalutController
    {
        private readonly RepositoryAutores _repositoryAutores;

        public AutoresController(RepositoryAutores repositoryAutores)
        {
            _repositoryAutores = repositoryAutores;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var listaAutores = await _repositoryAutores.SelecionarTodosAsync();
            return View(listaAutores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Autores autor)
        {
            if (ModelState.IsValid)
            {
                await _repositoryAutores.IncluirAsync(autor);
                return RedirectToAction("Index", "Autores", new RepositoryAutores());
            }
            return View(autor);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _repositoryAutores.SelecionarPkAsync(id);
            if (autor == null)
            {
                return NotFound(); // Autor não encontrado
            }
            return View(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Autores autor)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _repositoryAutores.AlterarAsync(autor);
                if (resultado == null)
                {
                    return NotFound(); // Autor não encontrado
                }
                // Adicione o código abaixo para redirecionar para a ação Index do controlador Autores
                return RedirectToAction("Index", "Autores", new RepositoryAutores());
            }
            // Se o modelo não for válido, volte para a view de edição com os erros de validação
            return View(autor);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var detalhes = await _repositoryAutores.SelecionarPkAsync(id);
            if (detalhes == null)
            {
                return NotFound(); // Autor não encontrado
            }
            return View(detalhes);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var detalhes = await _repositoryAutores.SelecionarPkAsync(id);

            return View(detalhes);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Autores autores)
        {
            var oAutor = await _repositoryAutores.SelecionarPkAsync(autores.AutorId);
            await _repositoryAutores.ExcluirAsync(oAutor);
            return RedirectToAction("Index");
        }
    }
}
