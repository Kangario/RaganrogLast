using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

namespace RLandscape
{
    public class RLoadWorld : IRLoadWorld
    {
        private GenerateLandscape generateLandscape;
        [SerializeField] private List<LandscapeElementsArray> landscapeElementsArray = new List<LandscapeElementsArray>();
        [SerializeField] private List<GameObject> landscapeObjects = new List<GameObject>();
        [SerializeField] private List<LandscapeData> landscapeData = new List<LandscapeData>();
        public RLoadWorld(GenerateLandscape generate_Landscape)
        {
            this.generateLandscape = generate_Landscape;
            this.landscapeElementsArray = generate_Landscape.landscapeElementsArray;
            this.landscapeObjects = generate_Landscape.landscapeObjects;
            this.landscapeData = generate_Landscape.landscapeData;
        }
        public void Load()
        {
            string nameWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().nameWorld;
            string folderPath = "Assets/Worlds/" + nameWorld + ".json";
            string json = File.ReadAllText(folderPath);
            LandscapeDataWrapper wrapper = JsonUtility.FromJson<LandscapeDataWrapper>(json);
            landscapeData.AddRange(wrapper.data);
            for (int i = 0; i < landscapeData.Count; i++)
            {
                GameObject objectLandscapeTemp = new GameObject();
                objectLandscapeTemp.transform.position = new Vector3(landscapeData[i].x, landscapeData[i].y, landscapeData[i].z);
                objectLandscapeTemp.AddComponent<SpriteRenderer>();
                objectLandscapeTemp.GetComponent<SpriteRenderer>().sprite = landscapeElementsArray[landscapeData[i].numberLandscape].landscapeElement[landscapeData[i].numberTiles].spriteObject;
                landscapeObjects.Add(objectLandscapeTemp);
            }
        }
    }
}
