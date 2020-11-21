using System;

namespace ToDo.Business.Result
{
    public class Errors
    {
        public static Meta MissingParameter(String about = "")
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "Missing parameter",
                MessageDetail = about,
                Code = 400
            };

        }

        public static Meta ServerError(String about = "")
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "Server Error",
                MessageDetail = about,
                Code = 500
            };

        }

        public static Meta Custom(String about = "")
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "unexpected error",
                MessageDetail = about,
                Code = 424
            };

        }

    }
}
