using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameManager : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return 0;
        if (!GameManager.instance)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}
