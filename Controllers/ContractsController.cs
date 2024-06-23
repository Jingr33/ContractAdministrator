using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contract_Administrator.Data;
using Contract_Administrator.Models;
using NuGet.Protocol.Core.Types;
using System.Xml;
using System.Data;
using Microsoft.Extensions.Logging;
using Contract_Administrator.Interfaces;
using Contract_Administrator.ViewModels;
using System.Diagnostics.Contracts;
using Contract = Contract_Administrator.Models.Contract;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contract_Administrator.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAdviserRepository _adviserRepository;
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context, IContractRepository contractRepository, IClientRepository clientRepository, IAdviserRepository adviserRepository)
        {
            _contractRepository = contractRepository;
            _clientRepository = clientRepository;
            _adviserRepository = adviserRepository;
            _context = context;
        }

        /// <summary>
        /// Zobrazuje tabulku s přehledem smluv.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _contractRepository.GetAll());
        }

        /// <summary>
        /// Zobrazuje podrobnosti smlouvy podle id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            var contract = await _contractRepository.GetByIdWithAdvisersAndClientsAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        /// <summary>
        /// Zobrazuje view pro přidání nové smlouvy do databáze.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            // vytvoření viewmodelu
            var viewModel = new ContractViewModel();
            SelectList client = new SelectList(_context.Client.ToList(), "Id", "LastName");
            SelectList adviser = new SelectList(_context.Adviser.ToList(), "Id", "LastName");
            viewModel.Clients = client;
            viewModel.Advisers = adviser;

            return View(viewModel);
        }

        /// <summary>
        /// Ukládá data nově vytvořené slmouvy do databáze
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            ModelState.Remove("contract.Client");
            if (!ModelState.IsValid) // ověření platnosti
            {
                var viewModel = new ContractViewModel();
                SelectList client = new SelectList(_context.Client.ToList(), "Id", "LastName");
                SelectList adviser = new SelectList(_context.Adviser.ToList(), "Id", "LastName");
                viewModel.Clients = client;
                viewModel.Advisers = adviser;
                return View(viewModel);
            }
            // vytvoření smlouvy
            _contractRepository.Add(contract);

            // propojení smlouvy a poradce
            var contractAdviser2 = new ContractAdviser
            {
                ContractId = contract.Id,
                AdviserId = contract.ContractAdmin,
                IsAdmin = true,
            };
            _context.ContractAdvisers.Add(contractAdviser2);

            foreach (var adviserId in contract.AdviserId)
            {
                var existingRelation = await _context.ContractAdvisers.FirstOrDefaultAsync(ca => ca.ContractId == contract.Id && ca.AdviserId == adviserId);
                if (existingRelation == null && (adviserId != contract.ContractAdmin))
                {
                    var contractAdviser = new ContractAdviser
                    {
                        ContractId = contract.Id,
                        AdviserId = adviserId,
                        IsAdmin = false,
                    };
                    _context.ContractAdvisers.Add(contractAdviser);
                }
            }
            await _context.SaveChangesAsync();

            // propojení klienta a smlouvy
            contract.Client = await _clientRepository.GetByIdAsync(contract.ClientId);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Zobrazuje pohled pro editaci zvolené smlouvy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            SelectList client = new SelectList(_context.Client.ToList(), "Id", "LastName", contract.ClientId);
            SelectList contractAdmin = new SelectList(_context.Adviser.ToList(), "Id", "LastName", contract.ContractAdmin);
            SelectList adviser = new SelectList(_context.Adviser.ToList(), "Id", "LastName", contract.AdviserId);
            // viewmodel
            var viewModel = new ContractViewModel
            {
                Clients = client,
                ContractAdmin = contractAdmin,
                Advisers = adviser,
                Contract = contract
            };
            return View(viewModel);
        }

        /// <summary>
        /// ukládá upravené data o smlouvě do databáze. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ContractViewModel viewModel)
        {

            if (id != viewModel.Contract.Id)
            {
                return NotFound();
            }

            ModelState.Remove("contract.Client");
            ModelState.Remove("File");
            ModelState.Remove("Institution");
            if (!ModelState.IsValid)
            {
                var contract = await _contractRepository.GetByIdAsync(id);

                SelectList client = new SelectList(_context.Client.ToList(), "Id", "LastName", contract.ClientId);
                SelectList contractAdmin = new SelectList(_context.Adviser.ToList(), "Id", "LastName", contract.ContractAdmin);
                SelectList adviser = new SelectList(_context.Adviser.ToList(), "Id", "LastName", contract.AdviserId);
                viewModel.Clients = client;
                viewModel.ContractAdmin = contractAdmin;
                viewModel.Advisers = adviser;
                viewModel.Contract = contract;
                return View(viewModel);
            }
            _contractRepository.Update(viewModel.Contract);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Zobrazuje potvrzovací view před odstraněním smllouvy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        /// <summary>
        /// Odstraní záznam o smlouvě z databáze.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract != null)
            {
                _contractRepository.Delete(contract);
            }

            return RedirectToAction("Index");
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.Id == id);
        }
    }
}
