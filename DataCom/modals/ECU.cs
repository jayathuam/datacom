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
    public class ECU : TreeViewItem
    {
        public int numberOFPOsitiveOutputs { get; set; }
        public int numberOFNEgativeOutputs { get; set; }
        public int numberOFExternalOutputs { get; set; }
        public int numberOFAnalogInput { get; set; }
        public int numberOFCombineInputs { get; set; }
        public int numberOFEvents { get; set; }

        [JsonProperty]
        public List<PositiveOutput> positiveList { get; set; }
        [JsonProperty]
        public List<NegativeOutput> negativeList { get; set; }
        [JsonProperty]
        public List<ExternalInput> externalList { get; set; }
        [JsonProperty]
        public List<AnalogInput> analogList { get; set; }
        [JsonProperty]
        public List<CombineInputs> combineList { get; set; }
        [JsonProperty]
        public List<Events> eventsList { get; set; }
        [JsonProperty]
        public LoadShading loadShading { get; set; }
        [JsonProperty]
        public PowerManagement powerManagement { get; set; }

        public ECU_TYPE type { get; set; }

        [JsonProperty]
        public int shortAddress { get; set; }
        [JsonProperty]
        public string uuid { get; set; }

        public ECU()
        {
            positiveList = new List<PositiveOutput>();
            negativeList = new List<NegativeOutput>();
            externalList = new List<ExternalInput>();
            analogList = new List<AnalogInput>();
            combineList = new List<CombineInputs>();
            eventsList = new List<Events>();
        }

        public ECU(ECU_TYPE ecuType)
        {
            positiveList = new List<PositiveOutput>();
            negativeList = new List<NegativeOutput>();
            externalList = new List<ExternalInput>();
            analogList = new List<AnalogInput>();
            combineList = new List<CombineInputs>();
            eventsList = new List<Events>();
            this.type = ecuType;
            init(ecuType);
        }

        public string label { get; set; }

        private void init(ECU_TYPE ecuType)
        {
            switch (ecuType)
            {
                case ECU_TYPE.MainboardCatogory_System_1:
                    setNumbers(5, 2, 4, 2, 10, 10);
                    break;
                case ECU_TYPE.MainboardCatogory_System_2:
                    setNumbers(8, 8, 8, 3, 10, 10);
                    break;
                case ECU_TYPE.MainboardCatogory_System_3:
                    setNumbers(15, 10, 10, 3, 10, 10);
                    break;
                case ECU_TYPE.MainboardCatogory_System_4:
                    setNumbers(20, 10, 10, 3, 10, 10);
                    break;

            }
        }

        public void setNumbers(int positive, int negative, int external, int analog, int combine, int events)
        {
            numberOFPOsitiveOutputs = positive;
            numberOFNEgativeOutputs = negative;
            numberOFExternalOutputs = external;
            numberOFAnalogInput = analog;
            numberOFCombineInputs = combine;
            numberOFEvents = events;

            for (int i = 0; i < positive; i++)
            {
                PositiveOutput item = new PositiveOutput(i);                
                positiveList.Add(item);
            }

            for (int i = 0; i < negative; i++)
            {
                NegativeOutput item = new NegativeOutput(i);
                negativeList.Add(item);
            }

            for (int i = 0; i < external; i++)
            {
                ExternalInput item = new ExternalInput(i);
                externalList.Add(item);
            }

            for (int i = 0; i < analog; i++)
            {
                AnalogInput item = new AnalogInput(i);
                analogList.Add(item);
            }

            for (int i = 0; i < combine; i++)
            {
                CombineInputs item = new CombineInputs(i);
                combineList.Add(item);
            }

            for (int i = 0; i < events; i++)
            {
                Events item = new Events();
                eventsList.Add(item);
            }         

            loadShading = new LoadShading();
            powerManagement = new PowerManagement();
        }

        [JsonProperty]
        public string CustomLabel
        {
            get { return Header == null ? null : Header.ToString(); }
            set { Header = value; }
        }
    }
}
