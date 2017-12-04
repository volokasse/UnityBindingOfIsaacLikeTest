using UnityEngine;
using System.Collections;

public class BulletFactory : MonoBehaviour
{
    public Bullet bulletPrefab;

    private float lastShoot = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Rigidbody2D p_RgdBody, UnitInfos p_UnitInfos, GameObject p_Player, string p_Direction)
    {
        if (lastShoot == 0f || (Time.time - lastShoot) > p_UnitInfos.timerProjectile)
        {
            var newBullet = Instantiate(bulletPrefab, p_Player.transform.position, p_Player.transform.rotation).gameObject;
            var bulletRgdBody = newBullet.GetComponent<Rigidbody2D>();

            var bulletVelocity = new Vector2(0f, 0f);
            switch (p_Direction)
            {
                case "right":
                    bulletVelocity.x = p_UnitInfos.projectileVitesse;
                    break;
                case "left":
                    bulletVelocity.x = -p_UnitInfos.projectileVitesse;
                    break;
                case "top":
                    bulletVelocity.y = p_UnitInfos.projectileVitesse;
                    break;
                case "bottom":
                    bulletVelocity.y = -p_UnitInfos.projectileVitesse;
                    break;
            }

            bulletRgdBody.velocity = bulletVelocity;
            Destroy(newBullet.gameObject, p_UnitInfos.timerProjectile);

            lastShoot = Time.time;
        }
    }
}
