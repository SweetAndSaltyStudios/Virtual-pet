using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class UI_DragUIElementButton : UI_Button
    {
        private UI_Element ui_Element;

        public void MoveUIElementToMousePosition(UI_Element ui_Element)
        {
            this.ui_Element = ui_Element;

            ui_Element.gameObject.SetActive(false);
            ui_Element.transform.position = InputManager.Instance.MouseScreenPosition;
            ui_Element.gameObject.SetActive(true);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            ButtonEvent.Invoke();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            ButtonEvent.Invoke();

        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            ui_Element.transform.position += (Vector3)eventData.delta;

        }
    }
}