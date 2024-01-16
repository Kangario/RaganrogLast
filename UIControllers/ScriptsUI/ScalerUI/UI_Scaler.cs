using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scaler : MonoBehaviour
{
    private Vector2 screenSize;
    void Start()
    {
        // Сохраняем начальные размеры объекта
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
        // Вычисляем отношение старого размера к новому размеру
        float widthRatio = Screen.width / screenSize.x;
        float heightRatio = Screen.height / screenSize.y;

        // Находим минимальное из отношений, чтобы сохранить пропорции
        float minRatio = Mathf.Min(widthRatio, heightRatio);

        // Применяем отношение к каждой оси
        Vector2 newScale = new Vector2(screenSize.x * minRatio, screenSize.y * minRatio);

        // Присваиваем объекту новый размер
        GetComponent<RectTransform>().sizeDelta = newScale;
    }
}
