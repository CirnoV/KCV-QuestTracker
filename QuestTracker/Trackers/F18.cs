using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     装備の改修強化
    /// </summary>
    internal class F18 : ITracker
    {
        private readonly int max_count = 1;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 619;

        string ITracker.WikiIndex => "F18";

        string ITracker.Name => "장비의 개수강화";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.ReModelEvent += (sender, args) =>
                                     {
                                         if (!IsTracking)
                                             return;

                                         count += count >= max_count ? 0 : 1;

                                         ProcessChanged?.Invoke(this, new EventArgs());
                                     };
        }

        public void ResetQuest()
        {
            count = 0;

            ProcessChanged?.Invoke(this, new EventArgs());
        }

        public double GetPercentProcess()
        {
            return (double)count / max_count * 100;
        }

        public string GetDisplayProcess()
        {
            return count >= max_count ? "완료" : $"장비 개수 {count} / {max_count}";
        }

        public string SerializeData()
        {
            return $"{count}";
        }

        public void DeserializeData(string data)
        {
            try
            {
                count = int.Parse(data);
            }
            catch
            {
                count = 0;
            }
        }

        #endregion
    }
}