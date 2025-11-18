using ThenAndNow.Enums;
using ThenAndNow.Helpers;
using ThenAndNow.Models;

namespace ThenAndNow.Tests
{
    [TestClass]
    public class RequestHelperTests
    {
        #region GetDatabaseQuery Test Cases

        [TestMethod]
        public void GetDatabaseQuery_DefaultQueryParams_ReturnsExpectedRequest()
        {
            // Arrange
            var queryParams = new QueryParams();

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual(null, result.Tag);
            Assert.AreEqual(0, result.Skip);
            Assert.AreEqual(8, result.Take); // Default page size
            Assert.AreEqual(SortBy.Id, result.SortBy);
            Assert.AreEqual(SortDirection.Desc, result.SortDirection);
        }

        [DataTestMethod]
        [DataRow(0, 8, 0)]      // Page 0, default size, skip 0
        [DataRow(1, 8, 0)]      // Page 1, default size, skip 0
        [DataRow(2, 8, 8)]      // Page 2, default size, skip 8
        [DataRow(3, 8, 16)]     // Page 3, default size, skip 16
        [DataRow(10, 8, 72)]    // Page 10, default size, skip 72
        public void GetDatabaseQuery_WithCurrentPage_CalculatesCorrectSkip(int currentPage, int expectedTake, int expectedSkip)
        {
            // Arrange
            var queryParams = new QueryParams { CurrentPage = currentPage };

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual(expectedSkip, result.Skip);
            Assert.AreEqual(expectedTake, result.Take);
        }

        [DataTestMethod]
        [DataRow(0, 8)]     // Zero page size uses default
        [DataRow(5, 5)]     // Custom page size
        [DataRow(16, 16)]   // Another custom page size
        [DataRow(50, 50)]   // Large page size
        [DataRow(-5, -5)]   // Negative page size (passed through)
        public void GetDatabaseQuery_WithPageSize_SetsCorrectTake(int pageSize, int expectedTake)
        {
            // Arrange
            var queryParams = new QueryParams { PageSize = pageSize };

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual(expectedTake, result.Take);
        }

        [DataTestMethod]
        [DataRow("test-tag")]
        [DataRow("architecture")]
        [DataRow("2024")]
        [DataRow("")]
        [DataRow(null)]
        public void GetDatabaseQuery_WithTag_SetsCorrectTag(string tag)
        {
            // Arrange
            var queryParams = new QueryParams { Tag = tag };

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual(tag, result.Tag);
        }

        [DataTestMethod]
        [DataRow(Sorting.IdDescending, SortBy.Id, SortDirection.Desc)]
        [DataRow(Sorting.IdAscending, SortBy.Id, SortDirection.Asc)]
        [DataRow(Sorting.TitleDescending, SortBy.Title, SortDirection.Desc)]
        [DataRow(Sorting.TitleAscending, SortBy.Title, SortDirection.Asc)]
        [DataRow(Sorting.DateNowDescending, SortBy.DateNow, SortDirection.Desc)]
        [DataRow(Sorting.DateNowAscending, SortBy.DateNow, SortDirection.Asc)]
        public void GetDatabaseQuery_WithSorting_SetsCorrectSortingProperties(Sorting sorting, SortBy expectedSortBy, SortDirection expectedSortDirection)
        {
            // Arrange
            var queryParams = new QueryParams { Sorting = sorting };

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual(expectedSortBy, result.SortBy);
            Assert.AreEqual(expectedSortDirection, result.SortDirection);
        }

