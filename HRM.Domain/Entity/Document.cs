using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Document : AuditableEntity<long>
    {
        public Document()
        {
            

        }

        [ForeignKey(nameof(Type))]
        public long? TypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
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

        [ForeignKey(nameof(ReceiveDepartment))]
        public long ReceiveDepartmentId { get; set; }
        /// <summary>
        /// Phong Ban Nhan
        /// </summary>
        public Department ReceiveDepartment { get; set; }

        [ForeignKey(nameof(AssignTo))]
        public long AssignToId { get; set; }
        /// <summary>
        /// Phan Cong Cho
        /// </summary>
        public Employee AssignTo { get; set; }

        [ForeignKey(nameof(SignBy))]
        public long SignById { get; set; }
        /// <summary>
        ///  Ky Boi
        /// </summary>
        public Employee SignBy { get; set; }

        [ForeignKey(nameof(WriteBy))]
        public long WriteById { get; set; }
        /// <summary>
        /// Soan Thao Boi
        /// </summary>
        public Employee WriteBy { get; set; }
        /// <summary>
        /// Tap Tin
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Kiem Tra
        /// </summary>
        public int Checked { get; set; }

    }
}
