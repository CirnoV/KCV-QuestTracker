﻿using System;
using System.Collections.Generic;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     海上輸送路の安全確保に努めよ！
    /// </summary>
    internal class Bw10 : ITracker
    {
        private readonly List<string> boss_names = new List<string>
                                                   {
                                                       "敵通商破壊主力艦隊"
                                                   };

        private readonly int map_id = 1;
        private readonly int max_count = 3;

        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 261;

        string ITracker.WikiIndex => "Bw10";

        string ITracker.Name => "海上輸送路の安全確保に努めよ！";

        QuestType ITracker.Type => QuestType.Weekly;

        public bool IsTracking { get; set; }

        public void RegisterEvent(ApiEvent apiEvent)
        {
            apiEvent.BattleResultEvent += (sender, args) =>
                                          {
                                              if (!IsTracking)
                                                  return;

                                              if (args.MapAreaId != map_id)
                                                  return;

                                              if (!boss_names.Contains(args.EnemyName))
                                                  return;

                                              if (args.Rank != "S" && args.Rank != "A")
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
            return count >= max_count ? "完成" : $"{count} / {max_count}";
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