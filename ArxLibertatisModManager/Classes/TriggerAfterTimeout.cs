using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.Classes
{
    public class TriggerAfterTimeout
    {
        private readonly int milliseconds;
        private readonly Func<Task> target;

        private CancellationTokenSource? cts = null;
        private bool inTimeout = false;
        private Task? triggerTask = null;

        public TriggerAfterTimeout(int milliseconds, Func<Task> target)
        {
            this.milliseconds = milliseconds;
            this.target = target;
        }

        async Task TriggerTask(CancellationToken cancellationToken)
        {
            await Task.Delay(milliseconds, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            await target();
            cts = null;
            triggerTask = null;
            inTimeout = false;
        }

        public void Trigger()
        {
            if (!inTimeout)
            {
                cts = new CancellationTokenSource();
                triggerTask = TriggerTask(cts.Token);
                inTimeout = true;
            }
            else
            {
                cts?.Cancel();
                cts = new CancellationTokenSource();
                triggerTask = TriggerTask(cts.Token);
            }
        }
    }
}
