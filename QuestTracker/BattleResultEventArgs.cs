namespace QuestTracker
{
    internal class BattleResultEventArgs
    {
        public string EnemyName;
        public EnemyShip[] EnemyShips;
        public bool IsFirstCombat;
        public int MapAreaId;
        public string Rank;
    }
}