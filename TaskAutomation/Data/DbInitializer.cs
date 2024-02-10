using ExcelLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskAutomationDB.Context;
using TaskAutomationDB.Entities;

namespace TaskAutomation.Data;

public class DbInitializer
{
    const int NumSheetClasses = 1;
    const int NumSheetStages = 2;
    const int NumSheetModes = 3;
    const int NumSheetCustomers = 4;
    const int NumSheetTypesCO = 5;
    const int NumSheetFunctions = 6;
    const int NumSheetObjects = 7;
    const int NumSheetParameter = 8;
    const int DefaultColumn = 1;
    const int ColumnObject = 2;
    const int ColumnIdParameter = 3;
    const int ColumnParameter = 4;
    const int ColumnClasses = 5;
    const int StartColumnFunctions = 7;



    private const string MessageDeleteDB = "Удаление существующей БД выполнено за {0} мс";
    private const string MessageMigrationDB = "Миграция БД выполнено за {0} с";
    private const string MessageInitializationDB = "Инициализация БД выполнено за {0} с";
    private readonly TaskAutomationContext _db;
    private readonly ILogger<DbInitializer> _logger;
    private readonly string _pathInitializator = Environment.CurrentDirectory + @"\Data\Initializator.xlsx";

    public DbInitializer(TaskAutomationContext db, ILogger<DbInitializer> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        //var timer = Stopwatch.StartNew();
        //_logger.LogInformation("Инициализация БД...");
        //_logger.LogInformation("Удаление существующей БД...");
        //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        //_logger.LogInformation(MessageDeleteDB, timer.ElapsedMilliseconds);
        //_logger.LogInformation("Миграция БД...");
        //await _db.Database.MigrateAsync().ConfigureAwait(false);
        //_logger.LogInformation(MessageMigrationDB, timer.ElapsedMilliseconds);
        if (await _db.Classes.AnyAsync()) return;
        using (var excel = new Package(_pathInitializator))
        {
            await WriteColumn<Class>(excel,NumSheetClasses, DefaultValue);
            await WriteColumn<Stage>(excel, NumSheetStages, DefaultValue);
            await WriteColumn<Mode>(excel, NumSheetModes, DefaultValue);
            await WriteColumn<Customer>(excel, NumSheetCustomers, DefaultValue);
            await WriteColumn<TypeCO>(excel, NumSheetTypesCO, DefaultValue);
            await WriteColumn<FunctionParameter>(excel, NumSheetFunctions, DefaultValue);
            await WriteColumn<ObjectAutomation>(excel, NumSheetObjects, FunctionObj);
            await WriteColumnParameter(excel);
            await WriteDataMainTableParameter(excel);
        }
        //_logger.LogInformation(MessageInitializationDB, timer.Elapsed.TotalSeconds);
    }

    private async Task WriteColumn<T>(Package package, int numSheet, Func <Sheet,int, string> func) where T : NamedEntity, new()
    {
        var sheet = package.SelectSheet(numSheet);
        var countRows = sheet.CountRows;
        var items = new T[countRows - 1];
        for (int i = 2; i <= countRows; i++)
            items[i - 2] = new T { Name = func(sheet, i) };
        await _db.AddRangeAsync(items);
        await _db.SaveChangesAsync();
    }

    private async Task WriteDataMainTableParameter(Package package)
    {
        var sheet = package.SelectSheet(NumSheetParameter);
        var countRows = sheet.CountRows;
        var items = new List<ParameterClassFunction>();
        var idFunctions = new List<int>();
        for (int i = 2; i <= countRows; i++)
        {
            var parameter = _db.Parameters.Find(int.Parse(sheet.GetCell(i, ColumnIdParameter).GetValue()));
            var idClasses = sheet.GetCell(i, ColumnClasses).GetValue().Split(',',' ', options:StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)+1);
            for (int j = 0; j <= 10; j++)
                if (sheet.GetCell(i, StartColumnFunctions + j).GetValue() == "1")
                    idFunctions.Add(j + 1);
            foreach (var idClass in idClasses)
            {
                foreach (var idFunction in idFunctions)
                    items.Add(new ParameterClassFunction
                    {
                        Parameter = parameter,
                        Class = _db.Classes.Find(idClass),
                        FunctionParameter = _db.FunctionsParameters.Find(idFunction)
                    });
            }
            idFunctions.Clear();
        }
        await _db.AddRangeAsync(items.ToArray());
        await _db.SaveChangesAsync();
    }




    private async Task WriteColumnParameter(Package package) 
    {
        var sheet = package.SelectSheet(NumSheetParameter);
        var countRows = sheet.CountRows;
        var items = new List<Parameter>();
        var prevID = "";
        for (int i = 2; i <= countRows; i++)
        {
            var value = sheet.GetCell(i, ColumnIdParameter).GetValue();
            if (value != prevID)
            {
                items.Add(new Parameter
                {
                    Name = ParameterValue(sheet, i),
                    ObjectAutomation = _db.ObjectsAutomation.Find(int.Parse(sheet.GetCell(i, ColumnObject).GetValue()))
                });
            }
            prevID = value;
        }
        await _db.AddRangeAsync(items.ToArray());
        await _db.SaveChangesAsync();
    }


    private static string FunctionObj(Sheet sheet, int row)
    {
        var value = sheet.GetCell(row, 4).GetValue();
        return value.ToLower() != "нет" ? value : sheet.GetCell(row, 3).GetValue();
    }

    private static string DefaultValue(Sheet sheet, int row) => GetValue(sheet, row, DefaultColumn);

    private static string ParameterValue(Sheet sheet, int row) => GetValue(sheet, row, ColumnParameter);
    private static string GetValue(Sheet sheet, int row, int column) => sheet.GetCell(row, column).GetValue();
}