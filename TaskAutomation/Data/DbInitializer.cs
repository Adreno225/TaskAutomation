using ExcelLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAutomationDB.Context;
using TaskAutomationDB.Entities;

namespace TaskAutomation.Data
{
    public class DbInitializer
    {
        const int NumSheetClasses = 1;
        const int NumSheetStages = 2;
        const int NumSheetModes = 3;
        const int NumSheetCustomers = 4;
        const int NumSheetTypesCO = 5;
        private const string MessageDeleteDB = "Удаление существующей БД выполнено за {0} мс";
        private const string MessageMigrationDB = "Миграция БД выполнено за {0} с";
        private const string MessageInitializationDB = "Инициализация БД выполнено за {0} с";
        private readonly TaskAutomationContext _db;
        private readonly ILogger<DbInitializer> _logger;
        private readonly string _pathInitializator = Environment.CurrentDirectory + @"\Data\Initializator.xlsx";

        private readonly Class[] _classes = default;
        private readonly Stage[] _stages = default;
        private readonly Mode[] _modes = default;
        private readonly Customer[] _customers = default;
        private readonly TypeCO[] _typesCO = default;

        public DbInitializer(TaskAutomationContext db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");
            _logger.LogInformation("Удаление существующей БД...");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation(MessageDeleteDB, timer.ElapsedMilliseconds);
            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation(MessageMigrationDB, timer.ElapsedMilliseconds);
            if (await _db.Classes.AnyAsync()) return;
            using (var excel = new Package(_pathInitializator))
            {
                await WriteColumn(excel,NumSheetClasses,_classes);
                await WriteColumn(excel, NumSheetStages, _stages);
                await WriteColumn(excel, NumSheetModes, _modes);
                await WriteColumn(excel, NumSheetCustomers, _customers);
                await WriteColumn(excel, NumSheetTypesCO, _typesCO);
            }
            _logger.LogInformation(MessageInitializationDB, timer.Elapsed.TotalSeconds);
        }

        private async Task WriteColumn<T>(Package package, int numSheet, T[] items) where T : NamedEntity, new()
        {
            var sheet = package.SelectSheet(numSheet);
            var countRows = sheet.CountRows;
            items = new T[countRows - 1];
            for (int i = 2 ; i <= countRows; i++)
                items[i-2] = new T { Name = sheet.GetCell(i, 1).GetValue() };
            await _db.AddRangeAsync(items);
            await _db.SaveChangesAsync();
        }
    }
}
