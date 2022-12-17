using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;
using UnityEngine.UI;

public class EntityUI : MonoBehaviour
{
    [SerializeField]
    Damageable _damageable = null;

    [SerializeField]
    Image _healthBarImage = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        _damageable.DamageTaken += UpdateUiHealthBar;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void UpdateUiHealthBar(Damageable target , int currentHealth, int maxHealth)
    {
        _healthBarImage.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
