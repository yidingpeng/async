using MediatR;
using RW.Position.Models;
using RW.Position.websocketServers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RW.Position.events
{/// <summary>
/// MediatR 发送信息
/// </summary>
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand,string>
    {
        OnMessagePOSServers _onMessagePOSServers;
        public CreatePositionCommandHandler()
        {

        }
        public CreatePositionCommandHandler(OnMessagePOSServers onMessagePOSServers) :this()
        {
            _onMessagePOSServers = onMessagePOSServers;
        }
        public async Task<string> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            await _onMessagePOSServers.getPOSValue(request._positionInfos);
            return await Task.FromResult("1");
        }

        
    }
}
