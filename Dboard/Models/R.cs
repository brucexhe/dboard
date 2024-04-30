namespace Dboard
{
    public class R
    {
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }


        public static R Success(object data, string message = "success")
        {
            R r = new R();
            r.code = 200;
            r.data = data;
            r.message = message;

            return r;
        }

        public static R Error(string message = "error", int code = 500)
        {
            R r = new R();
            r.code = code;
            r.message = message;

            return r;
        }
    }
}
