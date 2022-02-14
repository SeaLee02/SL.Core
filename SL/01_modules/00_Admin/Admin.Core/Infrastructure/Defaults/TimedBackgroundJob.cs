using Microsoft.Extensions.Hosting;
using SL.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SL.Mkh.Admin.Core.Infrastructure.Defaults
{
    /// <summary>
    /// 后台任务处理
    /// </summary>
    public class TimedBackgroundJob : BackgroundService
    {
        private Timer _timer;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            ConsoleHelper.WriteColorLine($"{DateTime.Now}-开始", ConsoleColor.Yellow);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            ConsoleHelper.WriteColorLine($"{DateTime.Now}-停止", ConsoleColor.Yellow);
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            ConsoleHelper.WriteSuccessLine($"{DateTime.Now}-执行");
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }



    }
}
