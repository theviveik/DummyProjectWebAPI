using Newtonsoft.Json;

namespace BusinessAccessLayer.Models
{
    public class Error
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
