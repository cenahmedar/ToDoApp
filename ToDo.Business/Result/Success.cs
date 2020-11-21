using System;

namespace ToDo.Business.Result
{
    public class Success
    {

        public static Meta success = new Meta
        {
            IsSuccess = true,
            Message = "Success",
            MessageDetail = "",
            Code = 0
        };

        public static Meta notFound = new Meta
        {
            IsSuccess = true,
            Message = "Record Not Found",
            MessageDetail = "",
            Code = 0
        };


        public static Meta CustomSuccess(Meta meta, String about)
        {
            return new Meta
            {
                IsSuccess = true,
                Message = meta.Message,
                MessageDetail = about
            };

        }
        public static Meta CustomSuccess(Meta meta, String about, int code)
        {
            return new Meta
            {
                IsSuccess = true,
                Message = meta.Message,
                MessageDetail = about,
                Code = code
            };

        }

    }
}
