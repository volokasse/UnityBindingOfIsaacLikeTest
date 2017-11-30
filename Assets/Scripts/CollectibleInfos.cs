using UnityEngine;
using System.Collections;

public class CollectibleInfos : MonoBehaviour
{

    public Enums.CollectibleType CollectibleType = Enums.CollectibleType.HEAL;

    public int value = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
