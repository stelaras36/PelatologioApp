using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PelatologioApp.Models;

namespace PelatologioApp.Data
{
    public static class FileStorage
    {
        // Φάκελος δεδομένων: My Documents\PelatologioData
        private static readonly string DataDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PelatologioData");

        private static readonly string CustomersPath = Path.Combine(DataDir, "customers.json");
        private static readonly string AppointmentsPath = Path.Combine(DataDir, "appointments.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Δημιουργεί τον φάκελο αν δεν υπάρχει
        private static void EnsureDir()
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);
        }

        // Για να βλέπουμε πού γράφει (debug)
        public static string GetDataDir() => DataDir;

        // -------- Customers --------
        public static List<Customer> LoadCustomers()
        {
            EnsureDir();

            if (!File.Exists(CustomersPath))
                return new List<Customer>();

            var json = File.ReadAllText(CustomersPath);
            return JsonSerializer.Deserialize<List<Customer>>(json, JsonOptions)
                   ?? new List<Customer>();
        }

        public static void SaveCustomers(List<Customer> customers)
        {
            EnsureDir();

            var json = JsonSerializer.Serialize(customers, JsonOptions);
            File.WriteAllText(CustomersPath, json);
        }

        // -------- Appointments --------
        public static List<Appointment> LoadAppointments()
        {
            EnsureDir();

            if (!File.Exists(AppointmentsPath))
                return new List<Appointment>();

            var json = File.ReadAllText(AppointmentsPath);
            return JsonSerializer.Deserialize<List<Appointment>>(json, JsonOptions)
                   ?? new List<Appointment>();
        }

        public static void SaveAppointments(List<Appointment> appointments)
        {
            EnsureDir();

            var json = JsonSerializer.Serialize(appointments, JsonOptions);
            File.WriteAllText(AppointmentsPath, json);
        }
    }
}
