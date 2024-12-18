using Hospital.Utilities;
using Hospital.ViewModels;

namespace Hospital.Services;

public interface IDoctorService
{
    PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize);
    IEnumerable<TimingViewModel> GetAll();
    TimingViewModel GetTimingById(int timingId);
    void UpdateTiming(TimingViewModel timing);
    void AddTiming(TimingViewModel timing);
    void DeleteTiming(int timingId);
}