namespace Common
{
    public partial class ReturnCodeEnum 
    {
        public static ReturnCodeEnum None => new ReturnCodeEnum(nameof(None), 0);
        public static ReturnCodeEnum UnknownError => new ReturnCodeEnum(nameof(UnknownError), 1);
        public static ReturnCodeEnum JsonParseError => new ReturnCodeEnum(nameof(JsonParseError), 1);
        public static ReturnCodeEnum NetworkError => new ReturnCodeEnum(nameof(NetworkError), 1);

    }
}
