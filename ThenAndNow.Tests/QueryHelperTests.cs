using ThenAndNow.Enums;
using ThenAndNow.Helpers;
using ThenAndNow.Models;

namespace ThenAndNow.Tests.Helpers
{
    [TestClass]
    public class QueryHelperTests
    {
        #region GetCurrentPage Test Cases

        [DataTestMethod]
        [DataRow("-3", 0)]
        [DataRow("0", 0)]
        [DataRow("3.14", 0)]
        [DataRow("5", 5)]
        [DataRow("10", 10)]
        [DataRow("123abc", 0)]
        [DataRow("abc123", 0)]
        [DataRow("invalid", 0)]
        [DataRow(null, 0)]
        [DataRow("", 0)]
        [DataRow(" ", 0)]
        public void GetCurrentPage_VariousInputs_ReturnsExpectedResults(string input, int expected)
        {
            // Act
            var result = input.GetCurrentPage();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region GetPageSize Test Cases

        [DataTestMethod]
        [DataRow("-5", 8)]      // DefaultPageSize
        [DataRow("0", 0)]
        [DataRow("3.14", 8)]    // DefaultPageSize
        [DataRow("5", 5)]
        [DataRow("10", 10)]
        [DataRow("123abc", 8)]  // DefaultPageSize
        [DataRow("abc123", 8)]  // DefaultPageSize
        [DataRow("invalid", 8)] // DefaultPageSize
        [DataRow(null, 8)]      // DefaultPageSize
        [DataRow("", 8)]        // DefaultPageSize
        [DataRow(" ", 8)]       // DefaultPageSize
        public void GetPageSize_VariousInputs_ReturnsExpectedResults(string input, int expected)
        {
            // Act
            var result = input.GetPageSize();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region ToQueryString Edge Cases

        [TestMethod]
        public void ToQueryString_DefaultQueryParams_ReturnsRootOnly()
        {
            // Arrange
            var queryParams = new QueryParams();

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", result);
        }

        [DataTestMethod]
        [DataRow(1, "/?page=1")]
        [DataRow(5, "/?page=5")]
        [DataRow(100, "/?page=100")]
        public void ToQueryString_WithCurrentPage_ReturnsCorrectQueryString(int currentPage, string expected)
        {
            // Arrange
            var queryParams = new QueryParams { CurrentPage = currentPage };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(-10)]
        [DataRow(-1)]
        [DataRow(0)]
        public void ToQueryString_WithNonPositiveCurrentPage_DoesNotIncludePage(int currentPage)
        {
            // Arrange
            var queryParams = new QueryParams { CurrentPage = currentPage };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", result);
        }

        [DataTestMethod]
        [DataRow("test", "/?tag=test")]
        [DataRow("street", "/?tag=street")]
        [DataRow("2000", "/?tag=2000")]
        public void ToQueryString_WithTag_ReturnsCorrectQueryString(string tag, string expected)
        {
            // Arrange
            var queryParams = new QueryParams { Tag = tag };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ToQueryString_WithEmptyOrNullTag_DoesNotIncludeTag(string tag)
        {
            // Arrange
            var queryParams = new QueryParams { Tag = tag };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", result);
        }

        [DataTestMethod]
        [DataRow(1, "/?pageSize=1")]
        [DataRow(12, "/?pageSize=12")]
        [DataRow(16, "/?pageSize=16")]
        [DataRow(50, "/?pageSize=50")]
        public void ToQueryString_WithNonDefaultPageSize_ReturnsCorrectQueryString(int pageSize, string expected)
        {
            // Arrange
            var queryParams = new QueryParams { PageSize = pageSize };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(-5)] // Negative
        [DataRow(0)]  // Zero
        [DataRow(8)]  // Default page size
        public void ToQueryString_WithDefaultZeroOrNegativePageSize_DoesNotIncludePageSize(int pageSize)
        {
            // Arrange
            var queryParams = new QueryParams { PageSize = pageSize };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", result);
        }

        [DataTestMethod]
        [DataRow(Sorting.IdAscending, "/?sortBy=id&sorting=asc")]
        [DataRow(Sorting.TitleDescending, "/?sortBy=title&sorting=desc")]
        [DataRow(Sorting.TitleAscending, "/?sortBy=title&sorting=asc")]
        [DataRow(Sorting.DateNowDescending, "/?sortBy=datenow&sorting=desc")]
        [DataRow(Sorting.DateNowAscending, "/?sortBy=datenow&sorting=asc")]
        public void ToQueryString_WithNonDefaultSorting_ReturnsCorrectQueryString(Sorting sorting, string expected)
        {
            // Arrange
            var queryParams = new QueryParams { Sorting = sorting };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToQueryString_WithDefaultSorting_DoesNotIncludeSorting()
        {
            // Arrange
            var queryParams = new QueryParams { Sorting = Sorting.IdDescending };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", result);
        }

        [DataTestMethod]
        [DataRow(3, "street", 12, Sorting.DateNowDescending, "/?page=3&tag=street&pageSize=12&sortBy=datenow&sorting=desc")]
        [DataRow(1, "architecture", 16, Sorting.IdAscending, "/?page=1&tag=architecture&pageSize=16&sortBy=id&sorting=asc")]
        [DataRow(10, "2024", 25, Sorting.TitleDescending, "/?page=10&tag=2024&pageSize=25&sortBy=title&sorting=desc")]
        [DataRow(2, "nature-photos", 50, Sorting.DateNowAscending, "/?page=2&tag=nature-photos&pageSize=50&sortBy=datenow&sorting=asc")]
        [DataRow(100, "buildings", 1, Sorting.TitleAscending, "/?page=100&tag=buildings&pageSize=1&sortBy=title&sorting=asc")]
        public void ToQueryString_WithAllParametersVariations_ReturnsCompleteQueryString(int currentPage, string tag, int pageSize, Sorting sorting, string expected)
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                Tag = tag,
                PageSize = pageSize,
                Sorting = sorting
            };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(-1, null, 8, Sorting.IdDescending, "/")] // All excluded
        [DataRow(0, "", -5, Sorting.IdDescending, "/")] // All excluded
        [DataRow(5, null, 8, Sorting.IdDescending, "/?page=5")] // Only page included
        [DataRow(0, "valid-tag", 0, Sorting.IdDescending, "/?tag=valid-tag")] // Only tag included
        [DataRow(-10, "test", 16, Sorting.IdDescending, "/?tag=test&pageSize=16")] // Tag and pageSize included
        [DataRow(2, "", 8, Sorting.TitleAscending, "/?page=2&sortBy=title&sorting=asc")] // Page and sorting included
        [DataRow(5, "architecture", 8, Sorting.IdDescending, "/?page=5&tag=architecture")] // Page and tag included
        [DataRow(0, "test", 8, Sorting.TitleAscending, "/?tag=test&sortBy=title&sorting=asc")] // Tag and sorting included
        public void ToQueryString_WithMixedValidAndInvalidParametersVariations_ReturnsOnlyValidParameters(int currentPage, string tag, int pageSize, Sorting sorting, string expected)
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                Tag = tag,
                PageSize = pageSize,
                Sorting = sorting
            };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToQueryString_WithOnlyInvalidParameters_ReturnsRootOnly()
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = 0,                    // Invalid (not > 0)
                Tag = "",                           // Invalid (empty)
                PageSize = 8,                       // Invalid (default)
                Sorting = Sorting.IdDescending      // Invalid (default)
            };

