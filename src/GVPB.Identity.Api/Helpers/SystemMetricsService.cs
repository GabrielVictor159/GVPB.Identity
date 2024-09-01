using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Gauge;
using Microsoft.Extensions.Hosting;

namespace GVPB.Identity.Api.Helpers
{
    public class SystemMetricsService : IHostedService
    {
        private readonly IMetrics metrics;
        private Timer timer;

        public SystemMetricsService(IMetrics metrics)
        {
            this.metrics = metrics;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(CollectMetrics, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private void CollectMetrics(object state)
        {
            var cpuUsage = GetCpuUsage();
            var memoryUsage = GetMemoryUsage();

            metrics.Measure.Gauge.SetValue(new GaugeOptions
            {
                Name = "System CPU Usage",
                MeasurementUnit = Unit.Percent
            }, cpuUsage);

            metrics.Measure.Gauge.SetValue(new GaugeOptions
            {
                Name = "System Memory Usage",
                MeasurementUnit = Unit.Bytes
            }, memoryUsage);
        }

        private double GetCpuUsage()
        {
            var cpuInfo = File.ReadAllText("/proc/stat");
            var cpuLines = cpuInfo.Split('\n');
            var cpuLine = cpuLines[0];

            var cpuStats = cpuLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (cpuStats.Length < 5)
            {
                return 0;
            }

            long user = long.Parse(cpuStats[1]);
            long nice = long.Parse(cpuStats[2]);
            long system = long.Parse(cpuStats[3]);
            long idle = long.Parse(cpuStats[4]);
            long total = user + nice + system + idle;

            var usage = (double)(total - idle) / total * 100;
            return usage;
        }

        private double GetMemoryUsage()
        {
            var process = Process.GetCurrentProcess();
            return process.WorkingSet64 / (1024.0 * 1024.0);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
