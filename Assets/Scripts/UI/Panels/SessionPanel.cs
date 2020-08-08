using TMPro;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class SessionPanel : UI_Panel
    {
        [Header("References")]
        public TextMeshProUGUI CurrentSessionTimeText;
        public TextMeshProUGUI LastSessionTimeText;
        public TextMeshProUGUI TimeBetweensessionsText;

        private void Start()
        {
            UpdateSessions();
        }

        public void UpdateSessions()
        {
            CurrentSessionTimeText.text = GameMaster.Instance.GetCurrentSessionTime.ToString();
            LastSessionTimeText.text = GameMaster.Instance.GetLastSessionTime.ToString();

            TimeBetweensessionsText.text = GameMaster.Instance.GetTimeBetweenSessions.ToString();
        }
    }
}