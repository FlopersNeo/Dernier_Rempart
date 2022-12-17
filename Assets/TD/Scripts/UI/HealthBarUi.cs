//Code by Néo Grapin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField]
    private Image _healthBarImage = null;

    [SerializeField]
    private TextMeshProUGUI _healthAmountText = null;

    private void OnEnable()
    {
        BaseManager.Instance.OnBasesHealthChanged -= UpdateBasesHealthUi;
        BaseManager.Instance.OnBasesHealthChanged += UpdateBasesHealthUi;
    }

    private void OnDisable()
    {
        BaseManager.Instance.OnBasesHealthChanged -= UpdateBasesHealthUi;
    }
    private void Start()
    {
        _healthAmountText.text = BaseManager.Instance.totalBasesMaxHealth.ToString();
    }

    private void UpdateBasesHealthUi(int currentHealth)
    {
        _healthBarImage.fillAmount = (float)currentHealth / BaseManager.Instance.totalBasesMaxHealth;
        _healthAmountText.text = currentHealth.ToString();
    }

}
