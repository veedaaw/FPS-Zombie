using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    PlayerStat playerStat;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = PlayerManager.Instance.player.GetComponent<PlayerStat>();
        healthSlider.maxValue = playerStat.maxHealth.GetValue();
        healthSlider.minValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerStat.currentHealth;
    }
}
