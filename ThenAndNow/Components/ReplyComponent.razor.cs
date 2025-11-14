using Microsoft.AspNetCore.Components;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Components
{
    public partial class ReplyComponent
    {
        [Parameter]
        public Reply Reply { get; set; }
    }
}