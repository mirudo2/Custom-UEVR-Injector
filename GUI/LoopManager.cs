using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Custom_UEVR_Injector {
	
	public static class LoopManager
	{
		private static CancellationTokenSource _cts;

		public static void StartLoop(main__form form, Action uiAction)
		{
			if (_cts != null && !_cts.IsCancellationRequested)
				return;

			_cts = new CancellationTokenSource();
			var token = _cts.Token;

			Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					form.BeginInvoke(uiAction);

					try
					{
						await Task.Delay(1, token);
					}
					catch (TaskCanceledException)
					{
						break;
					}
				}
			}, token);
		}

		public static void StopLoop()
		{
			_cts?.Cancel();
			_cts = null;
		}
	}
	
}
