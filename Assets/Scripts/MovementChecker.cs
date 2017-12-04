using UnityEngine;
using System.Collections;

public class MovementChecker : MonoBehaviour
{
    private PlayerManager player;
    public Enums.MovementDirection directionCheck;

    private void Awake()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blocking")
            player.setCanMove(false, directionCheck);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Blocking")
            player.setCanMove(true, directionCheck);
    }
}
