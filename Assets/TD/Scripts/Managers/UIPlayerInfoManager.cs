//Code by Néo Grapin

namespace GSGD1
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class UIPlayerInfoManager : MonoBehaviour
    {
    #region CF Gabriel Groleas
        [SerializeField]
        private SpawnerManager _spawnerManager = null;

        [SerializeField]
        private TextMeshProUGUI _numberOfWaveText = null;


        private void UpdateNumberOfWaves()
        {
            int waveCount = _spawnerManager._numberOfWaves;
            _numberOfWaveText.text = waveCount.ToString();
        }
        #endregion

        [SerializeField]
        private ThunasseManager _thunasseManager = null;

        [SerializeField]
        private TextMeshProUGUI _moneyText = null;

        private void UpdateMoney()
        {
            int moneyCount = _thunasseManager._currentMoney;
            _moneyText.text = moneyCount.ToString();
        }





        private void Update()
        {
            UpdateNumberOfWaves();
            UpdateMoney();
        }

    }
}
