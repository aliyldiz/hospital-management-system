using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class RoomController : Controller
{
    private IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    public IActionResult Index(int pageNumber=1, int pageSize=10)
    {
        return View(_roomService.GetAll(pageNumber, pageSize));
    }

    public IActionResult Edit(int id)
    {
        var viewModel = _roomService.GetRoomById(id);
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(RoomViewModel vm)
    {
        _roomService.UpdateRoom(vm);
        return RedirectToAction("Index");
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(RoomViewModel vm)
    {
        _roomService.InsertRoom(vm);
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        _roomService.DeleteRoom(id);
        return RedirectToAction("Index");
    }
}