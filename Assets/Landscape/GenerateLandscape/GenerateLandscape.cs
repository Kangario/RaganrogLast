using System.Collections.Generic;
using UnityEngine;
namespace RLandscape
{
    public class GenerateLandscape : MonoBehaviour , IGenerateLandscape
    {
        [SerializeField] public List<LandscapeElementsArray> landscapeElementsArray = new List<LandscapeElementsArray>();
        [SerializeField] public List<LandscapeDecorArray> landscapeDecorArray = new List<LandscapeDecorArray>();
        [SerializeField] public List<GameObject> landscapeObjects = new List<GameObject>();
        [SerializeField] public List<LandscapeData> landscapeData = new List<LandscapeData>();
        [SerializeField] private int decorFrequency = 0;
        [SerializeField] private int sizeWorld = 0;
        [SerializeField] private float offset = 0;
        private RLoadWorld loadWorld;
        private System.Random random = new System.Random();
        public void GenerateDecor()
        {
            for (int x = sizeWorld - 3; x > 3; x--)
            {
                int orderLayer = 1;
                for (int y = sizeWorld - 3; y > 3; y--)
                {

                    float sample = Mathf.PerlinNoise(x / offset, y / offset);
                    for (int i = 0; i < landscapeDecorArray.Count; i++)
                    {
                        if (sample > landscapeDecorArray[i].landscapeDecor[0].rangeHeightMin && sample < landscapeDecorArray[i].landscapeDecor[0].rangeHeightMax)
                        {
                            int percent = random.Next(0, 100);
                            if (percent <= decorFrequency)
                            {

                                GameObject spawnedObject = new GameObject();
                                spawnedObject.AddComponent<SpriteRenderer>();
                                int typeDecor = random.Next(0, landscapeDecorArray[i].landscapeDecor.Length);
                                spawnedObject.GetComponent<SpriteRenderer>().sprite = landscapeDecorArray[i].landscapeDecor[typeDecor].spriteObject;
                                spawnedObject.AddComponent<BoxCollider2D>();
                                if (landscapeDecorArray[i].landscapeDecor[typeDecor].nameObject == classDecor.Tree)
                                {
                                    spawnedObject.GetComponent<BoxCollider2D>().size = new Vector2(0.25f, 0.25f);
                                    spawnedObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.45f);

                                }
                                spawnedObject.transform.position = new Vector2(x * 0.32f, y * 0.32f);
                                spawnedObject.GetComponent<SpriteRenderer>().sortingOrder = orderLayer;
                                landscapeObjects.Add(spawnedObject);
                                SaveLandscapeData(spawnedObject, landscapeDecorArray[i].landscapeDecor[typeDecor].id, i, typeDecor);

                                break;
                            }
                            break;
                        }
                    }
                    orderLayer++;
                }
            }
        }
        public void Generate()
        {
            sizeWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().sizeWorld;
            for (int x = 0; x < sizeWorld; x++)
            {
                for (int y = 0; y < sizeWorld; y++)
                {
                    float sample = Mathf.PerlinNoise(x / offset, y / offset);

                    for (int i = 0; i < landscapeElementsArray.Count; i++)
                    {
                        if (sample > landscapeElementsArray[i].landscapeElement[0].rangeHeightMin && sample < landscapeElementsArray[i].landscapeElement[0].rangeHeightMax)
                        {
                            int type = random.Next(0, landscapeElementsArray[i].landscapeElement.Length);
                            GameObject spawnedObject = new GameObject();
                            spawnedObject.AddComponent<SpriteRenderer>();
                            spawnedObject.GetComponent<SpriteRenderer>().sprite = landscapeElementsArray[i].landscapeElement[type].spriteObject;
                            if (landscapeElementsArray[i].landscapeElement[type].nameObject == classLandscape.Stone || landscapeElementsArray[i].landscapeElement[type].nameObject == classLandscape.Water)
                            {
                                spawnedObject.AddComponent<BoxCollider2D>();
                            }
                            spawnedObject.transform.position = new Vector2(x * 0.32f, y * 0.32f);
                            landscapeObjects.Add(spawnedObject);
                            SaveLandscapeData(spawnedObject, landscapeElementsArray[i].landscapeElement[type].id, i, type);
                            break;

                        }
                    }
                }
            }
        }
        public void SaveLandscapeData(GameObject saveObject, int id, int numberIteration, int numberTile)
        {
            LandscapeData ld = new LandscapeData();
            ld.x = saveObject.transform.position.x;
            ld.y = saveObject.transform.position.y;
            ld.z = saveObject.transform.position.z;
            ld.id = id;
            ld.numberLandscape = numberIteration;
            ld.numberTiles = numberTile;
            landscapeData.Add(ld);
        }
       
        private void Start()
        {
            loadWorld = new RLoadWorld(gameObject.GetComponent<GenerateLandscape>());
            if (GameObject.Find("TransferData").GetComponent<DataTransfer>().LoadWorld == false)
            {
                Generate();
                GenerateDecor();
                Debug.LogWarning(landscapeData.Count);
            }
            else
            {
                loadWorld.Load();
            }
        }
    }
}