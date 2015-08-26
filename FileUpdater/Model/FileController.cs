using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileUpdater.Model {
	class FileController {
		private const string SOURCE_ERROR = "Source file does not exist";

		private BaseFile[] destinations;
		private BaseFile source;
		private FileSystemWatcher watcher;

		//internal event EventHandler<String> FileDoesNotExist;

		internal FileController(string sourcePath, string destinationPath) {
			RegisterSource(sourcePath, destinationPath);
		}

		internal FileController(BaseFile sourceFile, BaseFile destinationFile) {
			RegisterPaths(sourceFile, destinationFile);
		}

		private void RegisterSource(string sourcePath, string destinationPath = "") {
			source = new BaseFile(sourcePath);
			if (!source.FileInfo.Exists) {
				//TODO: here must be notification message
				throw new FileNotFoundException("Source file did not find");
			}
			CreateWatcher(source.FileInfo.DirectoryName, source.FileInfo.Name);

			if (destinationPath != null && !destinationPath.Equals("")) {
				BaseFile dest = new BaseFile(destinationPath);
				if (!dest.FileInfo.Exists) {
					//TODO: here must be notification message
					throw new FileNotFoundException("Destination file did not find");
				}
				destinations[destinations.Length] = dest;
			}
		}

		private void RegisterPaths(BaseFile sourceFile, BaseFile destinationFile = null) {
			if (!sourceFile.FileInfo.Exists) {
				//TODO: here must be notification message
				throw new FileNotFoundException("Source file did not find");
			}
			source = sourceFile;
			CreateWatcher(source.FileInfo.DirectoryName, source.FileInfo.Name);

			if (destinationFile != null) {
				if (!destinationFile.FileInfo.Exists) {
					//TODO: here must be notification message
					throw new FileNotFoundException("Source file did not find");
				}
				destinations[destinations.Length] = destinationFile;
			}
		}

		private void CreateWatcher(string directoryPath, string fileName) {
			if (watcher == null) {
				watcher = new FileSystemWatcher(directoryPath, fileName);
				watcher.Changed += watcher_Changed;
			} else {
				watcher.Path = directoryPath;
				watcher.Filter = fileName;
			}
		}

		void watcher_Changed(object sender, FileSystemEventArgs e) {
			for (int i = 0, l = destinations.Length; i < l; i++) {
				try {
					File.Copy(source.FileInfo.FullName, destinations[i].FileInfo.FullName, true);
				} catch (Exception exc) {
					//TODO: notify user abdout this exception
				}
			}
		}
	}
}
