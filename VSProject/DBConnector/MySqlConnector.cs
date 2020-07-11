using System;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DBConnector
{
    public class MySqlConnector : IDBConnector, IDisposable
    {
        private MySqlConnection _pConnection;

        public MySqlConnector()
        {
            // TODO 나중에 바꿔야함;
            string strServer = "localhost";
            int iPort = 3306;
            string strUID = "root";
            string strPWD = "root";
            string strDatabase = "connectortest";


            string strConn = $"server={strServer};port={iPort};uid={strUID};pwd={strPWD};database={strDatabase};charset=utf8 ;";


            _pConnection = new MySqlConnection(strConn);
        }

        public IDbCommand PopCommand(string strQuery)
        {
            return CreateCommand(strQuery);
        }

        public string DoExecuteQuery(string strQuery)
        {
            MySqlCommand command = CreateCommand(strQuery);
            StringBuilder strBuilder = new StringBuilder();

            using (MySqlDataReader rdr = command.ExecuteReader())
            {
                if (rdr == null)
                    return "";

                while (rdr.Read())
                    strBuilder.Append(rdr[0]);
                rdr.Close();
            }

            return strBuilder.ToString();
        }

        // 참고 링크
        // http://www.csharpstudy.com/Practical/Prac-mysql.aspx
        public void Test(string strQuery, DataTable pTable)
        {
            MySqlCommand command = CreateCommand(strQuery);
            
            //MySqlDataAdapter 클래스를 이용하여
            //비연결 모드로 데이타 가져오기
            using (MySqlDataAdapter adpt = new MySqlDataAdapter())
            {
                adpt.SelectCommand = command;
                adpt.Fill(pTable);
            }
        }


        public string ConvertQueryString(ITable pTable)
        {
            var listColumn = pTable.GetColumnList();
            string strKeys = "";
            string strValues = "";

            if (listColumn.Count == 1)
            {
                ColumnInfo pInfo = listColumn.First();
                strKeys += $"'{pInfo.strColumnName}'";
                strValues += $"'{pInfo.GetFieldValue(pTable)}'";
            }
            else
            {
                for (int i = 0; i < listColumn.Count; i++)
                {
                    ColumnInfo pInfo = listColumn[i];
                    if (i == listColumn.Count - 1)
                    {
                        strKeys += $"{pInfo.strColumnName}";
                        strValues += $"'{pInfo.GetFieldValue(pTable)}'";
                    }
                    else
                    {
                        strKeys += $"{pInfo.strColumnName},";
                        strValues += $"'{pInfo.GetFieldValue(pTable)}',";
                    }
                }
            }

            return $"{pTable.GetTableName()}({strKeys}) VALUES({strValues});";
        }



        public void DoOpen()
        {
            _pConnection.Open();
        }

        public void DoClose()
        {
            _pConnection.Close();
        }

        public void Dispose()
        {
            _pConnection?.Dispose();
        }


        private MySqlCommand CreateCommand(string strQuery)
        {
            MySqlCommand command = new MySqlCommand(strQuery, _pConnection);
            return command;
        }
    }
}
