﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using SimpleBrowser.Internal;

namespace SimpleBrowser.Elements
{
	internal class FileUploadElement : InputElement, IHasRawPostData
	{
		public FileUploadElement(XElement element)
			: base(element)
		{
		}
		#region IHasRawPostData Members

		public string GetPostData()
		{
			string filename = this.Value;
			if (File.Exists(filename))
			{
				// Todo: create a mime type for extensions
				string extension = new FileInfo(filename).Extension;
				string contentType = String.Format("Content-Type: {0}\nContent-Transfer-Encoding: base64\n\n", 
					ApacheMimeTypes.MimeForExtension(extension));
				byte[] allBytes = File.ReadAllBytes(filename);
				return contentType + Convert.ToBase64String(allBytes);
			}
			return "";
		}

		#endregion
	}

	public interface IHasRawPostData
	{
		string GetPostData();
	}
}
