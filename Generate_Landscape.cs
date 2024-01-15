using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.Mathematics;
using static EnumForLandscape;



[Serializable]
public class Landscape_Elements_Array
{
    public landscape_Elements[] landscapeElement;
}
[Serializable]
public class Landscape_Decor_Array
{
    public Landscape_Elements_Decor[] landscapeDecor;
}
[Serializable]
public class LandscapeDataWrapper
{
    public LandscapeData[] data;
}
[System.Serializable]
public class LandscapeData
{
    public float x;
    public float y;
    public float z;
    public int id;
    public int numberLandscape;
    public int numberTiles;
}
public class Generate_Landscape : MonoBehaviour
{
    [SerializeField] public List<Landscape_Elements_Array> landscapeElementsArray = new List<Landscape_Elements_Array>();
    [SerializeField] public List<Landscape_Decor_Array> landscapeDecorArray = new List<Landscape_Decor_Array>();
    [SerializeField] public List<GameObject> landscapeObjects = new List<GameObject>();
    [SerializeField] public List<LandscapeData> landscapeData = new List<LandscapeData>();
    [SerializeField] private int decorFrequency = 0;
    [SerializeField] private int sizeWorld = 0;
    [SerializeField] private float offset = 0;
    private System.Random random = new System.Random();
    private void GenerateDecor()
    {
        for (int x = sizeWorld-3; x > 3; x--) {
            int orderLayer = 1;
            for(int y = sizeWorld-3; y>3; y--) {
            
                float sample = Mathf.PerlinNoise(x/offset, y/offset);
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
                            if (landscapeDecorArray[i].landscapeDecor[typeDecor].nameObject == classDecor.Tree) {
                                spawnedObject.GetComponent<BoxCollider2D>().size = new Vector2(0.25f, 0.25f);
                                spawnedObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.45f);

                                    }
                            spawnedObject.transform.position = new Vector2(x * 0.32f, y * 0.32f);
                            spawnedObject.GetComponent<SpriteRenderer>().sortingOrder = orderLayer;
                            landscapeObjects.Add(spawnedObject);
                            SaveLandscapeData(spawnedObject, landscapeDecorArray[i].landscapeDecor[typeDecor].id,i, typeDecor);
                            
                            break;
                        }
                        break;
                    }
                }
                orderLayer++;
            }
        
        }
    }
    private void Generate()
    {
        sizeWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().sizeWorld;
        for (int  x = 0; x<sizeWorld; x++)
        {
            for (int y =0; y<sizeWorld; y++)
            {
                float sample = Mathf.PerlinNoise(x/offset,y/offset);
                
                for (int i=0; i< landscapeElementsArray.Count; i++)
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
                        spawnedObject.transform.position =  new Vector2(x * 0.32f, y* 0.32f); 
                        landscapeObjects.Add(spawnedObject);
                        SaveLandscapeData(spawnedObject, landscapeElementsArray[i].landscapeElement[type].id,i,type);   
                        break;
                        
                    }
                }


                
            }
        }
    }
    private void SaveLandscapeData(GameObject saveObject, int id, int numberIteration, int numberTile)
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
    private void Load()
    {
        string nameWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().nameWorld;
        string folderPath = "Assets/Worlds/" + nameWorld + ".json";
        string json = File.ReadAllText(folderPath);
        LandscapeDataWrapper wrapper = JsonUtility.FromJson<LandscapeDataWrapper>(json);
        landscapeData.AddRange(wrapper.data);
        for (int i = 0; i <landscapeData.Count; i++)
        {
            GameObject objectLandscapeTemp = new GameObject();
            objectLandscapeTemp.transform.position = new Vector3 (landscapeData[i].x, landscapeData[i].y, landscapeData[i].z);
            objectLandscapeTemp.AddComponent<SpriteRenderer>();
            
                objectLandscapeTemp.GetComponent<SpriteRenderer>().sprite = landscapeElementsArray[landscapeData[i].numberLandscape].landscapeElement[landscapeData[i].numberTiles].spriteObject;
            
                landscapeObjects.Add(objectLandscapeTemp);
            
        }

    }
    private void Start()
    {   
        if (GameObject.Find("TransferData").GetComponent<DataTransfer>().LoadWorld == false)
        {
           
            Generate();
            GenerateDecor();
            Debug.LogWarning(landscapeData.Count);
        }
        else
        {
            Load();
        }

      
    }


    private void Update()
    {
        
    }
}
