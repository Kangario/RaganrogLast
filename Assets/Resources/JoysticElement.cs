using UnityEngine;
using UnityEngine.UIElements;

public class JoystickElement : VisualElement
{
    public new class UxmlFactory : UxmlFactory<JoystickElement, UxmlTraits> { }

    private VisualElement handle;
    private Vector3 startPosition;
    private Vector3 direction;
    private Vector2 offset;

    public Vector2 Direction => direction;
    private GameObject _object;

    private bool isClick;

    public JoystickElement()
    {
        style.width = 100;
        style.height = 100;

        handle = new VisualElement();
        handle.style.width = 50;
        handle.style.height = 50;
        handle.AddToClassList("handler-style");
        Add(handle); 
        direction = Vector2.zero;
        offset = Vector2.zero;
    }

    public void SetOffset(Vector2 newOffset)
    {
        offset = newOffset;
    }

    public void OnPointerDown(PointerDownEvent evt)
    {
        startPosition = this.WorldToLocal(evt.position);
        isClick = true;
        handle.style.left = style.width.value.value / 2 - handle.resolvedStyle.width / 2 + offset.x;
        handle.style.top = style.width.value.value / 2 - handle.resolvedStyle.width / 2 + offset.y;
    }

    public void OnPointerMove(PointerMoveEvent evt)
    {
        if (isClick)
        {
            Vector2 currentPos = this.WorldToLocal(evt.position); 
            Vector2 delta = currentPos - (Vector2)startPosition;
            float radius = style.width.value.value;

            Vector2 clampedDelta = Vector2.ClampMagnitude(delta, radius);
            handle.style.left = clampedDelta.x + style.width.value.value / 2 - handle.resolvedStyle.width / 2 + offset.x;
            handle.style.top = clampedDelta.y + style.height.value.value / 2 - handle.resolvedStyle.height / 2 + offset.y;

            direction = clampedDelta / radius; 
            direction.y = -direction.y;
            if (_object.transform.tag == "Controller_Attack")
            {
                Charactres_Events.AttackDragJoystick();
            }
        }
    }

    public void SetObject(GameObject gameObject)
    {
        _object = gameObject;
    }

    public void OnPointerUp(PointerUpEvent evt)
    {
        isClick = false;
        handle.style.left = style.width.value.value / 2 - handle.resolvedStyle.width / 2 + offset.x;
        handle.style.top = style.height.value.value / 2 - handle.resolvedStyle.height / 2 + offset.y;

        direction = Vector2.zero;
        if (_object.transform.tag == "Controller_Attack")
        {
            Charactres_Events.StopAttackDragJoystick();
        }
    }

}
