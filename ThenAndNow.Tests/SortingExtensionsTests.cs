using ThenAndNow.Enums;
using ThenAndNow.Helpers;

namespace ThenAndNow.Tests
{
    [TestClass]
    public class SortingExtensionsTests
    {
        #region ToSortBy Test Cases

        [DataTestMethod]
        [DataRow(Sorting.IdDescending, SortBy.Id)]
        [DataRow(Sorting.IdAscending, SortBy.Id)]
        [DataRow(Sorting.TitleDescending, SortBy.Title)]
        [DataRow(Sorting.TitleAscending, SortBy.Title)]
        [DataRow(Sorting.DateNowDescending, SortBy.DateNow)]
        [DataRow(Sorting.DateNowAscending, SortBy.DateNow)]
        public void ToSortBy_AllSortingValues_ReturnsCorrectSortBy(Sorting sorting, SortBy expected)
        {
            // Act
            var result = sorting.ToSortBy();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region ToSortDirection Test Cases

        [DataTestMethod]
        [DataRow(Sorting.IdAscending, SortDirection.Asc)]
        [DataRow(Sorting.TitleAscending, SortDirection.Asc)]
        [DataRow(Sorting.DateNowAscending, SortDirection.Asc)]
        public void ToSortDirection_WithAscendingSorting_ReturnsAsc(Sorting sorting, SortDirection expected)
        {
            // Act
            var result = sorting.ToSortDirection();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(Sorting.IdDescending, SortDirection.Desc)]
        [DataRow(Sorting.TitleDescending, SortDirection.Desc)]
        [DataRow(Sorting.DateNowDescending, SortDirection.Desc)]
        public void ToSortDirection_WithDescendingSorting_ReturnsDesc(Sorting sorting, SortDirection expected)
        {
            // Act
            var result = sorting.ToSortDirection();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(Sorting.IdDescending, SortDirection.Desc)]
        [DataRow(Sorting.IdAscending, SortDirection.Asc)]
        [DataRow(Sorting.TitleDescending, SortDirection.Desc)]
        [DataRow(Sorting.TitleAscending, SortDirection.Asc)]
        [DataRow(Sorting.DateNowDescending, SortDirection.Desc)]
        [DataRow(Sorting.DateNowAscending, SortDirection.Asc)]
        public void ToSortDirection_AllSortingValues_ReturnsCorrectDirection(Sorting sorting, SortDirection expected)
        {
            // Act
            var result = sorting.ToSortDirection();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region ToSorting Test Cases

        [DataTestMethod]
        [DataRow("datenow", "desc", Sorting.DateNowDescending)]
        [DataRow("datenow", "asc", Sorting.DateNowAscending)]
        public void ToSorting_WithDateNowParameters_ReturnsCorrectSorting(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("title", "desc", Sorting.TitleDescending)]
        [DataRow("title", "asc", Sorting.TitleAscending)]
        public void ToSorting_WithTitleParameters_ReturnsCorrectSorting(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("id", "asc", Sorting.IdAscending)]
        [DataRow("id", "desc", Sorting.IdDescending)]
        public void ToSorting_WithIdParameters_ReturnsCorrectSorting(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("invalid", "desc", Sorting.IdDescending)] // Default fallback
        [DataRow("invalid", "asc", Sorting.IdDescending)]  // Default fallback
        [DataRow("", "desc", Sorting.IdDescending)]        // Default fallback
        [DataRow("", "asc", Sorting.IdDescending)]         // Default fallback
        [DataRow(null, "desc", Sorting.IdDescending)]      // Default fallback
        [DataRow(null, "asc", Sorting.IdDescending)]       // Default fallback
        public void ToSorting_WithInvalidSortBy_ReturnsDefault(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("datenow", "invalid", Sorting.IdDescending)] // Invalid direction falls through
        [DataRow("datenow", "", Sorting.IdDescending)]        // Empty direction falls through
        [DataRow("datenow", null, Sorting.IdDescending)]      // Null direction falls through
        [DataRow("title", "invalid", Sorting.IdDescending)]   // Invalid direction falls through
        [DataRow("title", "", Sorting.IdDescending)]          // Empty direction falls through
        [DataRow("title", null, Sorting.IdDescending)]        // Null direction falls through
        [DataRow("id", "invalid", Sorting.IdDescending)]      // Invalid direction falls through
        [DataRow("id", "", Sorting.IdDescending)]             // Empty direction falls through
        [DataRow("id", null, Sorting.IdDescending)]           // Null direction falls through
        public void ToSorting_WithInvalidSortDirection_ReturnsDefault(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("datenow", "desc", Sorting.DateNowDescending)]
        [DataRow("datenow", "asc", Sorting.DateNowAscending)]
        [DataRow("title", "desc", Sorting.TitleDescending)]
        [DataRow("title", "asc", Sorting.TitleAscending)]
        [DataRow("id", "asc", Sorting.IdAscending)]
        public void ToSorting_AllValidCombinations_ReturnsCorrectSorting(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("id", "desc", Sorting.IdDescending)]           // Explicit id desc case
        [DataRow("unknown", "anything", Sorting.IdDescending)]  // Unknown sortBy
        [DataRow("", "", Sorting.IdDescending)]                 // Empty strings
        [DataRow(null, null, Sorting.IdDescending)]             // Null values
        [DataRow("invalid", "desc", Sorting.IdDescending)]      // Invalid sortBy with valid direction
        [DataRow("invalid", "asc", Sorting.IdDescending)]       // Invalid sortBy with valid direction
        [DataRow("id", "invalid", Sorting.IdDescending)]        // Valid sortBy with invalid direction
        [DataRow("title", "invalid", Sorting.IdDescending)]     // Valid sortBy with invalid direction
        [DataRow("datenow", "invalid", Sorting.IdDescending)]   // Valid sortBy with invalid direction
        [DataRow("", "desc", Sorting.IdDescending)]             // Empty sortBy
        [DataRow("", "asc", Sorting.IdDescending)]              // Empty sortBy
        [DataRow("id", "", Sorting.IdDescending)]               // Valid sortBy with empty direction
        [DataRow("title", "", Sorting.IdDescending)]            // Valid sortBy with empty direction
        [DataRow("datenow", "", Sorting.IdDescending)]          // Valid sortBy with empty direction
        [DataRow(null, "desc", Sorting.IdDescending)]           // Null sortBy
        [DataRow(null, "asc", Sorting.IdDescending)]            // Null sortBy
        [DataRow("id", null, Sorting.IdDescending)]             // Valid sortBy with null direction
        [DataRow("title", null, Sorting.IdDescending)]          // Valid sortBy with null direction
        [DataRow("datenow", null, Sorting.IdDescending)]        // Valid sortBy with null direction
        [DataRow("random", "random", Sorting.IdDescending)]     // Both invalid
        [DataRow("123", "456", Sorting.IdDescending)]           // Numeric strings
        [DataRow("  ", "  ", Sorting.IdDescending)]             // Whitespace strings
        public void ToSorting_DefaultCases_ReturnsIdDescending(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result, $"Failed for sortBy: '{sortBy}', sortDirection: '{sortDirection}'");
        }

        [DataTestMethod]
        [DataRow("DATENOW", "DESC", Sorting.DateNowDescending)] // Uppercase
        [DataRow("DateNow", "Desc", Sorting.DateNowDescending)] // Mixed case
        [DataRow("datenow", "desc", Sorting.DateNowDescending)] // Lowercase
        [DataRow("TITLE", "ASC", Sorting.TitleAscending)]       // Uppercase
        [DataRow("Title", "Asc", Sorting.TitleAscending)]       // Mixed case
        [DataRow("title", "asc", Sorting.TitleAscending)]       // Lowercase
        [DataRow("ID", "ASC", Sorting.IdAscending)]             // Uppercase
        [DataRow("Id", "Asc", Sorting.IdAscending)]             // Mixed case
        [DataRow("id", "asc", Sorting.IdAscending)]             // Lowercase
        public void ToSorting_CaseInsensitive_ReturnsCorrectSorting(string sortBy, string sortDirection, Sorting expected)
        {
            // Act
            var result = SortingExtensions.ToSorting(sortBy, sortDirection);

            // Assert
            Assert.AreEqual(expected, result, $"Method should be case-insensitive for: {sortBy}, {sortDirection}");
        }

        #endregion

        #region Integration Test Cases

        [DataTestMethod]
        [DataRow(Sorting.IdDescending, SortBy.Id, SortDirection.Desc)]
        [DataRow(Sorting.IdAscending, SortBy.Id, SortDirection.Asc)]
        [DataRow(Sorting.TitleDescending, SortBy.Title, SortDirection.Desc)]
        [DataRow(Sorting.TitleAscending, SortBy.Title, SortDirection.Asc)]
        [DataRow(Sorting.DateNowDescending, SortBy.DateNow, SortDirection.Desc)]
        [DataRow(Sorting.DateNowAscending, SortBy.DateNow, SortDirection.Asc)]
        public void SortingExtensions_IntegrationTest_SortingToSortByAndDirection(Sorting sorting, SortBy expectedSortBy, SortDirection expectedSortDirection)
        {
            // Act
            var sortBy = sorting.ToSortBy();
            var sortDirection = sorting.ToSortDirection();

            // Assert
            Assert.AreEqual(expectedSortBy, sortBy, $"SortBy mismatch for {sorting}");
            Assert.AreEqual(expectedSortDirection, sortDirection, $"SortDirection mismatch for {sorting}");
        }

        [DataTestMethod]
        [DataRow("datenow", "desc", SortBy.DateNow, SortDirection.Desc)]
        [DataRow("datenow", "asc", SortBy.DateNow, SortDirection.Asc)]
        [DataRow("title", "desc", SortBy.Title, SortDirection.Desc)]
        [DataRow("title", "asc", SortBy.Title, SortDirection.Asc)]
        [DataRow("id", "asc", SortBy.Id, SortDirection.Asc)]
        [DataRow("id", "desc", SortBy.Id, SortDirection.Desc)]
        [DataRow("invalid", "anything", SortBy.Id, SortDirection.Desc)] // Default case
        public void SortingExtensions_IntegrationTest_StringsToSortingToComponents(string sortByStr, string sortDirStr, SortBy expectedSortBy, SortDirection expectedSortDir)
        {
            // Test round-trip: strings -> Sorting -> SortBy/SortDirection
            // Act
            var sorting = SortingExtensions.ToSorting(sortByStr, sortDirStr);
            var actualSortBy = sorting.ToSortBy();
            var actualSortDir = sorting.ToSortDirection();

            // Assert
            Assert.AreEqual(expectedSortBy, actualSortBy, $"SortBy failed for: {sortByStr}, {sortDirStr}");
            Assert.AreEqual(expectedSortDir, actualSortDir, $"SortDirection failed for: {sortByStr}, {sortDirStr}");
        }

        [TestMethod]
        public void SortingExtensions_IntegrationTest_AllEnumValuesCovered()
        {
            // Ensure every Sorting enum value has been tested
            var allSortingValues = Enum.GetValues<Sorting>();
            var expectedMappings = new Dictionary<Sorting, (SortBy, SortDirection)>
            {
                [Sorting.IdDescending] = (SortBy.Id, SortDirection.Desc),
                [Sorting.IdAscending] = (SortBy.Id, SortDirection.Asc),
                [Sorting.TitleDescending] = (SortBy.Title, SortDirection.Desc),
                [Sorting.TitleAscending] = (SortBy.Title, SortDirection.Asc),
                [Sorting.DateNowDescending] = (SortBy.DateNow, SortDirection.Desc),
                [Sorting.DateNowAscending] = (SortBy.DateNow, SortDirection.Asc)
            };

            // Verify we have mappings for all enum values
            Assert.AreEqual(allSortingValues.Length, expectedMappings.Count, "All Sorting enum values should be mapped");

            foreach (var sorting in allSortingValues)
            {
                // Act
                var sortBy = sorting.ToSortBy();
                var sortDirection = sorting.ToSortDirection();

                // Assert
                Assert.IsTrue(expectedMappings.ContainsKey(sorting), $"Missing mapping for {sorting}");
                var (expectedSortBy, expectedSortDirection) = expectedMappings[sorting];
                Assert.AreEqual(expectedSortBy, sortBy, $"SortBy mismatch for {sorting}");
                Assert.AreEqual(expectedSortDirection, sortDirection, $"SortDirection mismatch for {sorting}");
            }
        }

        [TestMethod]
        public void SortingExtensions_IntegrationTest_ConsistencyCheck()
        {
            // Test that ToSorting method can produce all Sorting enum values
            var producibleSortings = new HashSet<Sorting>();

            // Test all valid string combinations
            var validCombinations = new[]
            {
                ("datenow", "desc"),
                ("datenow", "asc"),
                ("title", "desc"),
                ("title", "asc"),
                ("id", "asc"),
                ("anything", "else") // Default case
            };

            foreach (var (sortBy, sortDirection) in validCombinations)
            {
                var sorting = SortingExtensions.ToSorting(sortBy, sortDirection);
                producibleSortings.Add(sorting);
            }

            // Verify all enum values can be produced
            var allEnumValues = Enum.GetValues<Sorting>().ToHashSet();
            Assert.IsTrue(allEnumValues.IsSubsetOf(producibleSortings), "All Sorting enum values should be producible from string combinations");
        }

        #endregion
    }
}