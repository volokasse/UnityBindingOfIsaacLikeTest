using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public LivesManager livesManager;
    public UnitInfos unitInfos;
    public BulletFactory bulletFactory;

    public Rigidbody2D rgdBody;
    public SpriteRenderer spriteRenderer;

    private Enums.MovementDirection currentDirection  = Enums.MovementDirection.NONE;
    private Enums.MovementDirection blockedDirections = Enums.MovementDirection.NONE;

    private void Start()
    {

    }

    private void Update()
    {
        /// Movements
        if (Input.GetKey(KeyCode.S)) // bottom
        {
            currentDirection |= Enums.MovementDirection.BOTTOM;

            if ((blockedDirections & Enums.MovementDirection.BOTTOM) != 0)
                rgdBody.velocity = new Vector2(rgdBody.velocity.x, 0);
            else
                rgdBody.velocity = new Vector2(rgdBody.velocity.x, -unitInfos.getVitesse());
        }
        else if (Input.GetKey(KeyCode.Z)) // top
        {
            currentDirection |= Enums.MovementDirection.TOP;

            if ((blockedDirections & Enums.MovementDirection.TOP) != 0)
                rgdBody.velocity = new Vector2(rgdBody.velocity.x, 0);
            else
                rgdBody.velocity = new Vector2(rgdBody.velocity.x, unitInfos.getVitesse());
        }
        else if (rgdBody.velocity.y < 0.25f && rgdBody.velocity.y > -0.25f)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, 0);
        else if (rgdBody.velocity.y > 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, rgdBody.velocity.y - (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage));
        else if (rgdBody.velocity.y < 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, rgdBody.velocity.y + (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage));

        if (Input.GetKey(KeyCode.D)) // right
        {
            currentDirection |= Enums.MovementDirection.RIGHT;

            if ((blockedDirections & Enums.MovementDirection.RIGHT) != 0)
                rgdBody.velocity = new Vector2(0, rgdBody.velocity.y);
            else
                rgdBody.velocity = new Vector2(unitInfos.getVitesse(), rgdBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.Q)) // left
        {
            currentDirection |= Enums.MovementDirection.LEFT;

            if ((blockedDirections & Enums.MovementDirection.LEFT) != 0)
                rgdBody.velocity = new Vector2(0, rgdBody.velocity.y);
            else
                rgdBody.velocity = new Vector2(-unitInfos.getVitesse(), rgdBody.velocity.y);
        }
        else if (rgdBody.velocity.x < .25f && rgdBody.velocity.x > -.25f)
            rgdBody.velocity = new Vector2(0, rgdBody.velocity.y);
        else if (rgdBody.velocity.x > 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x - (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage), rgdBody.velocity.y);
        else if (rgdBody.velocity.x < 0)
            rgdBody.velocity = new Vector2(rgdBody.velocity.x + (float)(unitInfos.baseVitesse / unitInfos.coeffFreinage), rgdBody.velocity.y);

        /// Colors management
        if (unitInfos.vitesseModifier < 100f && unitInfos.vitesseModifier != 0f)
            spriteRenderer.color = Colors.slow;
        else
            spriteRenderer.color = Colors.normal;

        /// Shoot
        if (Input.GetKey(KeyCode.M)) // right
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "right");
        else if (Input.GetKey(KeyCode.K)) // left
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "left");
        else if (Input.GetKey(KeyCode.O)) // up
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "top");
        else if (Input.GetKey(KeyCode.L)) // bottom
            bulletFactory.Shoot(rgdBody, unitInfos, gameObject, "bottom");

        /// Check movements
        if (Input.GetKeyUp(KeyCode.S))
            currentDirection &= ~Enums.MovementDirection.BOTTOM;
        if (Input.GetKeyUp(KeyCode.Z))
            currentDirection &= ~Enums.MovementDirection.TOP;
        if (Input.GetKeyUp(KeyCode.Q))
            currentDirection &= ~Enums.MovementDirection.LEFT;
        if (Input.GetKeyUp(KeyCode.D))
            currentDirection &= ~Enums.MovementDirection.RIGHT;
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

    public void setCanMove(bool p_CanMove, Enums.MovementDirection p_Direction)
    {
        if (p_CanMove)
            blockedDirections &= ~p_Direction;
        else
            blockedDirections |= p_Direction;
    }
}