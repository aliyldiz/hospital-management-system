using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;

namespace Hospital.Services;

public class RoomService : IRoomService
{
    private IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
    {
        var vm = new RoomViewModel();
        int totalCount;
        List<RoomViewModel> vmList = new List<RoomViewModel>();
        try
        {
            int ExludeRecords = (pageSize * pageNumber) - pageSize;
            var modelList = _unitOfWork.GenericRepository<Room>().GetAll(includeProperties:"Hospital").Skip(ExludeRecords).Take(pageSize).ToList();
            totalCount = _unitOfWork.GenericRepository<Room>().GetAll().ToList().Count();
            vmList = ConvertModelToViewModelList(modelList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var result = new PagedResult<RoomViewModel>
        {
            Data = vmList,
            TotalItems = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return result;
    }

    public RoomViewModel GetRoomById(int roomId)
    {
        var model =  _unitOfWork.GenericRepository<Room>().GetById(roomId);
        var vm = new RoomViewModel(model);
        return vm;
    }

    public void UpdateRoom(RoomViewModel room)
    {
        var model = new RoomViewModel().ConvertViewModel(room);
        var modelById = _unitOfWork.GenericRepository<Room>().GetById(model.Id);
        modelById.Type = room.Type;
        modelById.RoomNumber = room.RoomNumber;
        modelById.Status = room.Status;
        modelById.HospitalInfoId = room.HospitalInfoId;
        _unitOfWork.GenericRepository<Room>().Update(modelById);
        _unitOfWork.Save();
    }

    public void InsertRoom(RoomViewModel room)
    {
        var model = new RoomViewModel().ConvertViewModel(room);
        _unitOfWork.GenericRepository<Room>().Add(model);
        _unitOfWork.Save();
    }

    public void DeleteRoom(int id)
    {
        var model =  _unitOfWork.GenericRepository<Room>().GetById(id);
        _unitOfWork.GenericRepository<Room>().Delete(model);
        _unitOfWork.Save();
    }
    private List<RoomViewModel> ConvertModelToViewModelList(List<Room> modelList)
    {
        return modelList.Select(x => new RoomViewModel(x)).ToList();
    }
}