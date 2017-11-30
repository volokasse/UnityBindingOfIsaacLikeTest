using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public GameManager gameManager;
    public GameObject heartAsset;
    public float offset;

    private Stack<GameObject> heartStack = new Stack<GameObject>();

    // Use this for initialization
    void Start()
    {
        var l_HeartsCount = gameManager.PlayerLives;
        Debug.Log("count of lives : " + l_HeartsCount);
        for (var l_Itr = 0; l_Itr < l_HeartsCount; l_Itr++)
        {
            var l_Transform = new Vector3(transform.position.x + (l_Itr * offset), transform.position.y, transform.position.z);
            var l_NewHeart = Instantiate(heartAsset, l_Transform, transform.rotation, transform);
            heartStack.Push(l_NewHeart);
        }
    }

    public void Hit()
    {
        if (heartStack.Count > 0)
        {
            var l_LastHeart = heartStack.Pop();
            Destroy(l_LastHeart);
        }
    }

    public void Heal()
    {
        var l_Transform = new Vector3(transform.position.x + (heartStack.Count * offset), transform.position.y, transform.position.z);
        var l_NewHeart  = Instantiate(heartAsset, l_Transform, transform.rotation, transform);

        heartStack.Push(l_NewHeart);
    }
}
