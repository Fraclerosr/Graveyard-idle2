using System.Collections.Generic;

namespace BabyStack.Model
{
    public class InteractSpeedRateModification : Modification<float>
    {
        private const string GUID = "InteractSpeedRateGUID";

        public InteractSpeedRateModification()
            : base(GUID) { }

        public override List<ModificationData<float>> Data
        {
            get
            {
                return new List<ModificationData<float>>()
                {
                    new ModificationData<float>(100, 1.0f),
                    new ModificationData<float>(200, 1.2f),
                    new ModificationData<float>(300, 1.4f),
                    new ModificationData<float>(500, 1.6f),
                    new ModificationData<float>(700, 1.8f),
                    new ModificationData<float>(900, 2.0f),
                    new ModificationData<float>(1200, 2.2f),
                    new ModificationData<float>(1500, 2.4f),
                };
            }
        }
    }
}