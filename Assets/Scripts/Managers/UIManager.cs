using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Sweet_And_Salty_Studios
{
    public class UIManager : Singelton<UIManager>
    {
        #region VARIABLES

        [Header("References")]
        public UI_Screen DefaultUIScreen;
        public TextMeshProUGUI DebugText;

        [Header("Events")]
        public UnityEvent OnScreenSwitched;

        private Canvas HUDCanvas;
        private DragLine dragLine;

        private UI_Screen currentUIScreen;
        private UI_Screen previousUIScreen;

        private Transform panelContainer;
        private Transform screenContainer;

        #endregion

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            SetStartingUILayout();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void Initialize()
        {
            HUDCanvas = GetComponentInChildren<Canvas>();
            panelContainer = HUDCanvas.transform.GetChild(0);
            screenContainer = HUDCanvas.transform.GetChild(1);

            dragLine = transform.GetComponentInChildren<DragLine>(true);
        }

        private void SetStartingUILayout()
        {         
            Invoke("SetCanvasShaderChannels", 2);
            panelContainer.gameObject.SetActive(true);
            screenContainer.gameObject.SetActive(true);

            SwitchUIScreen(DefaultUIScreen);
        }

        public void SwitchUIScreen(UI_Screen newUIScreen)
        {
            if(newUIScreen)
            {
                if(currentUIScreen)
                {
                    currentUIScreen.Close();
                    previousUIScreen = currentUIScreen;
                }

                currentUIScreen = newUIScreen;
                currentUIScreen.gameObject.SetActive(true);
                currentUIScreen.Open();

                OnScreenSwitched.Invoke();
            }
        }

        private void SwitchPreviousScreen()
        {
            SwitchUIScreen(previousUIScreen);
        }

        private void SetCanvasShaderChannels()
        {
            HUDCanvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
        }

        public void ActivateDragLine(bool isActive = false, Vector2 startPosition = new Vector2(), Vector2 endPosition = new Vector2())
        {
            dragLine.SetPosition(0, startPosition);
            dragLine.SetPosition(1, endPosition);

            dragLine.gameObject.SetActive(isActive);
        }

        public void UpdateDragLine(Vector2 startPosition, Vector3 endPosition)
        {
            dragLine.SetPosition(0, startPosition);
            dragLine.SetPosition(1, endPosition);           
        }

        public void UpdateDebugText(string newText)
        {
            DebugText.text = newText;
        }

        public void PauseButton()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        public void ExitButton()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }   

        #endregion CUSTOM_FUNCTIONS
    }
}
