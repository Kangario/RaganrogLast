using System.Collections;
namespace RPlayer
{
    internal interface IPlayerStats
    {
        public void ChangeStat(PlayerStat typeStat, float value);
    }
}