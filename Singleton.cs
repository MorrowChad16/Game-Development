using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic class to create any object type in our game
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance {
        get {
            if (instance == null)
            {
                //Create our new object for any type
                instance = FindObjectOfType<T>();
            }
            else if (instance != FindObjectOfType<T>())
            {
                //Destroys the object if it doesn't exist
                Destroy(FindObjectOfType<T>());
            }
            DontDestroyOnLoad(FindObjectOfType<T>());
            return instance;
        }
    }
}
