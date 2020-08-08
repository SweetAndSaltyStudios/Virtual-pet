using System.Collections;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Effect : MonoBehaviour
    {
        private ParticleSystem effectParticleSystem;

        private void Awake()
        {
            effectParticleSystem = GetComponentInChildren<ParticleSystem>();
        }

        private void OnEnable()
        {
            StopCoroutine(ILifeTime());
            StartCoroutine(ILifeTime());
        }

        private void OnDisable()
        {
            ObjectPoolManager.Instance.Despawn(gameObject);
        }

        private IEnumerator ILifeTime()
        {
            yield return new WaitUntil(() => effectParticleSystem.isPlaying == false);

            gameObject.SetActive(false);
        }
    }
}