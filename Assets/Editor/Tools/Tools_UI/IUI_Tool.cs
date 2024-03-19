using UnityEngine;

interface ICreateUITool
{
    public void CreateUIFirst();
    public GameObject CreateCanvas(string nameCanvas);
    public GameObject CreatePanel(string namePanel, Transform canvasTransform, Vector2 position, Vector2 size);
    public GameObject CreatePanel(string namePanel, Transform canvasTransform, Vector2 position, Vector2 size, AnchoredPos anchoredPosition);
    public void CreateUISecond();
    public void CreateUIThird();

}