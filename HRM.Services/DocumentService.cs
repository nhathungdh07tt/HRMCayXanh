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
    public interface IDocumentService : IService<Document>
    {
        List<DocumentViewModel> GetDocuments();
        List<DocumentViewModel> GetDocuments1();
        DocumentViewModel GetInfo(long id);
        void Insert(DocumentViewModel model);
        void Update(DocumentViewModel model);
        void Delete(DocumentViewModel model);
        List<DocumentViewModel> GetDocumentTypeByDocument(long? TypeId);
    }

    public class DocumentService : BaseService<Document>, IDocumentService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDocumentRepository _repository;

        #endregion Properties

        #region Constructors

        public DocumentService(IContext context, IDocumentRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DocumentViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<DocumentViewModel> GetDocumentTypeByDocument(long? TypeId)
        {            
            return _repository.GetAllByCondition(x => x.DocumentType.Id == TypeId, x => x.DocumentType)
                .ProjectTo<DocumentViewModel>().ToList();
        }

        public List<DocumentViewModel> GetDocuments()
        {           
            return _repository.GetAllByCondition(x => x.Checked == 0, x => x.Type, x => x.ReceiveDepartment, x => x.SignBy)
               .ProjectTo<DocumentViewModel>().ToList();
          
        }

        public List<DocumentViewModel> GetDocuments1()
        {
            
            return _repository.GetAllByCondition(x => x.Checked == 1, x => x.Type, x => x.ReceiveDepartment, x => x.SignBy,x=>x.WriteBy)
               .ProjectTo<DocumentViewModel>().ToList();

        }

        public DocumentViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Document, DocumentViewModel>(contractType);

            return new DocumentViewModel();
        }

        public void Insert(DocumentViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DocumentViewModel, Document>(model);
            _repository.Add(contractType);
        }

        public void Update(DocumentViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DocumentViewModel, Document>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}