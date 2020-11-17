// http://neue.cc/2013/10/10_429.html

using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hondarersoft.Utility
{
    /// <summary>
    /// <see cref="Task"/> の拡張メソッドを提供します。
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// 待ち合せない <see cref="Task"/> で発生した例外の通知イベントを保持します。
        /// </summary>
        public static event EventHandler<NoWaitTaskExceptionEventArgs> NoWaitTaskException;

        /// <summary>
        /// <see cref="Task"/> を待ち合わせないことを明示的に宣言します。
        /// このメソッドを呼ぶことでコンパイラの警告の抑制と、例外発生時のロギングを行います。
        /// </summary>
        public static void NoWaitAndWatchException(this Task task, LogLevel logLevel = LogLevel.Critical)
        {
            task.ContinueWith(x =>
            {
                if (NoWaitTaskException != null)
                {
                    NoWaitTaskException(null, new NoWaitTaskExceptionEventArgs(x.Exception, logLevel));
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
