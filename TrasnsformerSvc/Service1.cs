using System;
using System.Collections.Generic;
using Boundaries.DocumentTransformation;
using System.ServiceProcess;
using Boundaries.PdfEngine;
using TrasnsformerSvc.Contract;
using TrasnsformerSvc.Converter;
using TrasnsformerSvc.Runners;

namespace TrasnsformerSvc
{
    public partial class Service1 : ServiceBase
    {
        private IServiceRunner runner;
        private IDocumentConverter converter;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var api = new ServiceSettings();
            var config = api.GetServiceConfig();
            LogMessage($"Configuration retrieve successfully");
            LogMessage($"Service configure for run with {config.Settings.WorkMode}");
            runner = new TimerRunner(api, config.Settings); //new TimerRunner(api,config);
            runner.Build();
            runner.OnServiceRunnerEventHandler += ReceivedData;
            converter = new DocumentConvert(config.Engines, config.Rules);
            converter.BuildEngines();
        }

        private async void ReceivedData(object sender, ServiceReachedEventArgs e)
        {
            ResumeProcess(false);
            var confirmMessage = e.Type == QueueReceiverType.Batches ? await converter.GenerateDocuments(e.JsonData) : await converter.GenerateDocument(e.JsonData); ;
            LogMessage($"{confirmMessage}");
            ResumeProcess(true);
        }

        void ResumeProcess(bool resume)
        {
            if (resume)
            {
                ((TimerRunner)runner).clock.Start();
            }
            else
            {
                ((TimerRunner)runner).clock.Stop();
            }
        }

        protected override void OnStop()
        {
            runner.StopRunner();
        }

        public void OnStartStopDebug(string[] args)
        {
            OnStart(args);
            Console.WriteLine($"Press any key to exist");
            Console.ReadKey();
            OnStop();
        }

        private void LogMessage(string message)
        {
            Console.Out.WriteLine(message);
        }
    }
}
