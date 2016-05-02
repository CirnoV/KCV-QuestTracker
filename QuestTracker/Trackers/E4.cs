using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     艦隊酒保祭り！
    /// </summary>
    internal class E4 : ITracker
    {
        private readonly int max_count = 15;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 504;

        string ITracker.WikiIndex => "E4";

        string ITracker.Name => "함대 PX 축제!";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.ChargeEvent += (sender, args) =>
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
            return count >= max_count ? "완료" : $"보급 {count} / {max_count}";
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