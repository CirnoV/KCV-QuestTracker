﻿using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     「演習」で他提督を圧倒せよ！
    /// </summary>
    internal class C3 : ITracker
    {
        private readonly int max_count = 5;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 304;

        string ITracker.WikiIndex => "C3";

        string ITracker.Name => "연습으로 다른 제독 압도";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.PracticeBattleResultEvent += (sender, args) =>
                                                  {
                                                      if (!IsTracking)
                                                          return;

                                                      if (!args.IsSuccess)
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
            return count >= max_count ? "완료" : $"연습전 승리 {count} / {max_count}";
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