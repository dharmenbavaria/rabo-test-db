using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaboBankTest.Domain.Data;
using RaboBankTest.Domain.Message;

namespace RaboBankTest.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomDataModel, CustomModelNotification>();
        }
    }
}
