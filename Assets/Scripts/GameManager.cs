using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public bool isPaused;

    private void Awake() 
    {
        singleton = this;
    }
}
