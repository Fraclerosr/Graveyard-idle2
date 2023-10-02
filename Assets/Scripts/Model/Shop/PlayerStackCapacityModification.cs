using System.Collections.Generic;

namespace BabyStack.Model
{
    public class PlayerStackCapacityModification : Modification<int>
    {
        private const string GUID = "PlayerStackCapacityModificationGUID";

        public PlayerStackCapacityModification()
            : base(GUID) { }

        public override List<ModificationData<int>> Data
        {
            get
            {
                return new List<ModificationData<int>>()
                {
                    new ModificationData<int>(300, 3),
                    new ModificationData<int>(200, 4),
                    new ModificationData<int>(400, 5),
                    new ModificationData<int>(700, 6),
                    new ModificationData<int>(1000, 7),
                    new ModificationData<int>(1500, 8),
                };
            }
        }
    }
}