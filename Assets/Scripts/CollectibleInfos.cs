using UnityEngine;
using System.Collections;

public class CollectibleInfos : MonoBehaviour
{

    public Enums.CollectibleType CollectibleType = Enums.CollectibleType.HEAL;

    public bool destroy      = true;
    public float pickupDelay = 3f;
    public float lastPickup  = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerManager = collision.gameObject.GetComponent<UnitInfos>();
        var bonusInfos  = gameObject.GetComponent<Bonus>();
        playerManager.collectibles.Add((uint)playerManager.collectibles.Count + 1, bonusInfos);

        if (destroy)
            Destroy(gameObject);
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            lastPickup = Time.time;
        }
    }

    private void Update()
    {
        if (!destroy && lastPickup != 0f)
        {
            if ((Time.time - lastPickup) > pickupDelay)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                lastPickup = 0f;
            }
        }
    }
}
