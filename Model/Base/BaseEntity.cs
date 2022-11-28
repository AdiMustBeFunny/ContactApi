using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Base
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime X_CreateTime { get; set; }
        public DateTime? X_Remove_Time { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
