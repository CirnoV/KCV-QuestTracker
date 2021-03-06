﻿using System;
using System.Collections.Generic;

namespace QuestTracker.Trackers
{
    /// <summary>
    ///     敵東方中枢艦隊を撃破せよ！
    /// </summary>
    internal class Bw8 : ITracker
    {
        private readonly List<string> boss_names = new List<string>
                                                   {
                                                       "敵東方中枢艦隊"
                                                   };

        private readonly int map_id = 4;
        private readonly int max_count = 1;

        private int count;

        #region ITracker

        public event EventHandler ProcessChanged;

        int ITracker.Id => 242;

        string ITracker.WikiIndex => "Bw8";

        string ITracker.Name => "적 동방 중추함대 격멸";

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

                                              if (args.Rank != "S" && args.Rank != "A" && args.Rank != "B")
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
            return (double)count * 100;
        }

        public string GetDisplayProcess()
        {
            return count >= 0 ? "완료" : "4-2 보스 승리 0 / 1";
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