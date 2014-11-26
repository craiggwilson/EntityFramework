// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Relational.FunctionalTests;
using Xunit;

namespace Microsoft.Data.Entity.Sqlite.FunctionalTests
{
    public class SqLiteIncludeTest : IncludeTestBase<SqLiteNorthwindQueryFixture>
    {
        public override void Include_collection()
        {
            base.Include_collection();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_order_by_key()
        {
            base.Include_collection_order_by_key();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_order_by_non_key()
        {
            base.Include_collection_order_by_non_key();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""City"", ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""City"", ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""City"", ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_as_no_tracking()
        {
            base.Include_collection_as_no_tracking();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_principal_already_tracked()
        {
            base.Include_collection_principal_already_tracked();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
LIMIT @p1

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""
LIMIT @p1

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
    LIMIT @p1
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_principal_already_tracked_as_no_tracking()
        {
            base.Include_collection_principal_already_tracked_as_no_tracking();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
LIMIT @p1

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""
LIMIT @p1

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
    LIMIT @p1
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_with_filter()
        {
            base.Include_collection_with_filter();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_with_filter_reordered()
        {
            base.Include_collection_with_filter_reordered();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_when_projection()
        {
            base.Include_collection_when_projection();

            Assert.Equal(
                @"SELECT ""c"".""CustomerID""
FROM ""Customers"" AS ""c""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_on_join_clause_with_filter()
        {
            base.Include_collection_on_join_clause_with_filter();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
    WHERE ""c"".""CustomerID"" = @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_on_additional_from_clause_with_filter()
        {
            base.Include_collection_on_additional_from_clause_with_filter();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c1""
CROSS JOIN ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c1""
    CROSS JOIN ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_on_additional_from_clause()
        {
            base.Include_collection_on_additional_from_clause();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_duplicate_collection()
        {
            base.Include_duplicate_collection();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0 OFFSET @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    LIMIT @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""t0"".*
    FROM (
        SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
        FROM ""Customers"" AS ""c""
        ORDER BY ""c"".""CustomerID""
        LIMIT @p0 OFFSET @p0
    ) AS ""t0""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0 OFFSET @p0",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_duplicate_collection_result_operator()
        {
            base.Include_duplicate_collection_result_operator();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0 OFFSET @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    LIMIT @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""t0"".*
    FROM (
        SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
        FROM ""Customers"" AS ""c""
        ORDER BY ""c"".""CustomerID""
        LIMIT @p0 OFFSET @p0
    ) AS ""t0""
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_on_join_clause_with_order_by_and_filter()
        {
            base.Include_collection_on_join_clause_with_order_by_and_filter();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""City"", ""c"".""CustomerID""

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""City"", ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
    WHERE ""c"".""CustomerID"" = @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""City"", ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_on_additional_from_clause2()
        {
            base.Include_collection_on_additional_from_clause2();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0

SELECT 1
FROM ""Customers"" AS ""c""

SELECT 1
FROM ""Customers"" AS ""c""

SELECT 1
FROM ""Customers"" AS ""c""

SELECT 1
FROM ""Customers"" AS ""c""

SELECT 1
FROM ""Customers"" AS ""c""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_duplicate_collection_result_operator2()
        {
            base.Include_duplicate_collection_result_operator2();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0 OFFSET @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    LIMIT @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_reference()
        {
            base.Include_reference();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_duplicate_reference()
        {
            base.Include_duplicate_reference();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""o"".""CustomerID""
LIMIT @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""o"".""CustomerID""
LIMIT @p0 OFFSET @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""o"".""CustomerID""
LIMIT @p0 OFFSET @p0",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_duplicate_reference2()
        {
            base.Include_duplicate_reference2();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""o"".""OrderID""
LIMIT @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
ORDER BY ""o"".""OrderID""
LIMIT @p0 OFFSET @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
ORDER BY ""o"".""OrderID""
LIMIT @p0 OFFSET @p0",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_duplicate_reference3()
        {
            base.Include_duplicate_reference3();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
ORDER BY ""o"".""OrderID""
LIMIT @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""o"".""OrderID""
LIMIT @p0 OFFSET @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""o"".""OrderID""
LIMIT @p0 OFFSET @p0",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_reference_when_projection()
        {
            base.Include_reference_when_projection();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID""
FROM ""Orders"" AS ""o""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_reference_with_filter_reordered()
        {
            base.Include_reference_with_filter_reordered();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
WHERE ""o"".""CustomerID"" = @p0",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_reference_with_filter()
        {
            base.Include_reference_with_filter();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
WHERE ""o"".""CustomerID"" = @p0",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_dependent_already_tracked_as_no_tracking()
        {
            base.Include_collection_dependent_already_tracked_as_no_tracking();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
WHERE ""o"".""CustomerID"" = @p0

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""
LIMIT @p1

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
    LIMIT @p1
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_dependent_already_tracked()
        {
            base.Include_collection_dependent_already_tracked();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
WHERE ""o"".""CustomerID"" = @p0

SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
WHERE ""c"".""CustomerID"" = @p0
ORDER BY ""c"".""CustomerID""
LIMIT @p1

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    WHERE ""c"".""CustomerID"" = @p0
    LIMIT @p1
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_reference_dependent_already_tracked()
        {
            base.Include_reference_dependent_already_tracked();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
WHERE ""o"".""CustomerID"" = @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_reference_as_no_tracking()
        {
            base.Include_reference_as_no_tracking();

            Assert.Equal(
                @"SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Orders"" AS ""o""
LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public override void Include_collection_as_no_tracking2()
        {
            base.Include_collection_as_no_tracking2();

            Assert.Equal(
                @"SELECT ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""CustomerID"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
FROM ""Customers"" AS ""c""
ORDER BY ""c"".""CustomerID""
LIMIT @p0

SELECT ""o"".""CustomerID"", ""o"".""OrderDate"", ""o"".""OrderID""
FROM ""Orders"" AS ""o""
INNER JOIN (
    SELECT DISTINCT ""c"".""CustomerID""
    FROM ""Customers"" AS ""c""
    LIMIT @p0
) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
ORDER BY ""c"".""CustomerID""",
                TestSqlLoggerFactory.Sql);
        }

        public SqLiteIncludeTest(SqLiteNorthwindQueryFixture fixture)
            : base(fixture)
        {
        }
    }
}
