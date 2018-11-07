using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static DataCom.modals.enums.Definitions;

namespace DataCom.modals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class KeyPad: TreeViewItem
    {
        [JsonProperty]
        public List<Key> keys { get; set; }

        public KeyPad()
        {
            keys = new List<Key>();
        }

        public KeyPad(KEYPAD_TYPE keypadType)
        {
            keys = new List<Key>();
            init(keypadType);                      
        }

        public string label { get; set; }

        private void init(KEYPAD_TYPE keypadType)
        {
            switch (keypadType)
            {
                case KEYPAD_TYPE.type_1:
                    setNumbers(6);
                    break;
                case KEYPAD_TYPE.type_2:
                    setNumbers(8);
                    break;
                case KEYPAD_TYPE.type_3:
                    setNumbers(10);
                    break;
            }
        }

        private void setNumbers(int num)
        {
            //keys = Enumerable.Repeat(new Key(), num).ToList();
            for(int i = 0; i < num; i++)
            {
                Key item = new Key();
                keys.Add(item);
            }
        }
    }
}
