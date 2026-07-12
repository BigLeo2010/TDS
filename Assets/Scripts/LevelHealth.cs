using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHealth : MonoBehaviour
{
    [HideInInspector] public float curHealth;
    [SerializeField] float maxHealth;
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    private void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        healthText.text = curHealth.ToString() + "/" + maxHealth.ToString();
        healthBar.fillAmount = curHealth / maxHealth;
    }
}
