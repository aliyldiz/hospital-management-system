using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ContactController : Controller
{
    private IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    
    public IActionResult Index(int pageNumber=1, int pageSize=10)
    {
        return View(_contactService.GetAll(pageNumber, pageSize));
    }

    public IActionResult Edit(int id)
    {
        // ViewBag.hopital = new SelectList(_hospitalInfo.GetAll(), "Id", "Name");
        var viewModel = _contactService.GetContactById(id);
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(ContactViewModel vm)
    {
        _contactService.UpdateContact(vm);
        return RedirectToAction("Index");
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(ContactViewModel vm)
    {
        _contactService.InsertContact(vm);
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        _contactService.DeleteContact(id);
        return RedirectToAction("Index");
    }
}