namespace ConsoleApp6
{
    public class ThreadPoolWorker<TResult>
    {
        private readonly Func<object, TResult> _func;
        private TResult _result;

        public bool Completed { get; private set; } = false;
        public bool IsSuccess { get; private set; } = false;

        public TResult Result
        {
            get
            {
                while (Completed == false)
                {
                    Thread.Sleep(50);
                }
                return IsSuccess == true ? _result : throw new Exception();
            }
        }

        public ThreadPoolWorker(Func<object, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException();
        }

        public void Start(object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecute), state);
        }

        private void ThreadExecute(object state)
        {
            try
            {
                _result = _func.Invoke(state);
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                IsSuccess = false;
            }
            finally
            {
                Completed = true;
            }
        }
    }
}
