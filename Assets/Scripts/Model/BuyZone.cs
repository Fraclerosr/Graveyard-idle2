using System;
using System.Collections;
using UnityEngine;

namespace BabyStack.Model
{
    [Serializable]
    public class BuyZone : SavedObject<BuyZone>
    {
        [SerializeField] private DynamicCost _dynamicCost;

        private static Hashtable _buyZones = new Hashtable();

        private BuyZone(int totalCost, string guid)
            : base(guid)
        {
            _dynamicCost = new DynamicCost(totalCost);
        }

        public event Action<bool> Unlocked;
        public event Action<int> CostUpdated;

        public int TotalCost => _dynamicCost.TotalCost;
        public int CurrentCost => _dynamicCost.CurrentCost;

        public static BuyZone GetZone(int totalCost, string guid)
        {
            if (_buyZones.ContainsKey(guid))
                return (BuyZone) _buyZones[guid];
            
            BuyZone buyZone = new BuyZone(totalCost, guid);
            _buyZones.Add(guid, buyZone);

            return buyZone;
        }

        public void ReduceCost(int value)
        {
            _dynamicCost.Subtract(value);
            CostUpdated?.Invoke(_dynamicCost.CurrentCost);

            if (_dynamicCost.CurrentCost == 0)
                Unlocked?.Invoke(false);
        }

        protected override void OnLoad(BuyZone loadedObject)
        {
            _dynamicCost = loadedObject._dynamicCost;

            if (_dynamicCost.CurrentCost == 0)
                Unlocked?.Invoke(true);
        }
    }
}