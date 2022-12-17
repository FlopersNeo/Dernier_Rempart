//Code by Néo Grapin

namespace GSGD1
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class UIManager : Singleton<UIManager>
    {
        #region Variables
        [SerializeField]
        private GameObject _optionMenu = null;

        [SerializeField]
        private GameObject _pauseMenu = null;

        [SerializeField]
        private GameObject _controlsMenu = null;

        [SerializeField]
        private GameObject _inGameUI = null;

        [SerializeField]
        private GameObject _defeatUI = null;

        [SerializeField]
        private GameObject _victoryUI = null;

        [SerializeField]
        private Slider _brightnessSlider = null;

        [SerializeField]
        private Image _brightnessImage = null;

        #endregion
        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("esc");
                if (_pauseMenu.activeSelf == false)
                {   
                    _pauseMenu.SetActive(true);
                }
                else if(_pauseMenu.activeSelf == true)
                {
                    _pauseMenu.SetActive(false);
                }
                if(_optionMenu.activeSelf == true)
                {
                    _optionMenu.SetActive(false);
                    _pauseMenu.SetActive(true);
                }
                if(_controlsMenu.activeSelf == true)
                {
                    _controlsMenu.SetActive(false);
                    _optionMenu.SetActive(true);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                       
                        PrintName(hit.transform.gameObject);
                    }
                }
            }

        }
        #region Brightness Parameter


        public void BrightnessUpdate()
        {
            _brightnessImage.color = new Color(0, 0, 0, _brightnessSlider.value);
        }
        #endregion
        #region Menu Pause
        public void Resume()
        {
            _pauseMenu.SetActive(false);
        }

        public void Option()
        {
            _pauseMenu.SetActive(false);
            _optionMenu.SetActive(true);
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        #endregion
        #region Menu Option du menu pause
        public void Controls()
        {
            _optionMenu.SetActive(false);
            _controlsMenu.SetActive(true);
        }

        public void BackToPause()
        {
            _optionMenu.SetActive(false);
            _pauseMenu.SetActive(true);
        }
        #endregion
        #region Menu Control du Menu Option
        public void BackToOption()
        {
            _controlsMenu.SetActive(false);
            _optionMenu.SetActive(true);
        }
        #endregion
        #region Upgrade/Sell 
        private void Upgrade()
        {

        }

        private void Sell()
        {
            
        }
        #endregion

        private void PrintName(GameObject go)
        {
            print(go.name);
        }

        public void DefeatUi()
        {
            _inGameUI.SetActive(false);
            _defeatUI.SetActive(true);
        }
    }
}