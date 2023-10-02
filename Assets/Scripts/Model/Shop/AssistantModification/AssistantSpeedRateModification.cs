using System.Collections.Generic;

namespace BabyStack.Model
{
    public class AssistantSpeedRateModification : Modification<float>
    {
        private const string GUID = "AssistantsSpeedRateGUID";

        public AssistantSpeedRateModification()
            : base(GUID) { }

        public override List<ModificationData<float>> Data
        {
            get
            {
                return new List<ModificationData<float>>()
                {
                    new ModificationData<float>(100, 1f),
                    new ModificationData<float>(200, 1.1f),
                    new ModificationData<float>(300, 1.2f),
                    new ModificationData<float>(400, 1.3f),
                    new ModificationData<float>(500, 1.4f),
                    new ModificationData<float>(650, 1.5f),
                    new ModificationData<float>(800, 1.6f),
                    new ModificationData<float>(1000, 1.7f),
                };
            }
        }
    }
}