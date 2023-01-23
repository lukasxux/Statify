using AutoMapper;
using StatifyProject.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatifyProject.Application.Dto
{
    public class MappingProfile : Profile  
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User> ();
            CreateMap<User, UserDto>();



        }
    }
}
