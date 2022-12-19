using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
 void Awake() 
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;//singleton saying if u got one already u dont need another, when player respawn scene persist there alredy
        if(numScenePersists > 1)
        {
            Destroy(gameObject);//when we die it return us to the start
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
