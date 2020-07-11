using System.Data;

namespace DBConnector
{
    public interface IDB
    {
        IDBConnector GetConnector();
    }

    public interface ITable
    {
    }

    public interface IDBConnector
    {
        void DoOpen();
        void DoClose();

        IDbCommand PopCommand(string strQuery);
        string ConvertQueryString(ITable pTable);
    }

    public interface IDBCommand : IDbCommand
    {
    }
}
