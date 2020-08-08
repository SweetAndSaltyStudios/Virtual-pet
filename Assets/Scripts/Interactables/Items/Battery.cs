using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Battery : Item, IConsumable
    {
        public void Consume()
        {
            GameManager.Instance.Pet.AddEnergy(20);

            Instantiate(ResourceManager.Instance.EffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public override void OnInteract()
        {
            
        }

        protected override void OnDestoy()
        {
            Debug.Log(name + "OnDestoy");
        }
    }
}
