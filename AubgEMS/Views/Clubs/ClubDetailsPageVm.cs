using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;

namespace AubgEMS.Core.Models.Clubs
{
    public class ClubDetailsPageVm
    {
        public ClubDetailsDto Club { get; init; } = new();
        public PageResult<EventListItemDto> Events { get; init; } = null!;
    }
}