using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Triangle : Item
    {
        public override void OnInteract()
        {
            
        }

        protected override void OnDestoy()
        {
            Debug.Log(name + "OnDestoy");
        }
    }
}
