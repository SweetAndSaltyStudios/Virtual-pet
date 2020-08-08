using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void OnTouchDown();
        public abstract void OnDrag();
        public abstract void OnTouchUp();

        public abstract void OnInteract();
    }
}
