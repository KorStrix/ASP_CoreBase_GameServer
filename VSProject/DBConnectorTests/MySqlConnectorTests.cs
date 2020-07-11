using System.Data;
using DBConnector;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace DBConnectorTests
{
    public class MySqlConnectorTests
    {
        class testtable : ITable
        {
            [Column]
            public int intvalue;

            [Column]
            public string stringvalue;
        }

        [Test]
        public void ConnectTest()
        {
            // Arrange
            MySqlConnector pConnector = new MySqlConnector();
            pConnector.DoOpen();

            // Act && Assert
            string strResult = pConnector.DoExecuteQuery("SELECT VERSION()");
            Assert.IsFalse(string.IsNullOrEmpty(strResult));

            pConnector.DoClose();
        }


        [Test]
        public void Update_Test()
        {
            // Arrange
            MySqlConnector pConnector = new MySqlConnector();
            pConnector.DoOpen();



            pConnector.DoClose();
        }


        [Test]
        public void Insert_And_Delete_Test()
        {
            // Arrange
            IDBConnector pConnector = new MySqlConnector();
            pConnector.DoOpen();

            testtable sTable = new testtable();
            sTable.intvalue = 1;
            sTable.stringvalue = "asdf";
            


            // Act
            // Insert
            pConnector.PopCommand($"INSERT INTO {pConnector.ConvertQueryString(sTable)}").ExecuteNonQuery();


            // Assert
            DataTable pTable = new DataTable();
            var pCommand_Select = pConnector.PopCommand($"SELECT * FROM {sTable.GetTableName()} WHERE {nameof(testtable.stringvalue)} = '{sTable.stringvalue}';");
            using (MySqlDataAdapter adpt = new MySqlDataAdapter())
            {
                adpt.SelectCommand = (MySqlCommand)pCommand_Select;
                adpt.Fill(pTable);
            }
            Assert.IsTrue(pTable.Rows.Count > 0);



            // Act
            // Delete
            pConnector.PopCommand($"DELETE FROM {sTable.GetTableName()} WHERE {nameof(testtable.stringvalue)} = '{sTable.stringvalue}'; ").ExecuteNonQuery();
            pTable.Clear();


            // Assert
            pCommand_Select = pConnector.PopCommand($"SELECT * FROM {sTable.GetTableName()} WHERE {nameof(testtable.stringvalue)} = '{sTable.stringvalue}';");
            using (MySqlDataAdapter adpt = new MySqlDataAdapter())
            {
                adpt.SelectCommand = (MySqlCommand)pCommand_Select;
                adpt.Fill(pTable);
            }
            pCommand_Select.Dispose();

            Assert.IsTrue(pTable.Rows.Count == 0);


            pConnector.DoClose();
        }
    }
}