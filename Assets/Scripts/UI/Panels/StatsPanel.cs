using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class StatsPanel : UI_Panel
    {
        [Header("References")]
        public TextMeshProUGUI EnergyValueText;
        public TextMeshProUGUI HappinessValueText;
        public Image EnergyBarFillImage;
        public Image HappinessBarFillImage;

        public float UpdateEnergyValueText
        {
            set
            {
                EnergyValueText.text = value.ToString();
                EnergyBarFillImage.fillAmount = value * 0.01f;
            }
        }

        public float UpdateHappinessValueText
        {         
            set 
            {
                HappinessValueText.text = value.ToString();
                HappinessBarFillImage.fillAmount = value * 0.01f;
            }
        }
    }
}