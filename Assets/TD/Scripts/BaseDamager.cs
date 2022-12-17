using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamager : MonoBehaviour
{
    [SerializeField]
    private int _damageToBase = 0;

    public int damageToBase { get { return _damageToBase; } }
}
