using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class UI_PanelButton : UI_Button
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            ButtonEvent.Invoke();
        }
    }
}