using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public ProjectileInfos projectileInfos;
    public Rigidbody2D rgdBody;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
            Destroy(this);
    }
}
