using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApiContrib.Formatting;
using Font = iTextSharp.text.Font;

namespace WebApi.MediaTypes
{
    public class PdfFormat : MediaTypeFormatter
    {
        public PdfFormat()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/pdf"));
        }

        public PdfFormat(MediaTypeMapping mediaTypeMapping) : this()
        {
            MediaTypeMappings.Add(mediaTypeMapping);
        }

        public PdfFormat(IEnumerable<MediaTypeMapping> mediaTypeMappings) : this()
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
            headers.Add("Content-Disposition", "attachment; filename=" + Guid.NewGuid() + ".pdf");
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() => WriteStream(type, value, stream, content));
        }

        private static void WriteStream(Type type, object value, Stream stream, HttpContent content)
        {
            Type itemType = type.GetGenericArguments()[0];
            MemoryStream myMemoryStream = new MemoryStream();

            Document myDocument = new Document();
            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            //Full path to the Unicode Arial file
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.TTF");

            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //Create a specific font object
            Font NormalFont = new Font(bf, 6, Font.NORMAL);


            using (PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream))
            {
                myDocument.Open();

                //Başlıklar oluşturulur
                var listHeader = itemType.GetProperties().Select(x => x.Name);

                // Add to content to your PDF here...
                var itemHeaders = listHeader as IList<string> ?? listHeader.ToList();
                PdfPTable table = new PdfPTable(itemHeaders.Count); //sütun sayısı belirtilir

                //Ana başlık bilgisi
                PdfPCell header = new PdfPCell(new Phrase(DateTime.Now.ToShortDateString() + " Tarihli PDF Çıktısı", NormalFont));
                header.Colspan = itemHeaders.Count; //sütun sayısı belirtilir
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right - Hücre içindeki verilerin align durumu
                table.AddCell(header);

                //Başlıklar oluşturulur
                Font HeaderFont = new Font(bf, 6, Font.NORMAL, BaseColor.WHITE);
                foreach (var itemHeader in itemHeaders)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(itemHeader, HeaderFont));
                    cell.BackgroundColor = BaseColor.GRAY;
                    cell.HorizontalAlignment = 1;
                    //cell.BorderColor = new Color(255, 242, 0);
                    //table.AddCell(new Phrase(itemHeader, NormalFont));
                    cell.PaddingBottom = 4f;
                    cell.PaddingTop = 2f;
                    table.AddCell(cell);
                }


                //Veriler oluşturulur
                foreach (var obj in (IEnumerable<object>)value)
                {

                    var vals = obj.GetType().GetProperties().Select(
                        pi => new
                        {
                            Value = pi.GetValue(obj, null)
                        }
                    );
                    
                    foreach (var val in vals)
                    {
                        table.AddCell(val.Value != null
                            ? new Phrase(val.Value.ToString(), NormalFont)
                            : new Phrase("", NormalFont));
                    }
                }

                myDocument.Add(table);
                myDocument.Close();


                byte[] content2 = myMemoryStream.ToArray();
                streamWriter.BaseStream.Write(content2, 0, content2.Length);

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