using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class BallsMovementLogs
    {
        public static string Path = "C:/Users/Public/";
        private readonly ConcurrentQueue<JObject> _ballsQueue = new ConcurrentQueue<JObject>();
        private readonly Mutex _mutex = new Mutex();

        private readonly string _path;
        private readonly StreamWriter _writer;
        private Task _logTask;

        public BallsMovementLogs(string fileName)
        {
            if (fileName == "")
            {
                _path = Path + "/log" + DateTime.Now.ToString("HH_mm_ss_ffffff") + ".json";
            }
            else
            {
                _path = Path + fileName + ".json";
            }

            _writer = File.CreateText(_path);
        }

        public void LogBallsMovement(Ball ball)
        {
            _mutex.WaitOne();

            try
            {
                JObject newLog = JObject.FromObject(ball);
                newLog["Time"] = DateTime.Now.ToString("HH:mm:ss");
                _ballsQueue.Enqueue(newLog);

                if (_logTask == null || _logTask.IsCompleted)
                {
                    _logTask = Task.Factory.StartNew(this.LogToFile);
                }
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        private async Task LogToFile()
        {
            while (_ballsQueue.TryDequeue(out JObject ball))
            {
                await _writer.WriteLineAsync(ball.ToString());
            }
        }
    }
}