using MediatR;
using RW.Position.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.events
{/// <summary>
/// MediatR传入参数
/// </summary>
    public class CreatePositionCommand:IRequest<string>
    {
        public List<PositionInfo> _positionInfos { get; set; }
        public CreatePositionCommand(List<PositionInfo> positionInfos) : this()
        {
            _positionInfos = positionInfos;
        }
        public CreatePositionCommand()
        {

        }
    }
}
