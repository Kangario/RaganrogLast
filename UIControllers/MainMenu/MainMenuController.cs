using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    float currentWindow = 0;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private GameObject[] windowGame;
    [SerializeField] private GameObject nameWorld;
    [SerializeField] private GameObject sizeWorld;
    [SerializeField] private GameObject listWorlds;
    [SerializeField] private Text labelWorlds;
    private void Start()
    {
        string folderPath = "Assets/Worlds/";
        string[] files = Directory.GetFiles(folderPath);
        List<string> fileNames = new List<string>();
        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = Path.GetExtension(filePath);
            if (fileExtension != ".meta")
            {
                fileNames.Add(fileName);
            }
        }
        listWorlds.GetComponent<Dropdown>().ClearOptions();
        listWorlds.GetComponent<Dropdown>().AddOptions(fileNames);
    }

    public void Next()
    {

       
            currentWindow += 21f;
            m_Camera.transform.position = new Vector2 (currentWindow, m_Camera.transform.position.y);
        
      

    }
    public void Back()
    {

            currentWindow -= 21f;
            m_Camera.transform.position = new Vector2(currentWindow, m_Camera.transform.position.y);
        
       

    }
    public void Exite()
    {
        Application.Quit();
    }
    public void CreateLevel()
    {
        GameObject transferPref = new GameObject();
            transferPref.AddComponent<DataTransfer>();
            transferPref.GetComponent<DataTransfer>().nameWorld = nameWorld.GetComponent<InputField>().text;
        transferPref.name = "TransferData";
        
            switch (sizeWorld.GetComponent<Dropdown>().value)
            {
                case 0:
                    transferPref.GetComponent<DataTransfer>().sizeWorld = 100;
                    break;
                case 1:
                    transferPref.GetComponent<DataTransfer>().sizeWorld = 200;
                    break;
                case 2:
                    transferPref.GetComponent<DataTransfer>().sizeWorld = 300;
                    break;
        }
        
        SceneManager.LoadScene(0);

    }
    public void LoadLevel()
    {
        GameObject transferPref = new GameObject();
        transferPref.AddComponent<DataTransfer>();
        transferPref.GetComponent<DataTransfer>().nameWorld = labelWorlds.text;
        transferPref.name = "TransferData";
        transferPref.GetComponent<DataTransfer>().LoadWorld = true;
        SceneManager.LoadScene(0);
    }


}
