using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public abstract class UI_Button : UI_Element
    {
        [Header("Events")]
        public UnityEvent ButtonEvent;

        [Header("Sprites")]
        public Sprite ButtonNormal;
        public Sprite ButtonHighlighted;
        public Sprite ButtonPressed;

        private Image ButtonBackground;

        private void Awake()
        {
            ButtonBackground = GetComponentInChildren<Image>();
        }

        private void Start()
        {
            ButtonBackground.sprite = ButtonNormal;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            ButtonBackground.sprite = ButtonHighlighted;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            ButtonBackground.sprite = ButtonPressed;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            ButtonBackground.sprite = ButtonNormal;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            ButtonBackground.sprite = ButtonNormal;
        }
    }
}
