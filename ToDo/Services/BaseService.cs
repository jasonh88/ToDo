using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ToDo.Services
{
    public class BaseService

    {
        public T MapToObject<T>(IDataReader reader)
        {
            var colname = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(c => c["ColumnName"].ToString().ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance<T>();
            foreach (var prop in properties)
            {
                if (colname.Contains(prop.Name.ToLower()))
                {
                    Type typ = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (reader[prop.Name] != DBNull.Value)
                    {
                        int i = reader.GetOrdinal(prop.Name);
                        if (reader[prop.Name].GetType() == typeof(decimal)) { prop.SetValue(obj, (reader.GetDouble(i)), null); }
                        else { prop.SetValue(obj, (reader.GetValue(reader.GetOrdinal(prop.Name)) ?? null), null); }
                    }
                }
            }
            return obj;
        }
    }
}
