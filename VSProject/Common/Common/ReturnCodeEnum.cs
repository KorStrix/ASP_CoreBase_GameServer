using System.Runtime.InteropServices;

namespace Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public partial class ReturnCodeEnum : IEnumClass
    {
        public ulong iValue { get; set; }
        public string strEnumName { get; set; } = string.Empty;

        public string Contents { get; set; } = string.Empty;

        #region operator overloading

        public static bool operator ==(ReturnCodeEnum x, ReturnCodeEnum y)
        {
            return x.strEnumName.Equals(y.strEnumName);
        }

        public static bool operator !=(ReturnCodeEnum x, ReturnCodeEnum y)
        {
            return x.strEnumName.Equals(y.strEnumName) == false;
        }


        public static ReturnCodeEnum operator |(ReturnCodeEnum x, ReturnCodeEnum y)
        {
            return new ReturnCodeEnum(
                $"({x.strEnumName} || {y.strEnumName})",
                x.iValue | y.iValue,
                $"({x.Contents} || {y.Contents})"
                );
        }

        public static ReturnCodeEnum operator &(ReturnCodeEnum x, ReturnCodeEnum y)
        {
            return new ReturnCodeEnum(
                $"({x.strEnumName} && {y.strEnumName})",
                x.iValue & y.iValue,
                $"({x.Contents} && {y.Contents})"
            );
        }

        public static implicit operator string(ReturnCodeEnum pReturnCode) => pReturnCode.ToString();

        #endregion

        public ReturnCodeEnum DoAddContents(string strContents)
        {
            this.Contents += strContents;
            return this;
        }

        protected ReturnCodeEnum()
        {
        }

        protected ReturnCodeEnum(string strEnumName, ulong iValue)
        {
            this.strEnumName = strEnumName;
            this.iValue = iValue;
        }

        protected ReturnCodeEnum(string strEnumName, ulong iValue, string strContents)
        {
            this.strEnumName = strEnumName;
            this.iValue = iValue;
            this.Contents = strContents;
        }

        public override string ToString()
        {
            return $"{strEnumName}({iValue}) - {Contents}";
        }
    }
}