        [TestMethod]
        public void GetDatabaseQuery_WithAllParameters_ReturnsCompleteRequest()
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = 5,
                PageSize = 12,
                Tag = "architecture",
                Sorting = Sorting.TitleAscending
            };

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual("architecture", result.Tag);
            Assert.AreEqual(48, result.Skip); // (5-1) * 12
            Assert.AreEqual(12, result.Take);
            Assert.AreEqual(SortBy.Title, result.SortBy);
            Assert.AreEqual(SortDirection.Asc, result.SortDirection);
        }

        [DataTestMethod]
        [DataRow(2, 16, 16)] // Page 2, size 16, skip 16
        [DataRow(3, 10, 20)] // Page 3, size 10, skip 20
        [DataRow(1, 25, 0)]  // Page 1, size 25, skip 0
        [DataRow(10, 5, 45)] // Page 10, size 5, skip 45
        public void GetDatabaseQuery_SkipCalculation_IsCorrectForPageAndSize(int currentPage, int pageSize, int expectedSkip)
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            // Act
            var result = RequestHelper.GetDatabaseQuery(queryParams);

            // Assert
            Assert.AreEqual(expectedSkip, result.Skip);
            Assert.AreEqual(pageSize, result.Take);
        }

        #endregion

        #region GetPagesCount Test Cases

        [DataTestMethod]
        [DataRow(0, 10, 0)]     // No items
        [DataRow(-1, 10, 0)]    // Negative items count
        [DataRow(1, 10, 1)]     // Single item
        [DataRow(9, 10, 1)]     // Less than page size
        [DataRow(10, 10, 1)]    // Exactly page size
        [DataRow(11, 10, 2)]    // One more than page size
        [DataRow(25, 10, 3)]    // Multiple pages
        [DataRow(100, 10, 10)]  // Exact multiple
        [DataRow(101, 10, 11)]  // One more than exact multiple
        public void GetPagesCount_VariousInputs_ReturnsCorrectPageCount(int itemsCount, int pageSize, int expectedPages)
        {
            // Act
            var result = itemsCount.GetPagesCount(pageSize);

            // Assert
            Assert.AreEqual(expectedPages, result);
        }

        [DataTestMethod]
        [DataRow(8, 8, 1)]    // Default page size
        [DataRow(16, 8, 2)]   // Double default
        [DataRow(25, 8, 4)]   // 25 items with default page size (ceil(25/8) = 4)
        [DataRow(64, 8, 8)]   // Exact multiple of default
        [DataRow(65, 8, 9)]   // One more than exact multiple
        public void GetPagesCount_WithDefaultPageSize_ReturnsCorrectCount(int itemsCount, int pageSize, int expectedPages)
        {
            // Act
            var result = itemsCount.GetPagesCount(pageSize);

            // Assert
            Assert.AreEqual(expectedPages, result);
        }

        [DataTestMethod]
        [DataRow(1, 1, 1)]     // Single item, single page size
        [DataRow(5, 1, 5)]     // Multiple items, single page size
        [DataRow(10, 1, 10)]   // Each item gets its own page
        public void GetPagesCount_WithSinglePageSize_ReturnsItemCount(int itemsCount, int pageSize, int expectedPages)
        {
            // Act
            var result = itemsCount.GetPagesCount(pageSize);

            // Assert
            Assert.AreEqual(expectedPages, result);
        }

        [TestMethod]
        public void GetPagesCount_WithLargeNumbers_HandlesCorrectly()
        {
            // Arrange
            var itemsCount = 1000000;
            var pageSize = 50;
            var expectedPages = 20000; // 1,000,000 / 50

            // Act
            var result = itemsCount.GetPagesCount(pageSize);

            // Assert
            Assert.AreEqual(expectedPages, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void GetPagesCount_WithZeroPageSize_ThrowsException()
        {
            // Arrange
            var itemsCount = 10;
            var pageSize = 0;

            // Act
            itemsCount.GetPagesCount(pageSize);

            // Assert - Exception expected
        }

        #endregion

        #region Integration Test Cases

        [TestMethod]
        public void RequestHelper_IntegrationTest_QueryParamsToRequestToPageCount()
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = 3,
                PageSize = 10,
                Tag = "test",
                Sorting = Sorting.TitleAscending
            };
            var totalItems = 45;

            // Act
            var request = RequestHelper.GetDatabaseQuery(queryParams);
            var pageCount = totalItems.GetPagesCount(queryParams.PageSize);

            // Assert
            Assert.AreEqual("test", request.Tag);
            Assert.AreEqual(20, request.Skip); // (3-1) * 10
            Assert.AreEqual(10, request.Take);
            Assert.AreEqual(SortBy.Title, request.SortBy);
            Assert.AreEqual(SortDirection.Asc, request.SortDirection);
            Assert.AreEqual(5, pageCount); // ceil(45/10) = 5
        }

        [DataTestMethod]
        [DataRow(1, 8, 25, 0, 4)]       // First page, 25 items, 4 total pages
        [DataRow(2, 8, 25, 8, 4)]       // Second page, skip 8
        [DataRow(4, 8, 25, 24, 4)]      // Last page, skip 24
        [DataRow(1, 10, 100, 0, 10)]    // Perfect division
        [DataRow(5, 20, 150, 80, 8)]    // Page 5 of 8
        public void RequestHelper_IntegrationTestVariations_CompleteWorkflow(int currentPage, int pageSize, int totalItems, int expectedSkip, int expectedPageCount)
        {
            // Arrange
            var queryParams = new QueryParams
            {
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            // Act
            var request = RequestHelper.GetDatabaseQuery(queryParams);
            var pageCount = totalItems.GetPagesCount(pageSize);

            // Assert
            Assert.AreEqual(expectedSkip, request.Skip);
            Assert.AreEqual(pageSize, request.Take);
            Assert.AreEqual(expectedPageCount, pageCount);
        }

        [TestMethod]
        public void RequestHelper_IntegrationTest_DefaultsWorkflow()
        {
            // Arrange
            var queryParams = new QueryParams(); // All defaults
            var totalItems = 50;

            // Act
            var request = RequestHelper.GetDatabaseQuery(queryParams);
            var pageCount = totalItems.GetPagesCount(request.Take);

            // Assert
            Assert.AreEqual(null, request.Tag);
            Assert.AreEqual(0, request.Skip);
            Assert.AreEqual(8, request.Take); // Default page size
            Assert.AreEqual(SortBy.Id, request.SortBy);
            Assert.AreEqual(SortDirection.Desc, request.SortDirection);
            Assert.AreEqual(7, pageCount); // ceil(50/8) = 7
        }

        [TestMethod]
        public void RequestHelper_IntegrationTest_EdgeCasePageSizes()
        {
            // Test with zero page size (should use default)
            var queryParams = new QueryParams { PageSize = 0, CurrentPage = 2 };
            var request = RequestHelper.GetDatabaseQuery(queryParams);

            Assert.AreEqual(8, request.Take); // Default page size used
            Assert.AreEqual(8, request.Skip); // (2-1) * 8

            // Test with very large page size
            queryParams = new QueryParams { PageSize = 1000, CurrentPage = 1 };
            request = RequestHelper.GetDatabaseQuery(queryParams);

            Assert.AreEqual(1000, request.Take);
            Assert.AreEqual(0, request.Skip);
        }

        [TestMethod]
        public void RequestHelper_IntegrationTest_NegativeAndEdgeCasePages()
        {
            // Test with negative current page
            var queryParams = new QueryParams { CurrentPage = -5, PageSize = 10 };
            var request = RequestHelper.GetDatabaseQuery(queryParams);

            Assert.AreEqual(0, request.Skip); // Negative pages should result in 0 skip

            // Test with zero current page
            queryParams = new QueryParams { CurrentPage = 0, PageSize = 10 };
            request = RequestHelper.GetDatabaseQuery(queryParams);

            Assert.AreEqual(0, request.Skip); // Zero page should result in 0 skip
        }

        #endregion
    }
}