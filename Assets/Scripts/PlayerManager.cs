using UnityEngine;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    public LivesManager livesManager;
    public UnitInfos unitInfos;
    public BulletFactory bulletFactory;

    public Rigidbody2D rgdBody;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S)) // bottom
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, -unitInfos.vitesse);
        else if (Input.GetKey(KeyCode.Z)) // top
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, unitInfos.vitesse);
        else if (rgdBody.velocity.y < 0.25f && rgdBody.velocity.y > -0.25f)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, 0);
        else if (rgdBody.velocity.y > 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, rgdBody.velocity.y - (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage));
        else if (rgdBody.velocity.y < 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, rgdBody.velocity.y + (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage));

        if (Input.GetKey(KeyCode.D)) // right
            rgdBody.velocity = new Vector2(unitInfos.vitesse, rgdBody.velocity.y);
        else if (Input.GetKey(KeyCode.Q)) // left
            rgdBody.velocity = new Vector2(-unitInfos.vitesse, rgdBody.velocity.y);
        else if (rgdBody.velocity.x < .25f && rgdBody.velocity.x > -.25f)
            rgdBody.velocity = new Vector2(0, rgdBody.velocity.y);
        else if (rgdBody.velocity.x > 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x - (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage), rgdBody.velocity.y);
        else if (rgdBody.velocity.x < 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x + (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage), rgdBody.velocity.y);

        if (unitInfos.vitesseModifier < 100f && unitInfos.vitesseModifier != 0f)
            spriteRenderer.color = Colors.slow;
        else
            spriteRenderer.color = Colors.normal;

        if (Input.GetKey(KeyCode.M)) // right
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "right");
        else if (Input.GetKey(KeyCode.K)) // left
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "left");
        else if (Input.GetKey(KeyCode.O)) // up
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "top");
        else if (Input.GetKey(KeyCode.L)) // bottom
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "bottom");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            var l_UnitInfos = collision.GetComponent<UnitInfos>();
            if (l_UnitInfos != null)
            {
                for(var l_Itr = 0; l_Itr < l_UnitInfos.damage; l_Itr++)
                    livesManager.Hit();
            }
        }
        else if (collision.tag == "Collectible")
        {
            var l_CollectibleInfos = collision.GetComponent<CollectibleInfos>();
            switch(l_CollectibleInfos.CollectibleType)
            {
                case Enums.CollectibleType.HEAL:
                    livesManager.Heal();
                    break;
            }
        }
        else if (collision.tag == "Tile")
        {
            var l_TileInfos = collision.GetComponent<TileInfos>();
            switch(l_TileInfos.TileType)
            {
                case Enums.TilesType.SLOW:
                    unitInfos.ChangeSpeed(l_TileInfos.slowPercent);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            var l_TileInfos = collision.GetComponent<TileInfos>();
            switch (l_TileInfos.TileType)
            {
                case Enums.TilesType.SLOW:
                    unitInfos.ChangeSpeed(-l_TileInfos.slowPercent);
                    break;
            }
        }
    }
}