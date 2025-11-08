using System;

namespace PelatologioApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Notes { get; set; } = "";
        /// -1 = κακός, 0 = ουδέτερος, 1 = καλός
        public int Rating { get; set; } = 0;

        public override string ToString()
            => $"{FullName} ({Phone})";
    }
}
