
using UnityEngine;
using System.IO;
namespace RLandscape
{
    public class RSaveWorld : MonoBehaviour , IRSaveWorld
    {
        [SerializeField] private string nameWorld;
        public void SaveWorld()
        {
            nameWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().nameWorld;
            if (GameObject.Find("GeneratorLandscape") != null)
            {
                GenerateLandscape generator = GameObject.Find("GeneratorLandscape").GetComponent<GenerateLandscape>();
                // Создаем объект для сериализации
                LandscapeData[] dataArray;
                dataArray = generator.landscapeData.ToArray(); // Предполагая, что landscapeData - это List<LandscapeData>
                // Сериализуем в JSON
                string json = "{\r\n  \"data\":[";
                foreach (LandscapeData dt in dataArray)
                {
                    json += JsonUtility.ToJson(dt);
                    json += ",\n";
                }
                json = json.Remove(json.Length - 2);
                json += "]\r\n}";
                // Сохраняем в файл
                string nameWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().nameWorld;
                File.WriteAllText("Assets/Worlds/" + nameWorld + ".json", json);
                Debug.Log("World saved to worldData.json");
            }
            else
            {
                Debug.Log("Dont have world!");
            }
        }


    }
}