using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace RController
{
    public class Joystic : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler , IJoystic
    {
        [SerializeField] private Image joystickBackground;
        [SerializeField] private Image touch_Element;

        public Vector2 inputDirection = Vector2.zero;
        public void OnPointerDown(PointerEventData eventData)
        {

            OnDrag(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            inputDirection = Vector2.zero;
            touch_Element.rectTransform.anchoredPosition = Vector2.zero;
            if (gameObject.transform.tag == "Controller_Attack")
            {
                Charactres_Events.StopAttackDragJoystick();
            }

        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickBackground.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position))
            {
                position.x = (position.x / joystickBackground.rectTransform.sizeDelta.x);
                position.y = (position.y / joystickBackground.rectTransform.sizeDelta.y);

                inputDirection = new Vector2(position.x * 2, position.y * 2);
                inputDirection = (inputDirection.magnitude > 1.0f) ? inputDirection.normalized : inputDirection;

                touch_Element.rectTransform.anchoredPosition =
                    new Vector2(inputDirection.x * (joystickBackground.rectTransform.sizeDelta.x / 3),
                                inputDirection.y * (joystickBackground.rectTransform.sizeDelta.y / 3));
            }
            if (gameObject.transform.tag == "Controller_Attack")
            {
                Charactres_Events.AttackDragJoystick();
            }
        }

        public Vector2 GetInputDirection()
        {
            return inputDirection;
        }
    }
}