using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DBConnector
{
    public static class TableManager
    {
        static Dictionary<Type, List<ColumnInfo>> _mapTableinfo = new Dictionary<Type, List<ColumnInfo>>();

        static Dictionary<Type, string> _mapTableinfo_TableName = new Dictionary<Type, string>();

        public static string GetTableName<T>(this T pSomethingTable)
            where T : ITable
        {
            Type pType = pSomethingTable.GetType();
            if (_mapTableinfo_TableName.TryGetValue(pType, out string strTableName))
                return strTableName;

            strTableName = pType.Name;
            _mapTableinfo_TableName.Add(pType, strTableName);

            return strTableName;
        }

        public static IReadOnlyList<ColumnInfo> GetColumnList<T>(this T pSomethingTable)
            where T : ITable
        {
            Type pType = pSomethingTable.GetType();
            if (_mapTableinfo.TryGetValue(pType, out List<ColumnInfo> listColumn))
                return listColumn;

            listColumn = new List<ColumnInfo>();
            _mapTableinfo.Add(pType, listColumn);

            IEnumerable<FieldInfo> arrMemberInfo = pType.GetFields().Where(x => x.GetCustomAttribute(typeof(ColumnAttribute)) != null);
            foreach (FieldInfo pField in arrMemberInfo)
            {
                ColumnAttribute pAttribute = (ColumnAttribute)pField.GetCustomAttribute(typeof(ColumnAttribute));
                if (pAttribute == null)
                    continue;

                string strColumnName = string.IsNullOrEmpty(pAttribute.strColumnName) ? pField.Name : pAttribute.strColumnName;
                listColumn.Add(new ColumnInfo(strColumnName, pAttribute.iOrder, pField));
            }

            listColumn.Sort((x, y)=> x.iOrder.CompareTo(y.iOrder));

            return listColumn;
        }
    }


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string strColumnName { get; private set; } = "";
        public int iOrder { get; private set; }
        public Func<object, string> GetValueString { get; private set; }

        public ColumnAttribute()
        {
        }

        public ColumnAttribute(int iOrder)
        {
            this.iOrder = iOrder;
        }

        public ColumnAttribute(Func<object, string> GetValueString)
        {
            this.GetValueString = GetValueString;
        }

        public ColumnAttribute(int iOrder, Func<object, string> GetValueString)
        {
            this.iOrder = iOrder;
            this.GetValueString = GetValueString;
        }

        public ColumnAttribute(string strColumnName, int iOrder)
        {
            this.strColumnName = strColumnName;
            this.iOrder = iOrder;
        }

    }

    public class ColumnInfo
    {
        public string strColumnName;
        public int iOrder;

        private FieldInfo _pField;

        public ColumnInfo(string strColumnName, int iOrder, FieldInfo pField)
        {
            this.strColumnName = strColumnName;
            this.iOrder = iOrder;
            this._pField = pField;
        }

        public void SetFieldValue(object pObjectOwner, object pObjectValue)
        {
            _pField.SetValue(pObjectOwner, pObjectValue);
        }

        public object GetFieldValue(object pObjectOwner)
        {
            return _pField.GetValue(pObjectOwner);
        }
    }
}