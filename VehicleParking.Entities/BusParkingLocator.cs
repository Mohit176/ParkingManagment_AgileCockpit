using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleParking.Interfaces;

namespace VehicleParking.Entities
{
    public class BusParkingLocator : IParkingLocator
    {
        private readonly IDictionary<int, LinkedList<LinkedList<IParkingSlot>>> _parkingLookup;

        public BusParkingLocator(IDictionary<int, LinkedList<LinkedList<IParkingSlot>>> parkingLookup)
        {
            this._parkingLookup = parkingLookup;
        }
        public IParkingSlot LocateFreeSlots(VehicleBase vehicle)
        {
            IParkingSlot largeSlot = null;
            int requiredSpace = 5; //it can be read from configuration file. 
            int counter = 0;

            foreach (var level in _parkingLookup)
            {
                foreach (var row in level.Value)
                {
                    LinkedListNode<IParkingSlot> currentNode = row.First;


                    while (currentNode != null)
                    {
                        if (currentNode.Value is LargeSlot && !currentNode.Value.IsOccupied)
                        {
                            counter++;

                            if (counter == requiredSpace)
                            {
                                largeSlot = currentNode.Value;
                                return largeSlot;
                            }
                        }
                        currentNode = currentNode.Next;
                    }


                }
            }

            return largeSlot;
        }

               

    }
}