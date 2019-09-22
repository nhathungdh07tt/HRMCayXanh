using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Document;

namespace HRM.Services
{
    public interface IDocumentTypeService : IService<DocumentType>
    {
        List<DocumentTypeViewModel> GetDocumentTypes();
        DocumentTypeViewModel GetInfo(long id);
        void Insert(DocumentTypeViewModel model);
        void Update(DocumentTypeViewModel model);
        void Delete(DocumentTypeViewModel model);
       
    }

    public class DocumentTypeService : BaseService<DocumentType>, IDocumentTypeService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDocumentTypeRepository _repository;

        #endregion Properties

        #region Constructors

        public DocumentTypeService(IContext context, IDocumentTypeRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DocumentTypeViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<DocumentTypeViewModel> GetDocumentTypes()
        {
            return _repository.GetAll().ProjectTo<DocumentTypeViewModel>().ToList();
        }

       

        public DocumentTypeViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<DocumentType, DocumentTypeViewModel>(contractType);

            return new DocumentTypeViewModel();
        }

        public void Insert(DocumentTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DocumentTypeViewModel, DocumentType>(model);
            _repository.Add(contractType);
        }

        public void Update(DocumentTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DocumentTypeViewModel, DocumentType>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}