            // Act
            var result = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", result);
        }

        #endregion

        #region Integration

        [TestMethod]
        public void QueryHelper_IntegrationTest_StringToQueryParamsToString()
        {
            // Arrange
            var originalPageString = "5";
            var originalPageSizeString = "16";
            var originalTag = "street";
            var originalSorting = Sorting.TitleAscending;

            // Act - Parse strings using helper methods
            var currentPage = originalPageString.GetCurrentPage();
            var pageSize = originalPageSizeString.GetPageSize();

            // Create QueryParams
            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Tag = originalTag,
                Sorting = originalSorting
            };

            // Convert back to query string
            var queryString = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(5, currentPage);
            Assert.AreEqual(16, pageSize);
            Assert.AreEqual("/?page=5&tag=street&pageSize=16&sortBy=title&sorting=asc", queryString);
        }

        [DataTestMethod]
        [DataRow("1", "10", "test", Sorting.IdAscending, 1, 10, "/?page=1&tag=test&pageSize=10&sortBy=id&sorting=asc")]
        [DataRow("0", "8", "architecture", Sorting.IdDescending, 0, 8, "/?tag=architecture")]
        [DataRow("invalid", "invalid", "", Sorting.DateNowDescending, 0, 8, "/?sortBy=datenow&sorting=desc")]
        [DataRow("10", "25", null, Sorting.TitleAscending, 10, 25, "/?page=10&pageSize=25&sortBy=title&sorting=asc")]
        public void QueryHelper_IntegrationTestVariations_StringToQueryParamsToString(string pageString, string pageSizeString, string tag, Sorting sorting, int expectedPage, int expectedPageSize, string expectedQueryString)
        {
            // Act - Parse strings using helper methods
            var currentPage = pageString.GetCurrentPage();
            var pageSize = pageSizeString.GetPageSize();

            // Create QueryParams
            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Tag = tag,
                Sorting = sorting
            };

            // Convert back to query string
            var queryString = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(expectedPage, currentPage);
            Assert.AreEqual(expectedPageSize, pageSize);
            Assert.AreEqual(expectedQueryString, queryString);
        }

        [TestMethod]
        public void QueryHelper_IntegrationTest_DefaultValuesRoundTrip()
        {
            // Arrange - Test with default constructor
            var originalQueryParams = new QueryParams();

            // Act - Convert to string and simulate parsing back
            var queryString = originalQueryParams.ToQueryString();
            var parsedPage = "0".GetCurrentPage(); // Simulating no page parameter
            var parsedPageSize = "".GetPageSize(); // Simulating no pageSize parameter

            var newQueryParams = new QueryParams
            {
                CurrentPage = parsedPage,
                PageSize = parsedPageSize,
                Tag = null,
                Sorting = Sorting.IdDescending
            };

            var finalQueryString = newQueryParams.ToQueryString();

            // Assert
            Assert.AreEqual("/", queryString);
            Assert.AreEqual("/", finalQueryString);
            Assert.AreEqual(0, parsedPage);
            Assert.AreEqual(8, parsedPageSize); // Default page size
        }

        [TestMethod]
        public void QueryHelper_IntegrationTest_EdgeCaseInputs()
        {
            // Arrange - Test with edge case inputs
            var pageString = "999999";
            var pageSizeString = "1";
            var tag = "tag-with-numbers-123";
            var sorting = Sorting.DateNowAscending;

            // Act
            var currentPage = pageString.GetCurrentPage();
            var pageSize = pageSizeString.GetPageSize();

            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Tag = tag,
                Sorting = sorting
            };

            var queryString = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(999999, currentPage);
            Assert.AreEqual(1, pageSize);
            Assert.AreEqual("/?page=999999&tag=tag-with-numbers-123&pageSize=1&sortBy=datenow&sorting=asc", queryString);
        }

        [TestMethod]
        public void QueryHelper_IntegrationTest_InvalidInputsWithFallbacks()
        {
            // Arrange - Test with various invalid inputs
            var pageString = "not-a-number";
            var pageSizeString = "3.14159";
            var tag = "valid-tag";
            var sorting = Sorting.TitleDescending;

            // Act
            var currentPage = pageString.GetCurrentPage();
            var pageSize = pageSizeString.GetPageSize();

            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Tag = tag,
                Sorting = sorting
            };

            var queryString = queryParams.ToQueryString();

            // Assert
            Assert.AreEqual(0, currentPage); // Fallback for invalid input
            Assert.AreEqual(8, pageSize); // Fallback to default for invalid input
            Assert.AreEqual("/?tag=valid-tag&sortBy=title&sorting=desc", queryString); // Only valid params included
        }

        #endregion
    }
}