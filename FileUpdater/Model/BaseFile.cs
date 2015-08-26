using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileUpdater.Model {
	class BaseFile {
		private FileInfo fileInfo;
		protected string path;

		internal string Path { get { return path; } }
		internal FileInfo FileInfo { get { return fileInfo; } }

		internal BaseFile(string path = "") {
			this.path = path;
			fileInfo = new FileInfo(path);
		}
	}
}
