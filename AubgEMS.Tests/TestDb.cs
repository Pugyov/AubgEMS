using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;                // needs Microsoft.Data.Sqlite
using Microsoft.EntityFrameworkCore;
using AubgEMS.Infrastructure.Data;

namespace AubgEMS.Tests
{
    /// <summary> In-memory relational DB for fast, isolated tests. </summary>
    public sealed class TestDb : IDisposable
    {
        private readonly DbConnection _conn;
        public ApplicationDbContext Db { get; }

        public TestDb()
        {
            _conn = new SqliteConnection("DataSource=:memory:");
            _conn.Open();

            var opts = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_conn)           // relational provider (FKs, uniques)
                .Options;

            Db = new ApplicationDbContext(opts);
            Db.Database.EnsureCreated();    // builds schema + applies HasData (in TEST DB only)
        }

        public void Dispose()
        {
            Db.Dispose();
            _conn.Dispose();
        }
    }
}