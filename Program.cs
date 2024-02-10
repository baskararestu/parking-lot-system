using System;
using System.Collections.Generic;
using ParkingLotSystem.Models;
using parking_lot.services;

public class Program
{
    public static void Main(string[] args)
    {
        ParkingLot parkingLot = null;

        while (true)
        {
            Console.Write("$ ");
            string command = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(command))
            {
                continue;
            }

            string[] parts = command.Split(' ');

            int slotNumber;
            List<int> slotNumbers;
            string color;
            switch (parts[0])
            {
                case "create_parking_lot":
                    int capacity = int.Parse(parts[1]);
                    parkingLot = new ParkingLot(capacity);
                    Console.WriteLine($"Created a parking lot with {capacity} slots");
                    break;

                case "park":
                    if (parkingLot == null)
                    {
                        Console.WriteLine("Please create a parking lot first.");
                        break;
                    }
                    string licensePlate = parts[1];
                    string typeVehicle = parts[2];
                    color = parts[3];
                    Vehicle vehicle = new Vehicle { LicensePlate = licensePlate, Type = typeVehicle, Color = color };
                    try
                    {
                        ParkingTicket ticket = parkingLot.ParkVehicle(vehicle);
                        Console.WriteLine($"Allocated slot number: {ticket.ParkingSpace.Id}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "status":
                    if (parkingLot == null)
                    {
                        Console.WriteLine("Please create a parking lot first.");
                        break;
                    }
                    Console.WriteLine("Slot\tNo. \t\t\tType \t\tRegistration No\tColour");
                    foreach (var ticket in parkingLot.GetParkedVehicles())
                    {
                        Console.WriteLine($"{ticket.ParkingSpace.Id}\t{ticket.Vehicle.LicensePlate}\t\t{ticket.Vehicle.Type}\t\t{ticket.Vehicle.Color}");
                    }
                    break;

                case "leave":
                    if (parkingLot == null)
                    {
                        Console.WriteLine("Please create a parking lot first.");
                        break;
                    }
                    slotNumber = int.Parse(parts[1]);
                    try
                    {
                        parkingLot.LeaveVehicle(slotNumber);
                        Console.WriteLine($"Slot number {slotNumber} is free");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                
                
                    case "type_of_vehicles":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Please create a parking lot first.");
                            break;
                        }
                        string type = parts[1];
                        int count = parkingLot.CountVehiclesOfType(type);
                        Console.WriteLine(count);
                        break;

                    case "registration_numbers_for_vehicles_with_colour":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Please create a parking lot first.");
                            break;
                        }
                        color = parts[1];
                        var regNumbers = parkingLot.GetRegistrationNumbersByColor(color);
                        Console.WriteLine(string.Join(", ", regNumbers));
                        break;

                    case "slot_numbers_for_vehicles_with_colour":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Please create a parking lot first.");
                            break;
                        }
                        color = parts[1];
                        slotNumbers = parkingLot.GetSlotNumbersByColor(color);
                        Console.WriteLine(string.Join(", ", slotNumbers));
                        break;

                    case "slot_number_for_registration_number":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Please create a parking lot first.");
                            break;
                        }
                        string regNumber = parts[1];
                        slotNumber = parkingLot.GetSlotNumberByRegistrationNumber(regNumber);
                        if (slotNumber == -1)
                        {
                            Console.WriteLine("Not found");
                        }
                        else
                        {
                            Console.WriteLine(slotNumber);
                        }
                        break;
                
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }
}
