using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Query.Category.GetAll
{
    public class CategoryListItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? ParentCategoryId { get; set; }
    }
}
