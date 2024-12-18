using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;

namespace Hospital.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
    {
        var vm = new TimingViewModel();
        int totalCount;
        List<TimingViewModel> vmList = new List<TimingViewModel>();
        try
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var modelList = _unitOfWork.GenericRepository<Timing>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();
            totalCount = _unitOfWork.GenericRepository<Timing>().GetAll().ToList().Count();
            vmList = ConvertModelToViewModelList(modelList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var result = new PagedResult<TimingViewModel>
        {
            Data = vmList,
            TotalItems = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return result;
    }

    public IEnumerable<TimingViewModel> GetAll()
    {
        var timingList = _unitOfWork.GenericRepository<Timing>().GetAll().ToList();
        var vmList = ConvertModelToViewModelList(timingList);
        return vmList;
    }

    public TimingViewModel GetTimingById(int timingId)
    {
        var model = _unitOfWork.GenericRepository<Timing>().GetById(timingId);
        var vm = new TimingViewModel(model);
        return vm;
    }

    public void UpdateTiming(TimingViewModel timing)
    {
        var model = new TimingViewModel().ConvertViewModel(timing);
        var modelById = _unitOfWork.GenericRepository<Timing>().GetById(model.Id);
        modelById.Id = timing.Id;
        modelById.Doctor = timing.Doctor;
        modelById.Status = timing.Status;
        modelById.Duration = timing.Duration;
        modelById.MorningShiftStartTime = timing.MorningShiftStartTime;
        modelById.MorningShiftEndTime = timing.MorningShiftEndTime;
        modelById.AfternoonShiftStartTime = timing.AfternoonShiftStartTime;
        modelById.AfternoonShiftEndTime = timing.AfternoonShiftEndTime;
        
        _unitOfWork.GenericRepository<Timing>().Update(modelById);
        _unitOfWork.Save();
    }

    public void AddTiming(TimingViewModel timing)
    {
        var model = new TimingViewModel().ConvertViewModel(timing);
        Console.WriteLine(model.AfternoonShiftEndTime.ToString());
        _unitOfWork.GenericRepository<Timing>().Add(model);
        _unitOfWork.Save();
        
    }

    public void DeleteTiming(int timingId)
    {
        var model = _unitOfWork.GenericRepository<Timing>().GetById(timingId);
        _unitOfWork.GenericRepository<Timing>().Delete(model);
        _unitOfWork.Save();
    }
    
    private List<TimingViewModel> ConvertModelToViewModelList(List<Timing> modelList)
    {
        return modelList.Select(x => new TimingViewModel(x)).ToList();
    }
}