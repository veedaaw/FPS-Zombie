using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** combat system for all the entities in the game **/
public class CharacterCombat : MonoBehaviour
{
    public float attackRate = 1f;
    private float attackCountdown = 0f;

    PlayerStat myStats;
    public event System.Action OnAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<PlayerStat>();
    }

    void Update()
    {
        attackCountdown -= Time.deltaTime;
    }

    public void Attack(PlayerStat target)
    {
        if (attackCountdown <= 0f)
        {
            attackCountdown = 1f / attackRate;

            target.TakeDamage(myStats.damage.GetValue());
            OnAttack?.Invoke();
        }
    }
}
