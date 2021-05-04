// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders
{
    /// <summary>
    ///     <para>
    ///         Instances of this class are returned from methods when using the <see cref="ModelBuilder" /> API
    ///         and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    public class TemporalTableBuilder
    {
        private readonly IMutableEntityType _entityType;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        [EntityFrameworkInternal]
        public TemporalTableBuilder(IMutableEntityType entityType)
        {
            _entityType = entityType;
        }

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public virtual TemporalTableBuilder WithHistoryTable(string? name = null, string? schema = null)
        {
            if (name != null)
            {
                _entityType.SetTemporalHistoryTableName(name);
            }

            if (schema != null)
            {
                _entityType.SetTemporalHistoryTableSchema(schema);
            }

            return this;
        }

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public virtual TemporalPeriodPropertyBuilder HasPeriodStart(string propertyName)
        {
            if (_entityType.FindProperty(propertyName) != null)
            {
                throw new InvalidOperationException($"Entity '{_entityType.ShortName()}' can't use the property '{propertyName}' as part of the period because property with this name already exists.");
            }

            var periodProperty = _entityType.AddProperty(propertyName, typeof(DateTime));
            _entityType.SetTemporalPeriodStartPropertyName(propertyName);

            return new TemporalPeriodPropertyBuilder(_entityType, periodProperty, periodStart: true);
        }

        /// <summary>
        ///     TODO: add comments
        /// </summary>
        public virtual TemporalPeriodPropertyBuilder HasPeriodEnd(string propertyName)
        {
            if (_entityType.FindProperty(propertyName) != null)
            {
                throw new InvalidOperationException($"Entity '{_entityType.ShortName()}' can't use the property '{propertyName}' as part of the period because property with this name already exists.");
            }

            var periodProperty = _entityType.AddProperty(propertyName, typeof(DateTime));
            _entityType.SetTemporalPeriodEndPropertyName(propertyName);

            return new TemporalPeriodPropertyBuilder(_entityType, periodProperty, periodStart: false);
        }

        #region Hidden System.Object members

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns> A string that represents the current object. </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string? ToString()
            => base.ToString();

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns> <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />. </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj)
            => base.Equals(obj);

        /// <summary>
        ///     Serves as the default hash function.
        /// </summary>
        /// <returns> A hash code for the current object. </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
            => base.GetHashCode();

        #endregion
    }
}
