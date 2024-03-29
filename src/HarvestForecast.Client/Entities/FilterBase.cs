﻿using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarvestForecast.Client.Entities;

/// <summary>
///     Abstract base for classes which contain filtering options that should be convertible to a query string.
/// </summary>
public abstract record FilterBase
{
    internal string GetFilterQuery()
    {
        var collection = HttpUtility.ParseQueryString( string.Empty );

        foreach ( var pair in GetActiveFilters() )
        {
            collection.Add( pair.Key, pair.Value );
        }

        return collection.ToString() ?? string.Empty;
    }

    /// <summary>
    ///     Gets the values of the filters in an implemented class.
    /// </summary>
    internal abstract IEnumerable<KeyValuePair<string, string?>> GetFilters();

    /// <summary>
    ///     Gets the values of the active filters which should be included in a query string.
    /// </summary>
    internal IEnumerable<KeyValuePair<string, string>> GetActiveFilters()
    {
        return GetFilters().Where( f => f.Value is not null )!;
    }
}
