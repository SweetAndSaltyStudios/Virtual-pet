using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Ball : Item, IUseSpring
    {
        public override void OnInteract()
        {
            OnDestoy();
        }

        public void OnSpringRelease()
        {
            
        }

        protected override void OnDestoy()
        {
            base.OnDestoy();
            Destroy(gameObject);
        }
    }
}
