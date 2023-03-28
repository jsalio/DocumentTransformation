using System;
using System.Linq;
using System.Text;

namespace TrasnsformerSvc.Contract
{

    public delegate void ServiceReachedEventHandler(Object sender, ServiceReachedEventArgs e);

    interface IServiceRunner
    {
        event ServiceReachedEventHandler OnServiceRunnerEventHandler;
        void Build();
        void StopRunner();
    }

}
