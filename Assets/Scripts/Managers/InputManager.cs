using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class InputManager : Singelton<InputManager>
    {
        private RaycastHit2D hitInfo;

        public Interactable CurrentInteractable
        {
            get;
            private set;
        }

        public EventSystem EventSystem
        {
            get
            {
                return EventSystem.current;
            }
        }

        public Vector2 MouseWorldPosition
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public Vector2 MouseScreenPosition
        {
            get
            {
                return Input.mousePosition;
            }
        }

        private float tapDelay = 0f;
        private readonly float doubleTapThreshold = 0.5f;
        private int tapCount = 0;

        public bool IsDoubleTap
        {
            get;
            private set;
        }

        private void Update()
        {
            if(CurrentInteractable == null)
            {
                SetCurrentInteractable(null);
                UIManager.Instance.ActivateDragLine(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                tapCount++;

                hitInfo = Physics2D.Raycast(MouseWorldPosition, Vector2.zero);

                if (hitInfo && hitInfo.collider.isTrigger == false)
                {               
                    SetCurrentInteractable(hitInfo.transform.GetComponent<Interactable>());
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetCurrentInteractable(null);
            }

            CheckIfDoubleTap();
        }

        private bool CheckIfDoubleTap()
        {          
            if(tapCount == 1 && tapDelay < doubleTapThreshold)
            {
                tapDelay += Time.deltaTime;
            }

            if(tapCount == 1 && tapDelay >= doubleTapThreshold)
            {
                tapDelay = 0;
                tapCount = 0;
            }

            if(tapCount == 2 && tapDelay < doubleTapThreshold)
            {
                tapCount = 0;
                return IsDoubleTap = true;
            }

            return IsDoubleTap = false;           
        }

        public void SetCurrentInteractable(Interactable interactable)
        {
            if (CurrentInteractable != null)
            {
                CurrentInteractable.OnTouchUp();
            }

            CurrentInteractable = interactable;

            if (CurrentInteractable != null)
            {
                interactable.OnTouchDown();
            }
        }
    }
}