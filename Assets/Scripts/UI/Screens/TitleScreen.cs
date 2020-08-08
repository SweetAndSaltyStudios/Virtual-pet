using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class TitleScreen : UI_Screen
    {
        public UnityEvent OnPointerDownEvent;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            OnPointerDownEvent.Invoke();
        }
    }
}
