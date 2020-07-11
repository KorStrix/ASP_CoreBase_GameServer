using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBConnector
{
    // 참고 링크
    // https://stackoverflow.com/questions/8008389/how-to-convert-datatable-to-class-object
    public static class DBExtension
    {
        // function that creates a list of an object from the given data table
        public static List<T> CreateListFromTable<T>(this DataTable tbl) where T : ITable, new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each pRow
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }

        // function that creates an object from the given data pRow
        public static T CreateItemFromRow<T>(DataRow pRow) where T : ITable, new()
        {
            T pItem = new T();
            SetItemFromRow(pItem, pRow);

            return pItem;
        }

        public static void SetItemFromRow<T>(T pItem, DataRow pRow) where T : ITable, new()
        {
            var listColumn = pItem.GetColumnList();

            foreach (DataColumn pColumn in pRow.Table.Columns)
            {
                var pColumnField = listColumn.FirstOrDefault(p => p.strColumnName == pColumn.ColumnName);

                if (pColumnField != null && pRow[pColumn] != DBNull.Value)
                    pColumnField.SetFieldValue(pItem, pRow[pColumn]);
            }
        }
    }
}
