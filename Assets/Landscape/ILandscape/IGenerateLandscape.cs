using UnityEngine;

namespace RLandscape
{
    internal interface IGenerateLandscape
    {
        public void GenerateDecor();
        public void Generate();
        public void SaveLandscapeData(GameObject saveObject, int id, int numberIteration, int numberTile);
    }
}