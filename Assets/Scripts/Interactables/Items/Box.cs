using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Box : Item
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