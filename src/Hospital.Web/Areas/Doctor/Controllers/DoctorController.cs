using System.Runtime.InteropServices;
using System.Security.Claims;
using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Areas.Doctor.Controllers;

[Area("Doctor")]
public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10)
    {
        return View(_doctorService.GetAll(pageNumber, pageSize));
    }
    
    public IActionResult AddTiming()
    {
        Timing timing = new Timing();
        List<SelectListItem> morningShiftStart = new List<SelectListItem>();
        List<SelectListItem> morningShiftEnd = new List<SelectListItem>();
        List<SelectListItem> afternoonShiftStart = new List<SelectListItem>();
        List<SelectListItem> afternoonShiftEnd = new List<SelectListItem>();

        for (int i = 1; i <= 11; i++)
        {
            morningShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString()});
        }
        for (int i = 1; i <= 13; i++)
        {
            morningShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString()});
        }
        for (int i = 13; i <= 16; i++)
        {
            afternoonShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString()});
        }
        for (int i = 13; i <= 18; i++)
        {
            afternoonShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString()});
        }
        
        ViewBag.morningStart = new SelectList(morningShiftStart, "Value", "Text");
        ViewBag.morningEnd = new SelectList(morningShiftEnd, "Value", "Text");
        ViewBag.evenStart = new SelectList(afternoonShiftStart, "Value", "Text");
        ViewBag.evenEnd = new SelectList(afternoonShiftEnd, "Value", "Text");

        TimingViewModel vm = new TimingViewModel();
        vm.ScheduleDate = DateTime.Now;
        vm.ScheduleDate = vm.ScheduleDate.AddDays(1);
        
        return View(vm);
    }
    
    [HttpPost]
    public IActionResult AddTiming(TimingViewModel vm)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (claims != null)
        {
            Console.WriteLine(claims.Value);
            vm.Doctor = new ApplicationUser { Id = claims.Value };
            Console.WriteLine(vm.Doctor.Id);
            vm.ScheduleDate = DateTime.Now;
            
            Console.WriteLine(vm.Doctor.Id);
            _doctorService.AddTiming(vm);
        }
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        _doctorService.DeleteTiming(id);
        return RedirectToAction("Index");
    }
}