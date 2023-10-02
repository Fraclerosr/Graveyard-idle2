using System.Collections.Generic;

namespace BabyStack.Model
{
    public class PlayerSpeedRateModification : Modification<float>
    {
        private const string GUID = "PlayerSpeedRateGUID";

        public PlayerSpeedRateModification()
            : base(GUID) { }

        public override List<ModificationData<float>> Data
        {
            get
            {
                return new List<ModificationData<float>>()
                {
                    new ModificationData<float>(100, 1f),
                    new ModificationData<float>(300, 1.1f),
                    new ModificationData<float>(500, 1.2f),
                    new ModificationData<float>(700, 1.3f),
                    new ModificationData<float>(900, 1.3f),
                    new ModificationData<float>(1200, 1.4f),
                    new ModificationData<float>(1400, 1.5f),
                };
            }
        }
    }
}