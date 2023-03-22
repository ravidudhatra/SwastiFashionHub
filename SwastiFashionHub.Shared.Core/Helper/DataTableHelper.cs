using SwastiFashionHub.Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Helper
{
    public static class DataTableHelper
    {
        public static List<ColumnDefinition> BindDataTableColumn<T>()
        {
            List<ColumnDefinition> Columns = new List<ColumnDefinition>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                string displayName = property.Name;

                var atts = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (atts.Length > 0)
                    displayName = (atts[0] as DisplayNameAttribute).DisplayName;

                Columns.Add(new ColumnDefinition { Title = displayName, Field = property.Name });
            }

            return Columns;
        }
    }
}
