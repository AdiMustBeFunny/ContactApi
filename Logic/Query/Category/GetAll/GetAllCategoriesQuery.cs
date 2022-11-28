using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Query.Category.GetAll
{
    public class GetAllCategoriesQuery : IRequest<Request<IEnumerable<CategoryListItemDto>>>
    {
    }
}
