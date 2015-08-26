using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpdater.Model {
	public class MainController {
		private List<FileController> controllers;

		public List<FileController> Controllers { get { return controllers; } }

		MainController() {
			controllers = new List<FileController>();
		}

		MainController(string source, string[] destinations) : this() {
			controllers.Add(new FileController(source, destinations));
		}
	}
}
