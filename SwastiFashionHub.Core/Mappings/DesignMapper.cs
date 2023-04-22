using AutoMapper;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.Core.Mappings
{
    public class DesignMapper : Profile
    {
        public DesignMapper()
        {
            CreateMap<Design, DesignRequest>().ReverseMap();
            CreateMap<Design, DesignResponse>().ReverseMap();
            CreateMap<DesignImage, DesignImagesRequest>().ReverseMap();
        }
    }
}
