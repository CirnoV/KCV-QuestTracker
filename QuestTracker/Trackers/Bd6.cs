﻿using System;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     敵輸送船団を叩け！
    /// </summary>
    internal class Bd6 : ITracker
    {
        private readonly int max_count = 5;
        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 212;

        string ITracker.WikiIndex => "Bd6";

        string ITracker.Name => "적 수송선단을 쳐라";

        QuestType ITracker.Type => QuestType.Daily;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.BattleResultEvent += (sender, args) =>
                                          {
                                              if (!IsTracking)
                                                  return;

                                              foreach (var ship in args.EnemyShips)
                                              {
                                                  // 15 = AP
                                                  if (ship.Type == 15)
                                                      if (ship.MaxHp != int.MaxValue && ship.NowHp <= 0)
                                                          count += count >= max_count ? 0 : 1;
                                              }

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
            return count >= max_count ? "완료" : $"보급함 {count} / {max_count}";
        }

        public string SerializeData()
        {
            return
                $"{count}";
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