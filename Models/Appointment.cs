using System;

namespace PelatologioApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = "";
        public string Notes { get; set; } = "";

        // "booked", "cancelled" κλπ, αλλά δουλεύουμε κυρίως με booked
        public string Status { get; set; } = "booked";

        // Λίστα αναμονής: αν ο πελάτης θέλει νωρίτερα
        public bool WantsEarlier { get; set; }

        // Αν συμπληρωθεί, σημαίνει "θέλω πριν από αυτήν την ώρα"
        public TimeSpan? PreferredBefore { get; set; }

        // ✅ ΝΕΟ: Κωδικός χρήστη (A ή B)
        public string UserCode { get; set; } = "A";
    }
}
