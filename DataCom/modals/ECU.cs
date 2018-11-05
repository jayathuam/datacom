using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataCom.modals.enums.Definitions;

namespace DataCom.modals
{
    public class ECU
    {
        private int numberOFPOsitiveOutputs;
        private int numberOFNEgativeOutputs;
        private int numberOFExternalOutputs;
        private int numberOFAnalogInput;
        private int numberOFCombineInputs;
        private int numberOFEvents;

        public List<object> positiveList;
        public List<object> negativeList;
        public List<object> externalList;
        public List<object> analogList;
        public List<object> combineList;
        public List<object> eventsList;
        public LoadShading loadShading;
        public PowerManagement powerManagement;

        public ECU(ECU_TYPE ecuType)
        {
            positiveList = new List<object>();
            negativeList = new List<object>();
            externalList = new List<object>();
            analogList = new List<object>();
            combineList = new List<object>();
            eventsList = new List<object>();
            init(ecuType);
        }

        public string label { get; set; }

        private void init(ECU_TYPE ecuType)
        {
            switch (ecuType)
            {
                case ECU_TYPE.type_1:
                    setNumbers(5, 2, 4, 2, 10, 10);
                    break;
                case ECU_TYPE.type_2:
                    setNumbers(8, 8, 8, 3, 10, 10);
                    break;
                case ECU_TYPE.type_3:
                    setNumbers(15, 10, 10, 3, 10, 10);
                    break;
                case ECU_TYPE.type_4:
                    setNumbers(20, 10, 10, 3, 10, 10);
                    break;

            }
        }

        private void setNumbers(int positive, int negative, int external, int analog, int combine, int events)
        {
            numberOFPOsitiveOutputs = positive;
            numberOFNEgativeOutputs = negative;
            numberOFExternalOutputs = external;
            numberOFAnalogInput = analog;
            numberOFCombineInputs = combine;
            numberOFEvents = events;

            for (int i = 0; i < positive; i++)
            {
                PositiveOutput item = new PositiveOutput();
                positiveList.Add(item);
            }

            for (int i = 0; i < negative; i++)
            {
                NegativeOutput item = new NegativeOutput();
                negativeList.Add(item);
            }

            for (int i = 0; i < external; i++)
            {
                ExternalInput item = new ExternalInput();
                externalList.Add(item);
            }

            for (int i = 0; i < analog; i++)
            {
                AnalogInput item = new AnalogInput();
                analogList.Add(item);
            }

            for (int i = 0; i < combine; i++)
            {
                CombineInputs item = new CombineInputs();
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
    }
}
