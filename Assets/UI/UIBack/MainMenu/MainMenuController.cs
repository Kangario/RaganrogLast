using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MainMenuController : UIToolKitConnectable
{
    float currentWindow = 0;
    [SerializeField] private Camera m_Camera;
    //[SerializeField] private GameObject[] windowGame;
    [SerializeField] private string nameWorld;
    [SerializeField] private string sizeWorld;
    [SerializeField] private string listWorlds;
    private TextField nameWorldInput;
    private Slider sizeWorldSlider;
    private DropdownField listWorld;

    private void Awake() => Connect();

    private void Start()
    {
        nameWorldInput = _rootElement.Q<TextField>(nameWorld);
        sizeWorldSlider = _rootElement.Q<Slider>(sizeWorld);
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
        listWorld = _rootElement.Q<DropdownField>(listWorlds);
        listWorld.choices.Clear();
        listWorld.choices = fileNames;
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
        transferPref.GetComponent<DataTransfer>().nameWorld = nameWorldInput.value;
        transferPref.name = "TransferData";
        transferPref.GetComponent<DataTransfer>().sizeWorld = (int)sizeWorldSlider.value;
        SceneManager.LoadScene(0);

    }
    public void LoadLevel()
    {
        GameObject transferPref = new GameObject();
        transferPref.AddComponent<DataTransfer>();
        transferPref.GetComponent<DataTransfer>().nameWorld = listWorld.value;
        transferPref.name = "TransferData";
        transferPref.GetComponent<DataTransfer>().LoadWorld = true;
        SceneManager.LoadScene(0);
    }


}
