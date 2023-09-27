using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RaboBankTest.Application.Feature
{
    public class ProcessDataRequest : IRequest
    {
        public DateTime ProcessDateTime { get; set; }
    }
}
