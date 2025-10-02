using System.Collections.Generic;
using AubgEMS.Infrastructure.Data.Models;

namespace AubgEMS.Infrastructure.Data.SeedDb
{
    /// <summary>
    /// Centralized seed objects (LOOKUPS ONLY).
    /// Clubs/Events/News are seeded in their configurations.
    /// </summary>
    public class SeedData
    {
        public IReadOnlyList<Location> Locations { get; }
        public IReadOnlyList<EventType> EventTypes { get; }
        public IReadOnlyList<Department> Departments { get; }

        public SeedData()
        {
            Locations = new List<Location>
            {
                new Location { Id = 1,  Name = "BAC Auditorium", Capacity = 100 },
                new Location { Id = 2,  Name = "BAC Room 001",   Capacity = 36  },
                new Location { Id = 3,  Name = "BAC Room 002",   Capacity = 35  },
                new Location { Id = 4,  Name = "BAC Room 003",   Capacity = 33  },
                new Location { Id = 5,  Name = "BAC Room 004",   Capacity = 39  },
                new Location { Id = 6,  Name = "BAC Room 101",   Capacity = 24  },
                new Location { Id = 7,  Name = "BAC Room 103",   Capacity = 26  },
                new Location { Id = 8,  Name = "BAC Room 201",   Capacity = 25  },
                new Location { Id = 9,  Name = "BAC Room 202",   Capacity = 39  },
                new Location { Id = 10, Name = "BAC Room 203",   Capacity = 41  },
                new Location { Id = 11, Name = "BAC Room 204",   Capacity = 33  },
                new Location { Id = 12, Name = "BAC Room 206",   Capacity = 24  },
                new Location { Id = 13, Name = "BAC Room 207",   Capacity = 14  },
                new Location { Id = 14, Name = "Panitza Library", Capacity = 50 },
                new Location { Id = 15, Name = "Dr. Carl Djerassi Theater Hall ", Capacity = 300 },
                new Location { Id = 16, Name = "Sports Hall",    Capacity = 200 },
                new Location { Id = 17, Name = "Aspire",         Capacity = 30  },
                new Location { Id = 18, Name = "Ground Floor Lobby", Capacity = 50 },
                new Location { Id = 19, Name = "6306 - Board Room",   Capacity = 30 },
                new Location { Id = 20, Name = "6307 - Postbank Room", Capacity = 30 },
                new Location { Id = 21, Name = "6307 - Restaurant",   Capacity = 40 },
                new Location { Id = 22, Name = "6307 - Café",     Capacity = 20 },
                new Location { Id = 23, Name = "MB Auditorium",   Capacity = 108 },
                new Location { Id = 24, Name = "Skaptopara I MPR", Capacity = 40 },
                new Location { Id = 25, Name = "Skaptopara II MPR", Capacity = 40 },
                new Location { Id = 26, Name = "Skaptopara III MPR", Capacity = 40 },
                new Location { Id = 27, Name = "Outdoors",        Capacity = 40 }
            };

            EventTypes = new List<EventType>
            {
                new EventType { Id = 1,  Name = "Workshop" },
                new EventType { Id = 2,  Name = "Seminar" },
                new EventType { Id = 3,  Name = "Lecture" },
                new EventType { Id = 4,  Name = "Meeting" },
                new EventType { Id = 5,  Name = "Challenge" },
                new EventType { Id = 6,  Name = "Sports" },
                new EventType { Id = 7,  Name = "Theatre Performance" },
                new EventType { Id = 8,  Name = "Dance Performance" },
                new EventType { Id = 9,  Name = "Movie Screening" },
                new EventType { Id = 10, Name = "Conference" },
                new EventType { Id = 11, Name = "Video Shooting" },
                new EventType { Id = 12, Name = "Concert" }
            };

            Departments = new List<Department>
            {
                new Department { Id = 1,  Name = "Business Department" },
                new Department { Id = 2,  Name = "Computer Science Department" },
                new Department { Id = 3,  Name = "Economics Department" },
                new Department { Id = 4,  Name = "History and Civilizations Department" },
                new Department { Id = 5,  Name = "Journalism, Media, and Communication Department" },
                new Department { Id = 6,  Name = "Literature and Theater Department" },
                new Department { Id = 7,  Name = "Mathematics and Science Department" },
                new Department { Id = 8,  Name = "Modern Languages and Arts Department" },
                new Department { Id = 9,  Name = "Philosophy and Psychology Department" },
                new Department { Id = 10, Name = "Politics and European Studies Department" },
                new Department { Id = 11, Name = "University-wide (Independent Clubs)" }
            };
        }
    }
}
