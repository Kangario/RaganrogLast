using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scaler : MonoBehaviour
{
    private Vector2 screenSize;
    void Start()
    {
        // ��������� ��������� ������� �������
        screenSize = GetComponent<RectTransform>().sizeDelta;

        
    }
    private void Update()
    {
        if (Screen.width != screenSize.x || Screen.height != screenSize.y)
        {
          UpdateScale();
        }
    }
    void UpdateScale()
    {
        // ��������� ��������� ������� ������� � ������ �������
        float widthRatio = Screen.width / screenSize.x;
        float heightRatio = Screen.height / screenSize.y;

        // ������� ����������� �� ���������, ����� ��������� ���������
        float minRatio = Mathf.Min(widthRatio, heightRatio);

        // ��������� ��������� � ������ ���
        Vector2 newScale = new Vector2(screenSize.x * minRatio, screenSize.y * minRatio);

        // ����������� ������� ����� ������
        GetComponent<RectTransform>().sizeDelta = newScale;
    }
}
