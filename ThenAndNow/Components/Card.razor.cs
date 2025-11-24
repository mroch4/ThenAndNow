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

        [Parameter]
        public bool OriginalPhotoFirst { get; set; }

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

            OriginalPhoto = OriginalPhotoFirst;

            if (ShowDetails)
            {
                await GetDetails();
            }

            DataLoaded = true;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            OriginalPhoto = OriginalPhotoFirst;
        }

        #endregion

        #region Private Properties

        private bool DataLoaded { get; set; }
        private bool DetailsLoaded { get; set; }
        private string DirectUrl => $"{AppConfiguration.BaseUrl}{Routes.Entry}?{Routes.EntryIdQueryParamName}={Entry.Id}";
        private bool OriginalPhoto { get; set; }

        private Rating Rating { get; set; }
        private string RatingDesc { get; set; }
        private bool? VotingEnabled { get; set; }

        #endregion

        #region Private Methods

        private async Task GetDetails()
        {
            if (!DetailsLoaded)
            {
                Rating ??= await RatingService.GetRatingById(Entry.Id);
                RatingDesc ??= GetRatingDesc();
                VotingEnabled ??= await RatingService.GetVotingEnabled(Entry.Id);

                var details = await EntryRepository.GetDetailsById(Entry.Id);
                Entry.Description = details.Description ?? string.Empty;
                Entry.Tags = details.Tags ?? [];

                DetailsLoaded = true;
            }
        }

        private string GetFigcaption()
        {
            return OriginalPhoto
                ? Entry.Timestamp.Then
                : Entry.Timestamp.Now.ToString("Y", CultureInfo.GetCultureInfo(Constants.Constants.CultureInfo));
        }

        private string GetImagePath(ImageSize imageSize, bool isOriginal = false)
        {
            var suffix = isOriginal ? "a" : "b";
            return $"photos/{imageSize.ToString().ToLower()}/{Entry.Id}{suffix}.webp";
        }

        private string GetLocation()
        {
            return $"?q=" +
                   $"{Entry.Coordinates.Latitude.ToString("N6", CultureInfo.GetCultureInfo("en-US"))}+" +
                   $"{Entry.Coordinates.Longitude.ToString("N6", CultureInfo.GetCultureInfo("en-US"))}";
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
            VotingEnabled = await RatingService.GetVotingEnabled(Entry.Id);
        }

        private async Task ThumbsUp()
        {
            Rating = await RatingService.ThumbsUp(Entry.Id);
            RatingDesc = GetRatingDesc();
            VotingEnabled = await RatingService.GetVotingEnabled(Entry.Id);
        }

        private async Task ToggleDetails()
        {
            if (!ShowDetails && !DetailsLoaded)
            {
                await GetDetails();
            }
            ShowDetails = !ShowDetails;
        }

        #endregion
    }
}