using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class ResourceManager : Singelton<ResourceManager>
    {
        [Header("Prefabs")]
        public GameObject PetPrefab;
        public GameObject EffectPrefab;

        [Header("Motion sprites")]
        public Sprite[] MotionSprites;

        public Sprite GetSprite(string spriteName)
        {
            for(int i = 0; i < MotionSprites.Length; i++)
            {
                if(MotionSprites[i].name == spriteName)
                {
                    return MotionSprites[i];
                }
            }

            Debug.LogError(spriteName + " not found!");
            return Sprite.Create(new Texture2D(10, 10), new Rect(), new Vector2(0.5f, 0.5f));
        }

        public Sprite GetRandomSprite()
        {
            return MotionSprites[Random.Range(0, MotionSprites.Length)];
        }
    }
}

