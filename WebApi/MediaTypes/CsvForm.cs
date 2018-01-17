using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EntityLayer.Concrete;
using WebApiContrib.Formatting;

namespace WebApi.MediaTypes
{
    public class CsvForm : CSVMediaTypeFormatter
    {
        public CsvForm()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public CsvForm(MediaTypeMapping mediaTypeMapping) : this()
        {
            MediaTypeMappings.Add(mediaTypeMapping);
        }

        public CsvForm(IEnumerable<MediaTypeMapping> mediaTypeMappings) : this()
        {
            foreach (var mediaTypeMapping in mediaTypeMappings)
                MediaTypeMappings.Add(mediaTypeMapping);
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return IsTypeOfIEnumerable(type);
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.Add("Content-Disposition", "attachment; filename=" + Guid.NewGuid().ToString() + ".csv");
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() => WriteStream(type, value, stream, content));
        }

        private static void WriteStream(Type type, object value, Stream stream, HttpContent content)
        {
            //NOTE: We have check the type inside CanWriteType method
            //If request comes this far, the type is IEnumerable. We are safe.

            Type itemType = type.GetGenericArguments()[0];

            using (StringWriter stringWriter = new StringWriter())
            {
                stringWriter.WriteLine(
                    string.Join<string>(
                        ",", itemType.GetProperties().Select(x => x.Name)
                    )
                );

                foreach (var obj in (IEnumerable<object>)value)
                {

                    var vals = obj.GetType().GetProperties().Select(
                        pi => new
                        {
                            Value = pi.GetValue(obj, null)
                        }
                    );

                    string _valueLine = string.Empty;

                    foreach (var val in vals)
                    {

                        if (val.Value != null)
                        {

                            var _val = val.Value.ToString();

                            //Check if the value contans a comma and place it in quotes if so
                            if (_val.Contains(","))
                                _val = string.Concat("\"", _val, "\"");

                            //Replace any \r or \n special characters from a new line with a space
                            if (_val.Contains("\r"))
                                _val = _val.Replace("\r", " ");
                            if (_val.Contains("\n"))
                                _val = _val.Replace("\n", " ");

                            _valueLine = string.Concat(_valueLine, _val, ",");

                        }
                        else
                        {

                            _valueLine = string.Concat(string.Empty, ",");
                        }
                    }

                    stringWriter.WriteLine(_valueLine.TrimEnd(','));
                }


                var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

                streamWriter.Write(stringWriter.ToString());



                streamWriter.Flush();
                stream.Seek(0, SeekOrigin.Begin);

            }
        }

        private static bool IsTypeOfIEnumerable(Type type)
        {
            foreach (Type interfaceType in type.GetInterfaces())
                if (interfaceType == typeof(IEnumerable))
                    return true;

            return false;
        }
    }
}