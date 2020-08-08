using System.Collections.Generic;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class MagnifyGlass : Item
    {
        private Collider2D handleCollider2D;
        private List<Collider2D> overlapingColliders = new List<Collider2D>();
        private ContactFilter2D contactFilter2D = new ContactFilter2D();

        protected override void Awake()
        {
            base.Awake();

            handleCollider2D = GetComponent<Collider2D>();
        }

        public override void OnTouchDown()
        {
            if(DragMode == DragMode.SNAP)
            {
                handleCollider2D.isTrigger = true;
                isBeingDragged = true;

                return;
            }
        }

        public override void OnTouchUp()
        {
            handleCollider2D.isTrigger = false;
            isBeingDragged = false;

            handleCollider2D.OverlapCollider(contactFilter2D, overlapingColliders);

            if(overlapingColliders.Count > 0)
            {
                Destroy(gameObject);
            }
        }

        public override void OnInteract()
        {
            
        }
    }
}
