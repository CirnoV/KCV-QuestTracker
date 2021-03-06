﻿using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     「演習」で練度向上！
    /// </summary>
    internal class C2 : ITracker
    {
        private readonly int max_count = 3;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 303;

        string ITracker.WikiIndex => "C2";

        string ITracker.Name => "연습으로 훈련도 향상";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.PracticeBattleResultEvent += (sender, args) =>
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
            return count >= max_count ? "완료" : $"연습 {count} / {max_count}";
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