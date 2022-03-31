using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;

        public WalksController(IWalkRepository walkRepository, IDogRepository dogRepository, IWalkerRepository walkerRepository)
        {
            _walkRepo = walkRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
        }

        // GET: WalksController
        public ActionResult Index()
        {
            List<Walk> walks = _walkRepo.GetAllWalks();

            return View(walks);
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            WalkFormViewModel vm = new WalkFormViewModel()
            {
                Walk = new Walk(),
                Dogs = _dogRepo.GetAllDogs(),
                Walkers = _walkerRepo.GetAllWalkers(),
                SelectedDogIds = new List<int>()
            };

            return View(vm);
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalkFormViewModel vm)
        {
            try
            {
                foreach(int dogId in vm.SelectedDogIds)
                {
                    vm.Walk.DogId = dogId;
                    _walkRepo.AddWalk(vm.Walk);
                }

                return RedirectToAction("index");
            }
            catch(Exception ex)
            {
                return View(vm);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
