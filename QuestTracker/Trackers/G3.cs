﻿using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     「近代化改修」を進め、戦備を整えよ！
    /// </summary>
    internal class G3 : ITracker
    {
        private readonly int max_count = 15;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 703;

        string ITracker.WikiIndex => "G3";

        string ITracker.Name => "근대화개수를 진행하여 군비를 갖춰라";

        QuestType ITracker.Type => QuestType.Weekly;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.PowerUpEvent += (sender, args) =>
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
            return count >= max_count ? "완료" : $"근대화 개수 {count} / {max_count}";
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