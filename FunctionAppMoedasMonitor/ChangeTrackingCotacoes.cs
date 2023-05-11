using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using FunctionAppMoedasMonitor.Models;

namespace FunctionAppMoedasMonitor;

public static class ChangeTrackingCotacoes
{
    [Function(nameof(ChangeTrackingCotacoes))]   
    public static void Run(
        [SqlTrigger(tableName: "dbo.Cotacoes", connectionStringSetting: "BaseMoedas")]
        IReadOnlyList<SqlChange<HistoricoCotacao>> changes,
        FunctionContext context)

    {
        var logger = context.GetLogger(nameof(ChangeTrackingCotacoes));
        foreach (SqlChange<HistoricoCotacao> change in changes)
        {
            logger.LogInformation($"Operação: {change.Operation} | " +
                $"Dados ({change.Item.GetType().FullName}): {JsonSerializer.Serialize(change.Item)}");
        }
    }
}