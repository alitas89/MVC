using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLayer.Filters
{
    [DataContract]
    public class ApiResponse<T>
    {
        public ApiResponse(bool success, HttpStatusCode statusCode, T result, string errorMessage = null)
        {
            Success = success;
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }

        public ApiResponse()
        {

        }

        [DataMember]
        public string Version => "1.0";

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }
    }
}
