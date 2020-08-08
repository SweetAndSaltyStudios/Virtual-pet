using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class UI_ToggleButton : UI_Button
    {
        [Header("Toggle Sprites")]
        public Sprite ToggleOn;
        public Sprite ToggleOff;
        public Image Icon;

        public override void OnPointerUp(PointerEventData eventData)
        {
            Icon.sprite = Icon.sprite == ToggleOn ? ToggleOff : ToggleOn;

            base.OnPointerUp(eventData);

            ButtonEvent.Invoke();
        }
    }
}
