using Newtonsoft.Json;

namespace ToDo.Business.Result
{
    public class MResult
    {
        public Meta Meta { get; set; }

        public MResult(Meta meta)
        {
            Meta = meta;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
