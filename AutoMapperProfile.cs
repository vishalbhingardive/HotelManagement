using AutoMapper;
using HotelManagement.Data.Dtos;
using HotelManagement.Data.Models;

namespace HotelManagement
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}
