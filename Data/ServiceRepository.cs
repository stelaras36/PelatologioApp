using System.Collections.Generic;
using PelatologioApp.Models;

namespace PelatologioApp.Data
{
    public static class ServiceRepository
    {
        // Δείγμα υπηρεσιών: Id, Name, Duration (λεπτά)
        public static IEnumerable<Service> GetAll()
        {
            return new List<Service>
    {
        new Service { Id = 1, Name = "Μανικιούρ", Duration = 60 },
        new Service { Id = 2, Name = "Μανικιούρ ημιμόνιμο", Duration = 60 },
        new Service { Id = 3, Name = "Πεντικιούρ", Duration = 60 },
        new Service { Id = 4, Name = "Πεντικιούρ με βλάβες", Duration = 60 },
        new Service { Id = 5, Name = "Ονυχοκρύπτωση", Duration = 60 },
        new Service { Id = 6, Name = "Κάλοι", Duration = 60 },
        new Service { Id = 7, Name = "Σκληρύνσεις", Duration = 60 },
        new Service { Id = 8, Name = "Οστρακοειδή νύχια", Duration = 60 },
        new Service { Id = 9, Name = "Τεχνική ορθονυχίας", Duration = 60 },
    };
        }

    }
}
