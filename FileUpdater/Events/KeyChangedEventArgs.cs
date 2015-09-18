using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpdater.Events
{
    public class KeyChangedEventArgs : EventArgs
    {
        private int oldKey;
        private int newKey;

        public int OldKey { get { return oldKey; } }
        public int NewKey {  get { return newKey; } }

        public KeyChangedEventArgs(int oldKey, int newKey) : base()
        {
            this.oldKey = oldKey;
            this.newKey = newKey;
        }
    }
}
