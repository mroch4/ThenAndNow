using Microsoft.AspNetCore.Components;
using System.Globalization;
using ThenAndNow.Constants;
using ThenAndNow.Enums;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Configuration;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Components
{
    public partial class Card
    {
        #region Parameters

        [Parameter]
        public Entry Entry { get; set; }

        [Parameter]
        public bool ShowDetails { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private AppConfiguration AppConfiguration { get; set; }

        [Inject]
        private IEntryRepository EntryRepository { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IRatingService RatingService { get; set; }

        #endregion

        #region Blazor Overrides

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            OriginalPhoto = AppConfiguration.DefaultOriginalPhoto;

            if (ShowDetails)
            {
                await GetDetails();
            }

            Loaded = true;
        }

        #endregion

        #region Private Properties

        private bool Loaded { get; set; }
        private string DirectUrl => $"{AppConfiguration.BaseUrl}{Routes.Entry}?{Routes.EntryIdQueryParamName}={Entry.Id}";
        private bool OriginalPhoto { get; set; }

        private Rating Rating { get; set; }
        private string RatingDesc { get; set; }
        private bool? RatingEnabled { get; set; }

        #endregion

        #region Private Methods

        private async Task GetDetails()
        {
            Rating ??= await RatingService.GetRatingById(Entry.Id);
            RatingDesc ??= GetRatingDesc();
            RatingEnabled ??= await GetRatingEnabled();

            if (Entry.Description == null)
            {
                var details = await EntryRepository.GetDetailsById(Entry.Id);
                Entry.Description = details.Description ?? string.Empty;
                Entry.Tags = details.Tags ?? [];
            }
        }

        private string GetFigcaption()
        {
            return OriginalPhoto
                ? Entry.Timestamp.Then
                : Entry.Timestamp.Now.ToString("Y", CultureInfo.GetCultureInfo("pl-PL"));
            //: Entry.Timestamp.Now.ToString("f", CultureInfo.GetCultureInfo("pl-PL"));
        }

        private string GetImagePath(ImageSize imageSize)
        {
            return $"photos/{imageSize.ToString().ToLower()}/{Entry.Id}{(OriginalPhoto ? "a" : "b")}.webp";
        }

        private string GetLocation()
        {
            return $"?q=" +
                   $"{Entry.Coordinates.Latitude.ToString("N6", CultureInfo.GetCultureInfo("en-US"))}+" +
                   $"{Entry.Coordinates.Longitude.ToString("N6", CultureInfo.GetCultureInfo("en-US"))}";
        }

        private async Task<bool> GetRatingEnabled()
        {
            return await RatingService.RatingEnabled(Entry.Id);
        }

        private string GetRatingDesc()
        {
            var s = $"{Labels.Score}{Rating.Score} ({Rating.Total} ";
            var lastMember = Rating.Score switch
            {
                1 => Labels.Vote,
                > 1 and < 5 => Labels.Votes234,
                _ => Labels.Votes56789
            };

            return s + $"{lastMember})";
        }

        private async Task ThumbsDown()
        {
            Rating = await RatingService.ThumbsDown(Entry.Id);
            RatingDesc = GetRatingDesc();
            RatingEnabled = await GetRatingEnabled();
        }

        private async Task ThumbsUp()
        {
            Rating = await RatingService.ThumbsUp(Entry.Id);
            RatingDesc = GetRatingDesc();
            RatingEnabled = await GetRatingEnabled();
        }

        private async Task ToggleDetails()
        {
            await GetDetails();
            ShowDetails = !ShowDetails;
        }

        #endregion
    }
}
