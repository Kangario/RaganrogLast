using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_Tool : EditorWindow
{
    private List<Texture2D> blanks  = new List<Texture2D>();
    [MenuItem("Window/UI_adaptation")]
    public static void ShowWindow()
    {
        GetWindow<UI_Tool>("UI_adaptation");
    }
    void OnEnable()
    {
        blanks.Clear();
        // Загрузка вашего изображения. Замените путь на путь к вашему ресурсу или изображению.
        blanks.Add (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Tools/Tools_UI/Images/Blank1.png"));
        blanks.Add (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Tools/Tools_UI/Images/Blank2.png"));
        blanks.Add (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Tools/Tools_UI/Images/Blank3.png"));
    }

    void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
        labelStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("UI заготовки",labelStyle, GUILayout.ExpandWidth(true));
        GUILayout.Label("Выберите вариант заготовки для UI интерфейса.", labelStyle, GUILayout.ExpandWidth(true));
        GUILayout.BeginHorizontal();
        for (int i = 0; i < blanks.Count; i++)
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(blanks[i], GUILayout.Width(blanks[i].width / 5), GUILayout.Height(blanks[i].height / 5)))
            {
                CreateUITool creater = new CreateUITool();
                switch (i) {
                    case 0:
                        creater.CreateUIFirst();
                        break;
                    case 1:
                        creater.CreateUISecond();
                        break;
                    case 2:
                        creater.CreateUIThird();
                        break;
                }
            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        // Добавьте здесь дополнительные элементы интерфейса по вашему выбору
    }
   
}

public class CreateUITool:ICreateUITool
{
    public void CreateUIFirst()
    {
        GameObject Canvas = CreateCanvas("FirstCanvas");
        GameObject panelLeft = CreatePanel("LeftPanel", Canvas.transform,new Vector2(0,0),new Vector2(500,0));
        GameObject panelRight1 = CreatePanel("RightPanel", Canvas.transform, new Vector2(-435, 470), new Vector2(1369, 300),new AnchoredPos(0.5f, 0.5f, 0.5f, 0.5f));
        GameObject panelRight2 = CreatePanel("RightPanel", Canvas.transform, new Vector2(-435, 470-315), new Vector2(1369, 300), new AnchoredPos(0.5f, 0.5f, 0.5f, 0.5f));
        GameObject panelRight3 = CreatePanel("RightPanel", Canvas.transform, new Vector2(-435, 470-315*2), new Vector2(1369, 300), new AnchoredPos(0.5f, 0.5f, 0.5f, 0.5f));
        GameObject panelRight4 = CreatePanel("RightPanel", Canvas.transform, new Vector2(-435, 470-315*3), new Vector2(1369, 300), new AnchoredPos(0.5f, 0.5f, 0.5f, 0.5f));

    }
    public GameObject CreateCanvas(string nameCanvas)
    {
        GameObject Canvas = new GameObject(nameCanvas, typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        Canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        Canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        return Canvas;
    }
    public GameObject CreatePanel(string namePanel, Transform canvasTransform,Vector2 position, Vector2 size)
    {
        GameObject panelLeft = new GameObject(namePanel, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        panelLeft.transform.SetParent(canvasTransform);
        RectTransform panelTransform = panelLeft.GetComponent<RectTransform>();
        panelTransform.anchorMax = new Vector2(0, 1);
        panelTransform.anchorMin = new Vector2(0, 0);
        panelTransform.pivot = new Vector2(0, 0.5f);
        panelTransform.localScale = new Vector3(1, 1, 1);
        panelTransform.offsetMax = new Vector2(size.x + position.x,-size.y);
        panelTransform.offsetMin = new Vector2(position.x, position.y);
        return panelLeft;
    }
    public GameObject CreatePanel(string namePanel, Transform canvasTransform, Vector2 position, Vector2 size, AnchoredPos anchoredPosition)
    {
        GameObject panelLeft = new GameObject(namePanel, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        panelLeft.transform.SetParent(canvasTransform);
        RectTransform panelTransform = panelLeft.GetComponent<RectTransform>();
        panelTransform.anchorMax = new Vector2(anchoredPosition.point1, anchoredPosition.point2);
        panelTransform.anchorMin = new Vector2(anchoredPosition.point3, anchoredPosition.point4);
        panelTransform.pivot = new Vector2(0, 0.5f);
        panelTransform.localScale = new Vector3(1, 1, 1);
        panelTransform.sizeDelta = new Vector2(size.x , size.y);
        panelTransform.anchoredPosition = new Vector2(position.x, position.y);
        return panelLeft;
    }
    public void CreateUISecond()
    {
        Debug.Log("Второй ");

    }
    public void CreateUIThird()
    {
        Debug.Log("Третий ");

    }
}
