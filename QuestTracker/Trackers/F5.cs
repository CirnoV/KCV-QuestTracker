using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     新装備「開発」指令
    /// </summary>
    internal class F5 : ITracker
    {
        private readonly int max_count = 1;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 605;

        string ITracker.WikiIndex => "F5";

        string ITracker.Name => "신 장비 개발 지령";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.CreateItemEvent += (sender, args) =>
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
            return count >= max_count ? "완료" : $"장비 개발 {count} / {max_count}";
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