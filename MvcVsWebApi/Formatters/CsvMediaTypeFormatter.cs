using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CsvHelper;

namespace MvcVsWebApi.Formatters
{
	public class CsvMediaTypeFormatter : BufferedMediaTypeFormatter
	{
		public CsvMediaTypeFormatter()
		{
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
		}

		public override bool CanWriteType(Type type)
		{
			return type.GetInterfaces().Contains(typeof (IEnumerable));
		}
		public override bool CanReadType(Type type)
		{
			return false;
		}

		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			using (var writer = new StreamWriter(writeStream))
			using (var csv = new CsvWriter(writer))
			{
				csv.WriteRecords((IEnumerable)value);
				writer.Flush();
			}
		}
	}
}