using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public abstract class UI_Panel : UI_Element
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES


        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            HandlePositionInHierarchy();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void HandlePositionInHierarchy()
        {
            transform.SetAsLastSibling();
        }

        public void Open()
        {
           
        }

        public void Close()
        {
           
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            HandlePositionInHierarchy();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            transform.position += (Vector3)eventData.delta;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
