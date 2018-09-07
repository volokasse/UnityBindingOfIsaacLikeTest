using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    public GameObject[,] map;
    public int GridSizeX;
    public int GridSizeY;

    public float roomSizeX = 2.24f;
    public float roomSizeY = 1.6f;
    public float roomScale = 108;

    public float posZeroX;
    public float posZeroY;

    public GameObject[] OneDoorRooms;
    public GameObject[] TwoDoorsRooms;
    public GameObject[] ThreeDoorsRooms;
    public GameObject[] FourDoorsRooms;

    public GameObject testObject;

    public List<KeyValuePair<int, GameObject>> prefabRooms = new List<KeyValuePair<int, GameObject>>();

    private RectTransform rectTransform;
    private Canvas canvas;

    // Use this for initialization
    void Awake()
    {
        map           = new GameObject[GridSizeX, GridSizeY];
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvas        = gameObject.GetComponent<Canvas>();

        /// Merge all rooms into one list of key value pairs
        foreach (var room in OneDoorRooms)
            prefabRooms.Add(new KeyValuePair<int, GameObject>(1, room));
        foreach(var room in TwoDoorsRooms)
            prefabRooms.Add(new KeyValuePair<int, GameObject>(2, room));
        foreach(var room in ThreeDoorsRooms)
            prefabRooms.Add(new KeyValuePair<int, GameObject>(3, room));
        foreach (var room in FourDoorsRooms)
            prefabRooms.Add(new KeyValuePair<int, GameObject>(4, room));

        /// init map
        for(var l_ItrX = 0; l_ItrX < GridSizeX; l_ItrX++)
        {
            for(var l_ItrY = 0; l_ItrY < GridSizeY; l_ItrY++)
            {
                map[l_ItrX, l_ItrY] = null;
            }
        }

        posZeroX -= (rectTransform.rect.width * canvas.scaleFactor) / 2 - (roomSizeX * 108);
        posZeroY += (rectTransform.rect.height * canvas.scaleFactor) / 2 - (roomSizeY * 108);
    }

    public IEnumerator generateTest()
    {
        for (var l_ItrX = 0; l_ItrX < GridSizeX; l_ItrX++)
        {
            for (var l_ItrY = 0; l_ItrY < GridSizeY; l_ItrY++)
            {
                var mapRoom = map[l_ItrX, l_ItrY];
                if (mapRoom != null)
                {
                    float posX = (posZeroX + ((float)l_ItrX * roomScale * roomSizeX)) / roomScale;
                    float posY = (posZeroY - ((float)l_ItrY * roomScale * roomSizeY)) / roomScale;
                    var position = new Vector3(posX, posY, 0);

                    Instantiate(mapRoom, position, transform.rotation, transform);
                }
                yield return new WaitForSeconds(.5f);
            }
        }
    }

    void Start()
    {
        //generateMap();
        StartCoroutine("generateTest");
    }

    public void generateMap()
    {
        for(var l_ItrX = 0; l_ItrX < GridSizeX; l_ItrX++)
        {
            for(var l_ItrY = 0; l_ItrY < GridSizeY; l_ItrY++)
            {
                var position = new Vector3((posZeroX + ((float)l_ItrX * 108 * roomSizeX)) / 108, (posZeroY + ((float)l_ItrY * 108 * roomSizeY)) / 108, 0);
                // RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, input, camera, out output);
                //var finalPosition = new Vector3(1, 1, 0);
                //RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, position, canvas.GetComponent<Camera>(), out finalPosition);
                Debug.Log("x : " + position.x + ", y : " + position.y);
                Instantiate(testObject, position, transform.rotation, transform);
                //Instantiate()
            }
        }
    }

    private List<KeyValuePair<int, GameObject>> getRoomsByDoorsCount(int p_Doors)
    {
        return (from room in prefabRooms where room.Key == p_Doors select room).ToList();
    }

    private List<KeyValuePair<int, GameObject>> getCanConnectRooms(Enums.DoorsPosition p_MustConnectPos)
    {
        var canConnectRooms = new List<KeyValuePair<int, GameObject>>();
        foreach(var room in prefabRooms)
        {
            var roomInfos = room.Value.GetComponent<RoomInfos>();
            var toAdd = false;
            if (Utils.hasFlag((int)p_MustConnectPos, (int)Enums.DoorsPosition.T) && Utils.hasFlag((int)roomInfos.doorsPos, (int)Enums.DoorsPosition.B))
                toAdd = true;
            else if (Utils.hasFlag((int)p_MustConnectPos, (int)Enums.DoorsPosition.R) && Utils.hasFlag((int)roomInfos.doorsPos, (int)Enums.DoorsPosition.L))
                toAdd = true;
            else if (Utils.hasFlag((int)p_MustConnectPos, (int)Enums.DoorsPosition.B) && Utils.hasFlag((int)roomInfos.doorsPos, (int)Enums.DoorsPosition.T))
                toAdd = true;
            else if (Utils.hasFlag((int)p_MustConnectPos, (int)Enums.DoorsPosition.L) && Utils.hasFlag((int)roomInfos.doorsPos, (int)Enums.DoorsPosition.R))
                toAdd = true;

            if (toAdd)
                canConnectRooms.Add(room);
        }
        return canConnectRooms;
    }

    private KeyValuePair<int, GameObject> getRandomValueFromList(List<KeyValuePair<int, GameObject>> p_List)
    {
        var arr = p_List.ToArray();
        var r   = Random.Range(0, arr.Count() - 1);
        return arr[r];
    }
}
