namespace ThenAndNow.Constants
{
    public static class Labels
    {
        #region HeaderComponent

        public const string ThenAndNow = "dawniej & dziś";

        #endregion

        #region Navbar

        public const string About = "O blogu";
        public const string Map = "Mapa";
        public const string Random = "Losuj";
        public const string Tags = "Tagi";

        #endregion

        #region FooterComponent

        public const string Author = "Marcin Rochowski";

        #endregion

        #region AboutPage

        public const string Content = "Cześć, nazywam się Marcin Rochowski i witam na moim blogu! Strona powstała na skutek fuzji kilku moich hobby: pasji do fotografii, zawodowego zamiłowania do webdevelopmentu oraz zainteresowania historią. Wszystkie współcześne zdjęcia są mojego autorstwa, zdjęcia z przeszłości pochodzą zaś z ogólnodostępnych zasobów (spójrz poniżej). Jeśli masz jakieś niesamowite zdjęcia Poznania wykonane przed laty i chcesz się nimi podzielić - koniecznie daj mi znać :)";
        public const string Contact = "Kontakt ze mną:";
        public const string Cyryl = "CYRYL - Wirtualne Muzeum Historii Poznania";
        public const string Email = "rochowski.marcin(at)gmail.com";
        public const string Flickr = "Flickr";
        public const string Fotopolska = "FOTOPOLSKA - Baza zdjęć przedstawiających polskie miasta";
        public const string Github = "Github";
        public const string Sources = "Zasoby zdjęć historycznych:";

        #endregion

        #region BannerComponent

        public const string BannerHeading = "Pierwszy raz na blogu?";
        public const string BannerQuestion = "Oto kilka wskazówek, które ułatwią Ci nawigowanie po stronie:";
        public const string BannerLine1 = "Kliknij na zdjęcie, aby zobaczyć, jak na przestrzeni lat zmieniła się okolica na nim przedstawiona.";
        public const string BannerLine2 = "Kliknij na ikonkę akapitu tekstu (znajdującą się na końcu linijki z tytułem wpisu), aby dowiedzieć się więcej na temat obiektu przedstawionego na fotografiach.";
        public const string BannerLine3 = "Kliknij na tag pod historyczną notatką, aby zobaczyć więcej wpisów o podobnej tematyce.";
        public const string BannerCloseButton = "Wszystko jasne, zaczynajmy!";

        #endregion

        #region CardComponent

        public const string AddReply = "Skomentuj";
        public const string AddFirstReply = "Skomentuj jako pierwszy!";
        public const string FigcaptionBase = "Data wykonania zdjęcia: ";
        public const string HideReplies = "Ukryj komentarze";
        public const string MapIcon = "Pokaż na mapie";
        public const string MoreDetailsIcon = "Pokaż szczegóły";
        public const string Score = "Ocena: ";
        public const string ShowReplies = "Pokaż komentarze";
        public const string ThumbsDown = "Zagłosuj na nie";
        public const string ThumbsUp = "Zagłosuj na tak";
        public const string Vote = "głos";
        public const string Votes234 = "głosy";
        public const string Votes56789 = "głosów";

        #endregion

        #region NoData

        public const string NavigateToHomepage = "Powrót do strony głównej";
        public const string NoData = "Brak wyników spełniających kryteria wyszukiwania.";

        #endregion

        #region Sorting

        public static string IdAscending = GetOptionLabel(Id, Ascending);
        public static string IdDescending = GetOptionLabel(Id, Descending);
        public static string TitleAscending = GetOptionLabel(Title, Ascending);
        public static string TitleDescending = GetOptionLabel(Title, Descending);
        public static string DateNowAscending = GetOptionLabel(DateNow, Ascending);
        public static string DateNowDescending = GetOptionLabel(DateNow, Descending);

        private const string Id = "Numer wpisu";
        private const string Title = "Tytuł wpisu";
        private const string DateNow = "Data wykonania zdjęcia współczesnego";
        private const string Ascending = "rosnąco";
        private const string Descending = "malejąco";

        private static string GetOptionLabel(string sortBy, string sortDirection)
        {
            return $"{sortBy} ({sortDirection})";
        }

        #endregion

        public const string Loading = "Ładowanie...";
        public const string NotFound = "Niestety, nie udało znaleźć się strony pod tym adresem.";
        public const string OriginalPhotoFirst = "Oryginalne zdjęcie jako pierwsze";
        public const string PageTitle = "Poznań | Dawniej & Dziś";
        public const string TagsResult = "Rezultaty wyszukiwania dla tagu: ";
    }
}
