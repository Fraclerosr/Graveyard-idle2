using System;

namespace BabyStack.Model
{
    public class Timer : ITimer
    {
        private bool _isStarted;

        public event Action<float> Started;
        public event Action Stopped;
        public event Action Completed;
        public event Action<float> Updated;

        public float Time { get; private set; }
        public float ElapsedTime { get; private set; }

        public void Tick(float tick)
        {
            if (_isStarted == false)
                return;

            ElapsedTime += tick;
            Updated?.Invoke(ElapsedTime);

            if (ElapsedTime >= Time)
            {
                _isStarted = false;
                Completed?.Invoke();
            }
        }

        public void Start(float time)
        {
            ElapsedTime = 0;
            Time = time;
            _isStarted = true;
            Started?.Invoke(Time);
        }

        public void Stop()
        {
            _isStarted = false;
            Stopped?.Invoke();
        }
    }

    public interface ITimer
    {
        public float Time { get; } 

        public abstract event Action<float> Started;
        public abstract event Action Stopped;
        public abstract event Action Completed;
        public abstract event Action<float> Updated;
    }
}
