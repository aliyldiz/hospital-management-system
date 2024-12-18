using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels;

public class TimingViewModel
{
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public DateTime ScheduleDate { get; set; }
    public int MorningShiftStartTime { get; set; }
    public int MorningShiftEndTime { get; set; }
    public int AfternoonShiftStartTime { get; set; }
    public int AfternoonShiftEndTime { get; set; }
    public int Duration { get; set; }
    public Status Status { get; set; }
    private List<SelectListItem> morningShiftStart = new List<SelectListItem>();
    private List<SelectListItem> morningShiftEnd = new List<SelectListItem>();
    private List<SelectListItem> afternoonShiftStart = new List<SelectListItem>();
    private List<SelectListItem> afternoonShiftEnd = new List<SelectListItem>();
    public ApplicationUser Doctor { get; set; }

    public TimingViewModel()
    {
        
    }

    public TimingViewModel(Timing model)
    {
        Id = model.Id;
        DoctorId = model.DoctorId;
        ScheduleDate = model.Date;
        MorningShiftStartTime = model.MorningShiftStartTime;
        MorningShiftEndTime = model.MorningShiftEndTime;
        AfternoonShiftStartTime = model.AfternoonShiftStartTime;
        AfternoonShiftEndTime = model.AfternoonShiftEndTime;
        Duration = model.Duration;
        Status = model.Status;
        Doctor = model.Doctor;
    }
    
    public Timing ConvertViewModel(TimingViewModel model)
    {
        return new Timing
        {
            Id = model.Id,
            Date = model.ScheduleDate,
            MorningShiftStartTime = model.MorningShiftStartTime,
            MorningShiftEndTime = model.MorningShiftEndTime,
            AfternoonShiftStartTime = model.AfternoonShiftStartTime,
            AfternoonShiftEndTime = model.AfternoonShiftEndTime,
            Duration = model.Duration,
            Status = model.Status,
            DoctorId = model.Doctor.Id,
        };
    }
}