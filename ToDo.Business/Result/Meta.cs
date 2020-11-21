using Newtonsoft.Json;

namespace ToDo.Business.Result
{
    public class Meta
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public int Code { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
