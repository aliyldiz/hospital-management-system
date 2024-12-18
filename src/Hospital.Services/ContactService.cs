using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;

namespace Hospital.Services;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
    {
        var vm = new ContactViewModel();
        int totalCount;
        List<ContactViewModel> vmList = new List<ContactViewModel>();
        try
        {
            int ExludeRecords = (pageSize * pageNumber) - pageSize;
            var modelList = _unitOfWork.GenericRepository<Contact>().GetAll(includeProperties:"HospitalInfo").Skip(ExludeRecords).Take(pageSize).ToList();
            totalCount = _unitOfWork.GenericRepository<Contact>().GetAll().ToList().Count();
            vmList = ConvertModelToViewModelList(modelList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var result = new PagedResult<ContactViewModel>
        {
            Data = vmList,
            TotalItems = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return result;
    }

    public ContactViewModel GetContactById(int contactId)
    {
        var model = _unitOfWork.GenericRepository<Contact>().GetById(contactId);
        var vm = new ContactViewModel(model);
        return vm;
    }

    public void UpdateContact(ContactViewModel contact)
    {
        var model = new ContactViewModel().ConvertViewModel(contact);
        var modelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);
        modelById.Phone = contact.Phone;
        modelById.Email = contact.Email;
        _unitOfWork.GenericRepository<Contact>().Update(modelById);
        _unitOfWork.Save();
    }

    public void InsertContact(ContactViewModel contact)
    {
        var model = new ContactViewModel().ConvertViewModel(contact); 
        _unitOfWork.GenericRepository<Contact>().Add(model);
        _unitOfWork.Save();
    }

    public void DeleteContact(int id)
    {
        var model = _unitOfWork.GenericRepository<Contact>().GetById(id); 
        _unitOfWork.GenericRepository<Contact>().Delete(model);
        _unitOfWork.Save();
    }
    
    private List<ContactViewModel> ConvertModelToViewModelList(List<Contact> modelList)
    {
        return modelList.Select(x => new ContactViewModel(x)).ToList();
    }
}