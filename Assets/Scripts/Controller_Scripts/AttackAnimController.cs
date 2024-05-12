using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimController : MonoBehaviour
{   
    GameObject player;
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    void Awake() 
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = player.GetComponent<PlayerController>();

        
    }
    private void DestroyAttackAnim()
    {
        this.gameObject.SetActive(false);
    }
}
