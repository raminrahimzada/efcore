// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server specific extension methods for <see cref="TableBuilder" />.
    /// </summary>
    public static class SqlServerTableBuilderExtensions
    {
        private const string DefaultHistoryTableNameSuffix = "History";

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public static TableBuilder IsTemporal(
            this TableBuilder tableBuilder,
            Action<TemporalTableBuilder> buildAction)
        {
            SetDefaultTemporalAnnotations(tableBuilder.EntityType);
            buildAction(new TemporalTableBuilder(tableBuilder.EntityType));

            return tableBuilder;
        }

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public static TableBuilder IsTemporal(
            this TableBuilder tableBuilder)
        {
            SetDefaultTemporalAnnotations(tableBuilder.EntityType);

            return tableBuilder;
        }

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public static TableBuilder<TEntity> IsTemporal<TEntity>(
            this TableBuilder<TEntity> tableBuilder)
            where TEntity : class
        {
            SetDefaultTemporalAnnotations(tableBuilder.EntityType);

            return tableBuilder;
        }

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public static TableBuilder<TEntity> IsTemporal<TEntity>(
            this TableBuilder<TEntity> tableBuilder,
            Action<TemporalTableBuilder<TEntity>> buildAction)
            where TEntity: class
        {
            SetDefaultTemporalAnnotations(tableBuilder.EntityType);
            buildAction(new TemporalTableBuilder<TEntity>(tableBuilder.EntityType));

            return tableBuilder;
        }

        private static void SetDefaultTemporalAnnotations(IMutableEntityType entityType)
        {
            entityType.SetIsTemporal(true);
            entityType.SetTemporalHistoryTableName(entityType.ShortName() + DefaultHistoryTableNameSuffix);
        }
    }
}
