using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance =  FindObjectOfType<GameManager>();
            }
            return instance;

        }
    }

    private PlayerController mainPlayer;



    void Start()
    {
        UIManager.Instance.DebugMethod();
        
    }

    public void StartGame()
    {

    }

    public void RegisterMainPlayer( PlayerController playerToRegister)
    {
        mainPlayer = null;

        mainPlayer = playerToRegister;

    }

   
}
