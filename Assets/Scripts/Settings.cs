using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Settings : MonoBehaviour
{
    public static Settings instance = null;

    public Dictionary<int, string> Tags = new Dictionary<int, string>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
