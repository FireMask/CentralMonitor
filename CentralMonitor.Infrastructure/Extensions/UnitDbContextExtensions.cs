using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public static class UnitDbContextExtensions
{
    public static async Task<List<T>> ExecuteStoredProcedureAsync<T>(this DbContext context, string storedProcedure, Dictionary<string, object>? parameters = null, Func<DbDataReader, T>? map = null)
    {
        var connection = context.Database.GetDbConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = storedProcedure;
        command.CommandType = CommandType.StoredProcedure;

        if (parameters != null)
        {
            foreach (var kvp in parameters)
            {
                var dbParameter = command.CreateParameter();
                dbParameter.ParameterName = kvp.Key;
                dbParameter.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(dbParameter);
            }
        }

        using var reader = await command.ExecuteReaderAsync();

        var result = new List<T>();

        while (await reader.ReadAsync())
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map), "Mapping function cannot be null.");

            result.Add(map(reader));
        }

        await connection.CloseAsync();
        return result;
   }
}