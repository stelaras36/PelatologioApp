using System;
using System.Collections.Generic;
using System.Linq;
using PelatologioApp.Models;

namespace PelatologioApp.Data
{
    public class AppointmentRepository
    {
        private readonly List<Appointment> _appointments;

        public AppointmentRepository()
        {
            _appointments = FileStorage.LoadAppointments();
        }

        private void Save() => FileStorage.SaveAppointments(_appointments);

        private int NextId() => _appointments.Count == 0
            ? 1
            : _appointments.Max(a => a.Id) + 1;

        /// <summary>
        /// Έλεγχος αν υπάρχει άλλο ραντεβού που "χτυπάει" με το [start,end)
        /// για τον ΙΔΙΟ χρήστη (UserCode). Επιτρέπουμε ίδιες ώρες για διαφορετικούς users.
        /// </summary>
        private bool HasOverlapForUser(Appointment appt, DateTime start, DateTime end, int? ignoreId = null)
        {
            return _appointments.Any(a =>
                a.Status == "booked" &&
                a.UserCode == appt.UserCode &&                 // ίδιος χρήστης μόνο
                (ignoreId == null || a.Id != ignoreId.Value) &&
                !(a.End <= start || a.Start >= end));          // κλασικός overlap έλεγχος
        }

        public Appointment Insert(Appointment a)
        {
            // default τιμές όπου χρειάζεται
            if (string.IsNullOrWhiteSpace(a.Status))
                a.Status = "booked";
            if (string.IsNullOrWhiteSpace(a.UserCode))
                a.UserCode = "A";

            // έλεγχος σύγκρουσης μόνο για ίδιο χρήστη
            if (HasOverlapForUser(a, a.Start, a.End))
                throw new InvalidOperationException("Υπάρχει ήδη ραντεβού σε αυτή την ώρα για τον ίδιο χρήστη.");

            a.Id = NextId();
            _appointments.Add(a);
            Save();
            return a;
        }

        public void Delete(int id)
        {
            var a = _appointments.FirstOrDefault(x => x.Id == id);
            if (a == null) return;

            _appointments.Remove(a);
            Save();
        }

        /// <summary>
        /// Αλλαγή ώρας ραντεβού, με έλεγχο collision ανά UserCode.
        /// </summary>
        public void UpdateTime(int id, DateTime newStart, DateTime newEnd, bool clearEarlier = true)
        {
            var a = _appointments.FirstOrDefault(x => x.Id == id);
            if (a == null) return;

            // προσωρινά αλλάζουμε για να ελέγξουμε
            var oldStart = a.Start;
            var oldEnd = a.End;

            a.Start = newStart;
            a.End = newEnd;

            if (HasOverlapForUser(a, newStart, newEnd, id))
            {
                // επαναφορά αν υπάρχει σύγκρουση
                a.Start = oldStart;
                a.End = oldEnd;
                throw new InvalidOperationException("Σύγκρουση με άλλο ραντεβού για τον ίδιο χρήστη.");
            }

            if (clearEarlier)
            {
                a.WantsEarlier = false;
                a.PreferredBefore = null;
            }

            Save();
        }

        public List<Appointment> GetBetween(DateTime from, DateTime to)
        {
            return _appointments
                .Where(a => a.Status == "booked" &&
                            a.Start >= from && a.Start < to)
                .OrderBy(a => a.Start)
                .ToList();
        }

        public List<Appointment> GetForCustomer(int customerId)
        {
            return _appointments
                .Where(a => a.Status == "booked" &&
                            a.CustomerId == customerId)
                .OrderByDescending(a => a.Start)
                .ToList();
        }

        public List<Appointment> GetAll()
        {
            return _appointments
                .Where(a => a.Status == "booked")
                .OrderBy(a => a.Start)
                .ToList();
        }

        /// <summary>
        /// Υποψήφιοι για μεταφορά σε κενό slot από λίστα αναμονής.
        /// (Όπως πριν, δεν φιλτράρουμε ανά UserCode εδώ, ώστε το κενό να μπορεί να δοθεί σε οποιονδήποτε.)
        /// </summary>
        public List<Appointment> GetEarlierCandidates(DateTime freeStart)
        {
            return _appointments
                .Where(a =>
                    a.Status == "booked" &&
                    a.WantsEarlier &&
                    a.Start > freeStart &&
                    (!a.PreferredBefore.HasValue ||
                     freeStart.TimeOfDay <= a.PreferredBefore.Value))
                .OrderBy(a => a.Start)
                .ToList();
        }
    }
}
