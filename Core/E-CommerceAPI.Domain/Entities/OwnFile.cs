using E_CommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class OwnFile:BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        // baseEntityden gelen bir seyi bu tabloda istemiyorsak ovveride edip notMapped diyoruz
        [NotMapped]
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
