using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatProgress : VisualElement
{
    private VisualElement _progressBarBackground;
    private VisualElement _progressBarFill;
    private float _progress;

    public StatProgress()
    {
        var styleSheet = Resources.Load<StyleSheet>("CustomProgressBar");
        if (styleSheet != null)
        {
            styleSheets.Add(styleSheet);
        }
        else
        {
            Debug.LogError("StyleSheet not found! Make sure the USS file is placed in a Resources folder.");
        }

        _progressBarBackground = new VisualElement();
        _progressBarBackground.AddToClassList("progress-bar-background");
        Add(_progressBarBackground);

        _progressBarFill = new VisualElement();
        _progressBarFill.AddToClassList("progress-bar-fill");
        _progressBarBackground.Add(_progressBarFill);

        SetProgress(_progress);
    }

    public float Progress
    {
        get => _progress;
        set
        {
            _progress = Mathf.Clamp01(value);
            _progressBarFill.style.width = new Length(_progress * 100, LengthUnit.Percent);
        }
    }

    public void SetProgress(float value)
    {
        Progress = value;
    }

    public new class UxmlFactory : UxmlFactory<StatProgress, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlFloatAttributeDescription progressAttribute = new UxmlFloatAttributeDescription { name = "progress", defaultValue = 0f };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var statProgress = ve as StatProgress;
            statProgress.Progress = progressAttribute.GetValueFromBag(bag, cc);
        }
    }
}
