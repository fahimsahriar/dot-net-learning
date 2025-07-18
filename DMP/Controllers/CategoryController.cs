﻿using DMP.DataAccess.Repository;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMasteryProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;
        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 0 || pageSize < 0)
            {
                return View(new List<Category>());
            }
            try
            {
                var categories = await _repo.GetAll(pageNumber, pageSize);
                if (categories == null || !categories.Any())
                {
                    return View(new List<Category>());
                }
                categories = categories.OrderBy( item => item.DisplayOrder).ToList();

                ViewBag.CurrentPage = pageNumber;
                ViewBag.PageSize = pageSize;
                var totalItems = await _repo.GetTotalItemCount();
                ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                return View(categories);
            }
            catch
            {
                return View(new List<Category>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //adding custom validation 
                ModelState.AddModelError("name", "Display order and name can not be same.");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _repo.Add(obj);
                await _repo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Category creation failed.";
                //throw Exception
                return View();
            }

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0 || id < 1 || id == null)
            {
                return NotFound();
            }
            Category? obj = await _repo.Get(c => c.Id == id); //works on primary key
            //Category? obj2ndWay = _db.Categories.FirstOrDefault(c => c.Name == "Action"); // works on any kind of column
            //Category? obj3rdWay = _db.Categories.Where(c => c.DisplayOrder == 2).FirstOrDefault(); // works on any kind of column
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order and name cannot be the same.");
            }

            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            try
            {
                var objFromDb = await _repo.Get(c => c.Id == obj.Id);
                if (objFromDb == null)
                {
                    return NotFound();
                }

                // Manually update properties
                objFromDb.Name = obj.Name;
                objFromDb.DisplayOrder = obj.DisplayOrder;

                await _repo.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            catch (Exception)
            {
                return View(obj);
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id < 1 || id == null)
            {
                return NotFound();
            }
            Category? obj2ndWay = await _repo.Get(c => c.Id == id);
            if (obj2ndWay == null)
            {
                return NotFound();
            }
            try
            {
                _repo.Remove(obj2ndWay);
                await _repo.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index", "Category");
            }
            catch
            {
                return RedirectToAction("Index", "Category");
            }
        }
    }
}
