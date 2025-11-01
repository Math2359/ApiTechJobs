using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils;

/// <summary>
/// Atributo para ignorar a propriedade na hora do insert
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class IgnoreInsertAttribute : Attribute { }

public static class SqlHelper
{
    /// <summary>
    /// Gera uma string de consulta SQL INSERT para o tipo especificado, excluindo as propriedades marcadas com o atributo <see cref="IgnoreInsertAttribute"/>.
    /// </summary>
    /// <remarks>
    /// O método constrói a consulta incluindo todas as propriedades públicas de instância do tipo <typeparamref name="T"/>,
    /// exceto aquelas marcadas com o atributo <see cref="IgnoreInsertAttribute"/> e a coluna de saída especificada.
    /// </remarks>
    /// <typeparam name="T">O tipo que representa a tabela para a qual a consulta INSERT será gerada.</typeparam>
    /// <param name="outputColumn">O nome da coluna que será retornada após a operação de inserção. O valor padrão é "Id".</param>
    /// <returns>Uma string contendo a consulta SQL INSERT para o tipo especificado <typeparamref name="T"/>, com a coluna de saída indicada.</returns>

    public static string GerarInsertQuery<T>(string outputColumn = "Id")
    {
        Type type = typeof(T);

        var tableName = type.Name;

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<IgnoreInsertAttribute>() == null)
            .Where(p => !string.Equals(p.Name, outputColumn, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        var columns = string.Join(", ", properties.Select(p => p.Name));
        var parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

        string query = $@"INSERT INTO {tableName} ({columns})
                          OUTPUT INSERTED.{outputColumn}
                          VALUES ({parameters});";

        return query;
    }
}
