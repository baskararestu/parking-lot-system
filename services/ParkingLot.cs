using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLotSystem.Models;

namespace parking_lot.services
{
    public class ParkingLot
    {
        private List<ParkingSpace> _spaces;
        private List<ParkingTicket> _parkedVehicles;

        public ParkingLot(int capacity)
        {
            _spaces = new List<ParkingSpace>();
            for (int i = 1; i <= capacity; i++)
            {
                _spaces.Add(new ParkingSpace { Id = i, IsOccupied = false });
            }
            _parkedVehicles = new List<ParkingTicket>();
        }

        public ParkingTicket ParkVehicle(Vehicle vehicle)
        {
            var availableSpace = _spaces.FirstOrDefault(s => !s.IsOccupied);
            if (availableSpace == null)
            {
                throw new Exception("Parking lot is full");
            }

            availableSpace.IsOccupied = true;

            var ticket = new ParkingTicket
            {
                Vehicle = vehicle,
                ParkingSpace = availableSpace,
            };
            _parkedVehicles.Add(ticket);
            return ticket;
        }

        public List<ParkingTicket> GetParkedVehicles()
        {
            return _parkedVehicles;
        }

        public void LeaveVehicle(int slotNumber)
        {
            var ticket = _parkedVehicles.FirstOrDefault(t => t.ParkingSpace.Id == slotNumber);
            if (ticket == null)
            {
                throw new Exception($"Slot number {slotNumber} is already empty");
            }

            ticket.ParkingSpace.IsOccupied = false;
            _parkedVehicles.Remove(ticket);
        }
        
        public int CountVehiclesOfType(string type)
        {
            return _parkedVehicles.Count(ticket => ticket.Vehicle.Type == type);
        }

        public List<string> GetRegistrationNumbersByColor(string color)
        {
            return _parkedVehicles.Where(ticket => ticket.Vehicle.Color == color).Select(ticket => ticket.Vehicle.LicensePlate).ToList();
        }

        public List<int> GetSlotNumbersByColor(string color)
        {
            return _parkedVehicles.Where(ticket => ticket.Vehicle.Color == color).Select(ticket => ticket.ParkingSpace.Id).ToList();
        }

        public int GetSlotNumberByRegistrationNumber(string regNumber)
        {
            var ticket = _parkedVehicles.FirstOrDefault(t => t.Vehicle.LicensePlate == regNumber);
            return ticket != null ? ticket.ParkingSpace.Id : -1;
        }

    }
}