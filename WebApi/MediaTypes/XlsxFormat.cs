using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using WebApiContrib.Formatting.Xlsx;

namespace WebApi.MediaTypes
{
    public class XlsxFormat: XlsxMediaTypeFormatter
    {
        public XlsxFormat()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xlsx"));
        }

        public XlsxFormat(MediaTypeMapping mediaTypeMapping) : this()
        {
            MediaTypeMappings.Add(mediaTypeMapping);
        }

        public XlsxFormat(IEnumerable<MediaTypeMapping> mediaTypeMappings) : this()
        {
            foreach (var mediaTypeMapping in mediaTypeMappings)
                MediaTypeMappings.Add(mediaTypeMapping);
        }
    }
}