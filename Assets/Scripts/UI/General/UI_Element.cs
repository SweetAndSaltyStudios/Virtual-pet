using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public abstract class UI_Element : MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerDownHandler, 
        IPointerExitHandler,
        IPointerUpHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        ICancelHandler,
        IDeselectHandler
    {
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // Debug.Log(name + "OnPointerDown");
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log(name + "OnPointerEnter");
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log(name + "OnPointerExit");
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            // Debug.Log(name + "OnPointerUp");
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            // Debug.Log(name + "OnBeginDrag");
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            // Debug.Log(name + "OnDrag");
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            // Debug.Log(name + "OnEndDrag");
        }

        public virtual void OnCancel(BaseEventData eventData)
        {
            // Debug.Log(name + "OnCancel");
        }

        public virtual void OnDeselect(BaseEventData eventData)
        {
            // Debug.Log(name + "OnDeselect");
        }
    }
}
