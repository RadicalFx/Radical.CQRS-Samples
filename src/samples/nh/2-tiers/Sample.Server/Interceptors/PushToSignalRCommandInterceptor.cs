using Microsoft.AspNet.SignalR;
using Radical.CQRS.Server;
using Sample.Server.Push;
using System;

namespace Sample.Server.Interceptors
{
    class PushToSignalRCommandInterceptor : Jason.Handlers.ICommandInterceptor
    {
        readonly IOperationContextManager operationContextManager;

        public PushToSignalRCommandInterceptor( IOperationContextManager operationContextManager )
        {
            this.operationContextManager = operationContextManager;
        }

        public void OnException( object rawCommand, Exception exception )
        {
            var context = this.operationContextManager.GetCurrent();

            var ctx = GlobalHost.ConnectionManager.GetHubContext<ClientNotificastionsHub>();
            ctx.Clients.All.OnCommandFailed( rawCommand.GetType().Name + "Failed", new FailureNotification()
            {
                Error = exception,
                CorrelationId = context.CorrelationId
            } );
        }

        public void OnExecute( object rawCommand )
        {

        }

        public void OnExecuted( object rawCommand, object rawResult )
        {
            var context = this.operationContextManager.GetCurrent();

            var ctx = GlobalHost.ConnectionManager.GetHubContext<ClientNotificastionsHub>();
            ctx.Clients.All.OnCommandExecuted( rawCommand.GetType().Name + "Executed", new SuccessNotification()
            {
                Result = rawResult,
                CorrelationId = context.CorrelationId
            } );
        }
    }
}
