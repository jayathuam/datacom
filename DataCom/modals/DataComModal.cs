using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.modals
{
    public class DataComModal
    {

        public DataComModal()
        {
            ecus = new List<ECU>();
            keyPads = new List<KeyPad>();
        }

        public string comment { get; set; }
        public string version { get; set; }
        public string vehicle { get; set; }
        public string publisher { get; set; }
        public string cusName { get; set; }
        public string license { get; set; }
        public List<ECU> ecus { get; set; }
        public List<KeyPad> keyPads { get; set; }
    }
}
