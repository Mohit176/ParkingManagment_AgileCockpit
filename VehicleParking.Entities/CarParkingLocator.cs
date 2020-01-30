using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleParking.Interfaces;

namespace VehicleParking.Entities
{
    public class CarParkingLocator : IParkingLocator
    {
        private readonly IDictionary<int, LinkedList<LinkedList<IParkingSlot>>> _parkingLookup;

        public CarParkingLocator(IDictionary<int, LinkedList<LinkedList<IParkingSlot>>> parkingLookup)
        {
            _parkingLookup = parkingLookup;
        }
        public IParkingSlot LocateFreeSlots(VehicleBase vehicle)
        {
            IParkingSlot parkingSlot = null;

            foreach (var level in _parkingLookup)
            {
                parkingSlot = FindSlot(parkingSlot, level, typeof(CompactSlot));

                if (parkingSlot != null)
                {
                    break;
                }
                else
                {
                    parkingSlot = FindSlot(parkingSlot, level, typeof(LargeSlot));

                    if (parkingSlot != null)
                    {
                        break;
                    }

                }
            }

            return parkingSlot;
        }

        private static IParkingSlot FindSlot(IParkingSlot parkingSlot, KeyValuePair<int, LinkedList<LinkedList<IParkingSlot>>> level, Type type)
        {
            foreach (var row in level.Value)
            {
                var node = row.FirstOrDefault(x => x.GetType() == type && x.IsOccupied == false);

                if (node != null)
                {
                    parkingSlot = node;
                    break;
                }

            }

            return parkingSlot;
        }

    }
}
