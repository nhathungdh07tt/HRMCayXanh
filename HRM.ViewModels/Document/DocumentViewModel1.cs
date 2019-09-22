using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain.Entity;
using HRM.ViewModels.Department;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Document
{
    public class DocumentViewModel
    {
        public DocumentViewModel()
        {
           
        }

        public long Id { get; set; }
        /// <summary>
        /// Loai van ban
        /// </summary>
     
        public long TypeId { get; set; }
        public virtual DocumentTypeViewModel DocumentType { get; set; }

        /// <summary>
        /// Loai van ban
        /// </summary>
        public DocumentType Type { get; set; }
        /// <summary>
        /// Ma Cong Van
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// So Ky Hieu
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// Ngay Van Ban
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Ngay Nhan
        /// </summary>
        public DateTime? ReceivedDate { get; set; }
        /// <summary>
        /// Tu Co Quan
        /// </summary>
        public string FromDepartment { get; set; }
        /// <summary>
        /// Noi Giai Quyet
        /// </summary>
        public string ResolvePlace { get; set; }
        /// <summary>
        /// Noi Luu Tru
        /// </summary>
        public string StorePlace { get; set; }
        /// <summary>
        /// Mo Ta Ngan
        /// </summary>
        [DataType("text")]
        public string ShortDescription { get; set; }
               
        public long ReceiveDepartmentId { get; set; }
        /// <summary>
        /// Phong Ban Nhan
        /// </summary>
        public virtual DepartmentViewModel Department { get; set; }

       
        public long AssignToId { get; set; }
        /// <summary>
        /// Phan Cong Cho
        /// </summary>
       

       
        public long SignById { get; set; }
        /// <summary>
        ///  Ky Boi
        /// </summary>
      

      
        public long WriteById { get; set; }
        /// <summary>
        /// Soan Thao Boi
        /// </summary>
        public virtual EmployeeViewModel Employee { get; set; }
        /// <summary>
        /// Tap Tin
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Kiem Tra
        /// </summary>
        public int Checked { get; set; }

        public HttpPostedFileBase DocumentFile { get; set; }

    }
     

    }

