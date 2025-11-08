using System.Collections.Generic;
using System.Linq;
using PelatologioApp.Models;

namespace PelatologioApp.Data
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = FileStorage.LoadCustomers();
        }

        private void Save() => FileStorage.SaveCustomers(_customers);

        private int NextId() => _customers.Count == 0
            ? 1
            : _customers.Max(c => c.Id) + 1;

        public List<Customer> GetAll() =>
            _customers.OrderBy(c => c.FullName).ToList();

        public Customer? GetById(int id) =>
            _customers.FirstOrDefault(c => c.Id == id);

        public List<Customer> FindByLast4(string last4)
        {
            if (string.IsNullOrWhiteSpace(last4))
                return new List<Customer>();

            last4 = last4.Trim();
            return _customers
                .Where(c => !string.IsNullOrEmpty(c.Phone) &&
                            c.Phone.EndsWith(last4))
                .OrderBy(c => c.FullName)
                .ToList();
        }

        public Customer? FindByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            phone = phone.Trim();
            return _customers.FirstOrDefault(c => c.Phone == phone);
        }

        public Customer Upsert(Customer c)
        {
            if (c.Id == 0)
            {
                c.Id = NextId();
                _customers.Add(c);
            }
            else
            {
                var ex = _customers.FirstOrDefault(x => x.Id == c.Id);
                if (ex == null)
                    _customers.Add(c);
                else
                {
                    ex.FullName = c.FullName;
                    ex.Phone = c.Phone;
                    ex.Notes = c.Notes;
                    ex.Rating = c.Rating;
                }
            }

            Save();
            return c;
        }

        public void IncrementTotalAppointments(int customerId)
        {
            var c = _customers.FirstOrDefault(x => x.Id == customerId);
            if (c == null) return;
            // Αν θες μετρητή, πρόσθεσέ τον στο μοντέλο.
            Save();
        }

        public string GetRatingLabel(int rating) =>
            rating switch
            {
                > 0 => "καλός",
                < 0 => "κακός",
                _ => "ουδέτερος"
            };
    }
}
