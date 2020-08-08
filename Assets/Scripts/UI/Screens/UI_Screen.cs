using UnityEngine;
using UnityEngine.Events;

namespace Sweet_And_Salty_Studios
{
    public abstract class UI_Screen : UI_Element
    {
        [Header("Events")]
        [Space]
        public UnityEvent OnScreenOpen;
        public UnityEvent OnScreenClose;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);

            OnScreenOpen.Invoke();
        }

        public virtual void Close()
        {
            OnScreenClose.Invoke();

            gameObject.SetActive(false);
        }
    }
}