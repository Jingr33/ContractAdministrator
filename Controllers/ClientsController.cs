using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contract_Administrator.Data;
using Contract_Administrator.Models;
using Contract_Administrator.Interfaces;
using Contract_Administrator.Repository;

namespace Contract_Administrator.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Zobrazuje view s přehledem klientů
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _clientRepository.GetAll());
        }

        /// <summary>
        /// Zobrazuje view s podrobnostmi zvoleného klienta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            var client = await _clientRepository.GetByIdWithContractsAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        /// <summary>
        /// Zobrazuje view pro vytvoření klienta.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// Ukládá záznam o vytvořeného klienta do databáze.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            _clientRepository.Add(client);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Zobrazuje view pro editaci klienta.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();
            return View(client);
        }

        /// <summary>
        /// Ukládá upravená data o klientovi do databáze.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clientRepository.Update(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            return View(client);
        }

        /// <summary>
        /// Zobrazí potvrzovací view před odstraněním klienta.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {

            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        /// <summary>
        /// Odstraní klienta podle id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client != null)
            {
                _clientRepository.Delete(client);
            }
            return RedirectToAction("Index");
        }
    }
}
