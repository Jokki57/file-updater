using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileUpdater.Model {
	public class FileController {
		private const string SOURCE_ERROR = "Source file does not exist";

		private Dictionary<string, BaseFile> destinations = new Dictionary<string, BaseFile>();
		private BaseFile source;
		private FileSystemWatcher watcher;

		internal FileInfo SourceFileInfo { get { return source.FileInfo; } }

		//internal event EventHandler<String> FileDoesNotExist;

		internal FileController(string sourcePath, string destinationPath) {
			RegisterSource(sourcePath, destinationPath);
		}

		internal FileController(string sourcePath) {
			RegisterSource(sourcePath);
		}

		internal FileController(BaseFile sourceFile, BaseFile destinationFile) {
			RegisterPaths(sourceFile, destinationFile);
		}

		internal void AddDestionationFile(string destinationPath) {
			BaseFile dest = new BaseFile(destinationPath);
			if (!dest.FileInfo.Exists) {
				//TODO: here must be notification message
				throw new FileNotFoundException("Destination file does not find");
			}
			destinations.Add(destinationPath, dest);
		}

		internal void AddDestionationFile(BaseFile destinationFile) {
			if (!destinationFile.FileInfo.Exists) {
				//TODO: here must be notification message
				throw new FileNotFoundException("Destination file does not find");
			}
			destinations.Add(destinationFile.Path, destinationFile);
		}

        internal void RemoveDestinationFile(string destination)
        {
            destinations.Remove(destination);
        }

        internal void Dispose()
        {
            watcher.Changed -= watcher_Changed;
            watcher.Dispose();
           
        }

		private void RegisterSource(string sourcePath, string destinationPath = "") {
			source = new BaseFile(sourcePath);
			if (!source.FileInfo.Exists) {
				//TODO: here must be notification message
				throw new FileNotFoundException("Source file does not find");
			}
			CreateWatcher(source.FileInfo.DirectoryName, source.FileInfo.Name);

			if (destinationPath != null && !destinationPath.Equals("")) {
				BaseFile dest = new BaseFile(destinationPath);
				if (!dest.FileInfo.Exists) {
					//TODO: here must be notification message
					throw new FileNotFoundException("Destination file does not find");
				}
				destinations.Add(destinationPath, dest);
			}
		}

		private void RegisterPaths(BaseFile sourceFile, BaseFile destinationFile = null) {
			if (!sourceFile.FileInfo.Exists) {
				//TODO: here must be notification message
				throw new FileNotFoundException("Source file does not find");
			}
			source = sourceFile;
			CreateWatcher(source.FileInfo.DirectoryName, source.FileInfo.Name);

			if (destinationFile != null) {
				if (!destinationFile.FileInfo.Exists) {
					//TODO: here must be notification message
					throw new FileNotFoundException("Source file does not find");
				}
				destinations.Add(destinationFile.Path, destinationFile);
			}
		}

		private void CreateWatcher(string directoryPath, string fileName) {
			if (watcher == null) {
				watcher = new FileSystemWatcher(directoryPath, fileName);
                watcher.EnableRaisingEvents = true;
				watcher.Changed += watcher_Changed;
			} else {
				watcher.Path = directoryPath;
				watcher.Filter = fileName;
			}
		}

		void watcher_Changed(object sender, FileSystemEventArgs e) {
			try {
                foreach (BaseFile dest in destinations.Values)
                {
                    File.Copy(source.FileInfo.FullName, dest.FileInfo.FullName, true);
                }
			} catch (Exception exc) {
				//TODO: notify user abdout this exception
			}
			
		}
	}
}
