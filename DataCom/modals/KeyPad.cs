using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using static DataCom.modals.enums.Definitions;

namespace DataCom.modals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class KeyPad: TreeViewItem
    {
        [JsonProperty]
        public List<Key> keys { get; set; }

        public KEYPAD_TYPE type { get; set; }

        [JsonProperty]
        public int shortAddress { get; set; }
        [JsonProperty]
        public string uuid { get; set; }

        [JsonProperty]
        public int numOfKeys { get; set; }

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
                case KEYPAD_TYPE.KeypadCatogory_Keypad_5_plus_1:
                    setNumbers(6);
                    break;
                case KEYPAD_TYPE.KeypadCatogory_Keypad_8_plus_2:
                    setNumbers(10);
                    break;
                case KEYPAD_TYPE.KeypadCatogory_Keypad_10_plus_2:
                    setNumbers(12);
                    break;
            }
        }

        public void setNumbers(int num)
        {
            numOfKeys = num;
            //keys = Enumerable.Repeat(new Key(), num).ToList();
            for (int i = 0; i < num; i++)
            {
                Key item = new Key(i);
                keys.Add(item);
            }
        }

        [JsonProperty]
        public string CustomLabel
        {
            get { return Header == null ? null : Header.ToString(); }
            set { Header = value; }
        }
    }
}
