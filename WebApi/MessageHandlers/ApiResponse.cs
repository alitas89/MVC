using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using EntityLayer.Concrete;

namespace WebApi.MessageHandlers
{
    //[KnownType(typeof(List<Test>))]
    [DataContract]
    public class ApiResponse<T>
    {
        public ApiResponse(bool success, HttpStatusCode statusCode, T result,
            string errorMessage = null, string clientErrorMessage = null)
        {
            Success = success;
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
            ClientErrorMessage = clientErrorMessage;
        }

        public ApiResponse()
        {

        }

        private string _version;
        [DataMember]
        public string Version { get { return "1.0"; } set { this._version = value; } }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ClientErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }
    }

}