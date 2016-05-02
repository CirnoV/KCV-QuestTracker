using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     軍縮条約対応！
    /// </summary>
    internal class F9 : ITracker
    {
        private readonly int max_count = 2;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 609;

        string ITracker.WikiIndex => "F9";

        string ITracker.Name => "군축조약 대응";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.DestoryShipEvent += (sender, args) =>
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
            return count >= max_count ? "완료" : $"칸무스 해체 {count} / {max_count}";
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