using AutoMapper;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.Core.Mappings
{
    public class PartyMapper : Profile
    {
        public PartyMapper()
        {
            CreateMap<Party, PartyRequest>().ReverseMap();
            CreateMap<Party, PartyResponse>().ReverseMap();
        }
    }
}