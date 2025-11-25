using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Enums;
using ThenAndNow.Models.UI;

namespace ThenAndNow.Pages
{
    [Route(Routes.Contact)]
    public partial class Contact
    {
        public static readonly SocialMediaType[] SocialButtons =
        [
            SocialMediaType.MailToAuthor,
            SocialMediaType.Flickr,
            SocialMediaType.GitHub
        ];

        private static readonly NavItem[] NavItems =
        [
            new()
            {
                Label = Labels.Cyryl,
                Route = Routes.Cyryl
            },
            new()
            {
                Label = Labels.Fotopolska,
                Route = Routes.Fotopolska
            },
        ];
    }
}