using System.Collections.Generic;

namespace BabyStack.Model
{
    public class AssistantStackCapacityModification : Modification<int>
    {
        private const string GUID = "AssistantsStackCapacityGUID";

        public AssistantStackCapacityModification()
            : base(GUID) { }

        public override List<ModificationData<int>> Data
        {
            get
            {
                return new List<ModificationData<int>>()
                {
                    new ModificationData<int>(100, 2),
                    new ModificationData<int>(200, 3),
                    new ModificationData<int>(400, 4),
                    new ModificationData<int>(600, 5),
                    new ModificationData<int>(1000, 6),
                };
            }
        }
    }
}