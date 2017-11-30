using UnityEngine;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    public LivesManager livesManager;
    public UnitInfos unitInfos;

    public Rigidbody2D rgdBody;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey("down"))
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, -unitInfos.vitesse);
        else if (Input.GetKey("up"))
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, unitInfos.vitesse);
        else if (rgdBody.velocity.y < 0.25f && rgdBody.velocity.y > -0.25f)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, 0);
        else if (rgdBody.velocity.y > 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, rgdBody.velocity.y - (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage));
        else if (rgdBody.velocity.y < 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, rgdBody.velocity.y + (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage));

        if (Input.GetKey("right"))
            rgdBody.velocity = new Vector2(unitInfos.vitesse, rgdBody.velocity.y);
        else if (Input.GetKey("left"))
            rgdBody.velocity = new Vector2(-unitInfos.vitesse, rgdBody.velocity.y);
        else if (rgdBody.velocity.x < .25f && rgdBody.velocity.x > -.25f)
            rgdBody.velocity = new Vector2(0, rgdBody.velocity.y);
        else if (rgdBody.velocity.x > 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x - (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage), rgdBody.velocity.y);
        else if (rgdBody.velocity.x < 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x + (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage), rgdBody.velocity.y);
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
                    unitInfos.ChangeSPeed(l_TileInfos.slowPercent);
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
                    unitInfos.ChangeSPeed(-l_TileInfos.slowPercent);
                    break;
            }
        }
    }
}