using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Providers
{
    public class CategoryIdProvider
    {
        //three basic categories
        public static string BusinessCategoryId => "14C96B4F-023E-4CB5-9F98-F44BC65D9409";
        public static string PrivateCategoryId => "1BC4C0CB-862D-4AB2-9A60-998803B4F1EB";
        public static string CustomCategoryId => "99B0D56D-5652-4E17-9F45-D5EDB900D5FC";

        //two subcategories of BusinessCategory
        public static string BossCategoryId => "C6C8A423-B69B-4DF0-A826-6AAB9B15AE91";
        public static string ClientCategoryId => "E4BE4F01-E12C-43B8-B7F5-9CDF263579A3";
    }
}
