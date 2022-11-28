using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}
