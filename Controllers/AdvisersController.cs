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

namespace Contract_Administrator.Controllers
{
    public class AdvisersController : Controller
    {
        private readonly IAdviserRepository _adviserRepository;

        public AdvisersController(IAdviserRepository adviserRepository)
        {
            _adviserRepository = adviserRepository;
        }

        /// <summary>
        /// Spuští přehled poradců.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _adviserRepository.GetAll());
        }

        /// <summary>
        /// Zobrazuje View s detaily poradců.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            var adviser = await _adviserRepository.GetByIdAsync(id);
            if (adviser == null)
            {
                return NotFound();
            }

            return View(adviser);
        }

        /// <summary>
        /// Zobrazuje View pro vytvoření poradce.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// Ukládá data nově vytvořeného poradce do databáze.
        /// </summary>
        /// <param name="adviser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,TelNumber,PersonalIdNum,Age")] Adviser adviser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _adviserRepository.Add(adviser);
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Zobrazuje view pro editaci poradce.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var adviser = await _adviserRepository.GetByIdAsync(id);
            if (adviser == null) return NotFound();
            return View(adviser);
        }

        /// <summary>
        /// Ukládá upraveného klienta do databáze.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adviser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,TelNumber,PersonalIdNum,Age")] Adviser adviser)
        {
            if (id != adviser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _adviserRepository.Update(adviser);
                }
                catch (DbUpdateConcurrencyException)
                {
                        return NotFound();
                }
                return RedirectToAction("Index");
            }
            return View(adviser);
        }

        /// <summary>
        /// Zobrazuje potvrzovací view pro smazání poradce.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var adviser = await _adviserRepository.GetByIdAsync(id);
            if (adviser == null)
            {
                return NotFound();
            }

            return View(adviser);
        }

        /// <summary>
        /// Maže záznam o poradci podle id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adviser = await _adviserRepository.GetByIdAsync(id);
            if (adviser != null)
            {
                _adviserRepository.Delete(adviser);
            }
            return RedirectToAction("Index");
        }
    }
}
