using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RInventory
{
    public class InventoryMover : MonoBehaviour
    {
        private Button _selectedSlot;
        public void OnSlotUp(Button selectedSlot)
        {
            //_selectedSlot = selectedSlot;
            //_selectedSlot = null;
        }

        public void OnSlotDown( )
        {
            //_selectedSlot = null;
            //_selectedSlot = selectedSlot;
            //VisualElement tempElement = new VisualElement();

           // parentElement.Add(tempElement);

        }

        public void SlotMove(MouseMoveEvent evt)
        {
            if (_selectedSlot != null)
            {
                Vector2 localPosition = evt.mousePosition;
                Debug.LogWarning(localPosition);
                float centerX = _selectedSlot.resolvedStyle.width / 2;
                float centerY = _selectedSlot.resolvedStyle.height / 2;

                _selectedSlot.style.top = localPosition.y - centerY;
                _selectedSlot.style.left = localPosition.x - centerX;
            }
        }
    }
}