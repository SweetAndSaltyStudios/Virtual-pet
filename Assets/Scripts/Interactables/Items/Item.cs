using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public abstract class Item : Interactable
    {
        public DragMode DragMode = DragMode.SNAP;
        public float DestroyMagnitude = 10f;

        protected Rigidbody2D rb2D;
        private SpringJoint2D springJoint2D;
        private readonly float dragSpeed = 200f;
        protected bool isBeingDragged;

        public bool UsingSpring
        {
            get
            {
                if(springJoint2D == null && DragMode != DragMode.SNAP)
                {
                    DragMode = DragMode.VELOCITY;
                }

                return DragMode == DragMode.SPRING && springJoint2D != null;
            }
        }

        protected virtual void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            springJoint2D = GetComponent<SpringJoint2D>();

            if(springJoint2D != null)
            {
                springJoint2D.enabled = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(rb2D.velocity.magnitude >= DestroyMagnitude)
            {
                OnDestoy();
            }
        }

        private void FixedUpdate()
        {
            if(isBeingDragged)
            {
                OnDrag();
            }
        }

        private void LateUpdate()
        {
            if(isBeingDragged)
            {
                if(UsingSpring)
                {
                    UIManager.Instance.UpdateDragLine(ConvertPositionFromWorldToLocal(springJoint2D.anchor, false), springJoint2D.connectedAnchor);
                }
                else
                {
                    UIManager.Instance.UpdateDragLine(transform.position, InputManager.Instance.MouseWorldPosition);
                }
            }
        }

        private Vector2 ConvertPositionFromWorldToLocal(Vector2 position, bool useInverse)
        {
            if(useInverse)
            {
                return transform.InverseTransformPoint(position);
            }

            return transform.TransformPoint(position);
        }

        protected virtual void OnDestoy()
        {
            Debug.Log(name + "OnDestoy");
            Instantiate(ResourceManager.Instance.EffectPrefab, transform.position, Quaternion.identity);
        }

        public override void OnTouchDown()
        {
            rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            if(UsingSpring)
            {
                springJoint2D.anchor = ConvertPositionFromWorldToLocal(InputManager.Instance.MouseWorldPosition, true);
                springJoint2D.connectedAnchor = InputManager.Instance.MouseWorldPosition;
                springJoint2D.enabled = true;

                UIManager.Instance.ActivateDragLine(true, ConvertPositionFromWorldToLocal(springJoint2D.anchor, true), springJoint2D.connectedAnchor);
            }
            else
            {
                rb2D.gravityScale = 0;
                UIManager.Instance.ActivateDragLine(true, transform.position, InputManager.Instance.MouseWorldPosition);
            }

            isBeingDragged = true;
        }

        public override void OnDrag()
        {
            if(DragMode == DragMode.SNAP)
            {
                rb2D.transform.position = InputManager.Instance.MouseWorldPosition;
                return;
            }

            if(UsingSpring)
            {
                if(springJoint2D != null && springJoint2D.enabled)
                {
                    springJoint2D.connectedAnchor = InputManager.Instance.MouseWorldPosition;
                }

                return;
            }

            var distance = InputManager.Instance.MouseWorldPosition - (Vector2)transform.position;
            rb2D.velocity = distance * dragSpeed * Time.deltaTime;
        }

        public override void OnTouchUp()
        {
            if(UsingSpring)
            {
                springJoint2D.enabled = false;
            }
            else
            {
                rb2D.gravityScale = 1;
            }

            // !!!
            rb2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

            isBeingDragged = false;
        }
    }
}