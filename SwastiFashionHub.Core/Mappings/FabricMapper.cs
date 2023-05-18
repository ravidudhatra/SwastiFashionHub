using AutoMapper;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.Core.Mappings
{
    public class FabricMapper : Profile
    {
        public FabricMapper()
        {
            CreateMap<Fabric, FabricRequest>().ReverseMap();
            CreateMap<Fabric, FabricResponse>().ReverseMap();
        }
    }
}