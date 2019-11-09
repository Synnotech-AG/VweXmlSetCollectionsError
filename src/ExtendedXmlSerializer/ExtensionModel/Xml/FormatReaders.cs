using System;
using System.Xml;
using ExtendedXmlSerializer.ContentModel.Format;

namespace ExtendedXmlSerializer.ExtensionModel.Xml
{
	sealed class FormatReaders : IFormatReaders<System.Xml.XmlReader>
	{
		readonly IFormatReaderContexts _read;

		public FormatReaders(IFormatReaderContexts read)
		{
			_read = read;
		}

		public IFormatReader Get(System.Xml.XmlReader parameter)
		{
			switch (parameter.MoveToContent())
			{
				case XmlNodeType.Element:
					var result = new XmlReader(_read.Get(parameter), parameter);
					return result;
				default:
					throw new
						InvalidOperationException($"Could not locate the content from the Xml reader '{parameter}.'");
			}
		}
	}
}