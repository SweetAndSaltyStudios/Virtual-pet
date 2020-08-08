using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class PetEngine : Interactable
    {
        #region VARIABLES

        [Header("References")]
        [SerializeField] private SpriteRenderer faceSpriteRenderer = null;
        [SerializeField] private SpriteRenderer bodySpriteRenderer = null;

        private const float MAX_ENERGY = 100;
        private const float MAX_HAPPINESS = 100;

        private float currentEnergy;
        private float currentHappiness;

        public bool IsSpriteMaskInteractionVisible
        {
            get;
            private set;
        }

        #endregion VARIABLES

        #region UNITY_FUNCTIONS

        private void Start()
        {
            UpdateSpriteMaskInteraction(SpriteMaskInteraction.None);

            LoadStats();
        }

        private void OnDestroy()
        {
            SaveStats();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS 

        public void UpdateSpriteMaskInteraction(SpriteMaskInteraction spriteMaskInteraction)
        {
            bodySpriteRenderer.maskInteraction = spriteMaskInteraction;
            faceSpriteRenderer.maskInteraction = spriteMaskInteraction;
            IsSpriteMaskInteractionVisible = spriteMaskInteraction == SpriteMaskInteraction.VisibleInsideMask;
        }

        public override void OnTouchDown()
        {
            OnInteract();
        }

        public override void OnInteract()
        {
            AddHappiness(10);       
        }

        public override void OnDrag()
        {
            
        }

        public override void OnTouchUp()
        {

        }

        private void SetNeutralMotion()
        {
            faceSpriteRenderer.sprite = ResourceManager.Instance.GetSprite("Neutral");
        }

        public void AddEnergy(float value)
        {
            currentEnergy += value;

            if(currentEnergy > MAX_ENERGY)
            {
                currentEnergy = MAX_ENERGY;
            }

            if(currentEnergy <= 0)
            {
                Debug.Log("Energy is 0 or lower...");
                currentEnergy = 0;
            }

            // UIManager.Instance.StatsPanel.UpdateEnergyValueText = currentEnergy;
        }

        public void AddHappiness(float value)
        {
            currentHappiness += value;

            if(currentHappiness > MAX_ENERGY)
            {
                currentHappiness = MAX_ENERGY;
            }

            if(currentHappiness <= 0)
            {
                Debug.Log("Happiness is 0 or lower...");
                currentHappiness = 0;
            }

            // UIManager.Instance.StatsPanel.UpdateHappinessValueText = currentHappiness;
        }

        private void LoadStats()
        {
            var sessionTimeDifference = GameMaster.Instance.GetTimeBetweenSessions;

            var energy = PlayerPrefs.GetFloat("Energy", MAX_ENERGY) - (float)sessionTimeDifference.TotalHours * 2;
            AddEnergy(energy);

            var happiness = PlayerPrefs.GetFloat("Happiness", MAX_HAPPINESS) - (float)((100 - currentEnergy) * (sessionTimeDifference.TotalHours / 5));
            AddHappiness(happiness);
        }

        private void SaveStats()
        {
            PlayerPrefs.SetFloat("Energy", currentEnergy);
            PlayerPrefs.SetFloat("Happiness", currentHappiness);
        }   

        #endregion CUSTOM_FUNCTIONS
    }
}