using AutoMapper;
using CustomersAPI.DataAccess.Entities;
using CustomersAPI.Models;

namespace CustomersAPI.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
