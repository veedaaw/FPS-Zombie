using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public float range = 3f;
    GameObject player;
    PlayerStat playerStat;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.player;
        playerStat = player.GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        if(player.transform.CompareTag("Player"))
        { 
         if (item.isInteractable && item.isDestructable)
         {
            if (item.itemType == Item.Type.HEALTH)
            {
                playerStat.Heal(item.value);
                Destroy(this.gameObject);
            }

            else
            {
                Inventory.Instance.addToInventory(item);
                Destroy(this.gameObject);
            }
           
         }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
