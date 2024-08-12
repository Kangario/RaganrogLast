using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace RController
{
    public class Joystic : UIToolKitConnectable
    {
        [SerializeField] private string Joystick = "Joystick-Left";
        [SerializeField] private Vector2 _offset;
        private JoystickElement _joystickElement;
        
        private void Awake() => Connect();

        private void Start()
        {
            _joystickElement = _rootElement.Q<JoystickElement>(Joystick);

            _joystickElement.RegisterCallback<PointerDownEvent>(_joystickElement.OnPointerDown);
            _rootElement.RegisterCallback<PointerUpEvent>(_joystickElement.OnPointerUp);
            _rootElement.RegisterCallback<PointerMoveEvent>(_joystickElement.OnPointerMove);
            _joystickElement.SetObject(gameObject);
            _joystickElement.SetOffset(_offset);
        }

        public Vector2 GetInputDirection()
        {
            return _joystickElement.Direction;
        }
    }
}