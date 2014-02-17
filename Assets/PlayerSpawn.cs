﻿using UnityEngine;
using System.Collections;

public class PlayerSpawn : Singleton<PlayerSpawn> 
{
    private Transform player;
    public float CameraHeightOffset = 5;
	// Use this for initialization
	void Awake () 
    {
        player = GameObject.Find("PlayerCharacter").transform;
        Debug.Log(player.name);
        Respawn();
	}

    public static void Respawn()
    {
        Debug.Log(Instance.player.transform.position);
        Debug.Log(Instance.transform.position);
        Vector3 pos = Instance.transform.position;
        Instance.player.position = pos;
        pos.y+=Instance.CameraHeightOffset;
        pos.z = Camera.mainCamera.transform.position.z;
        Camera.mainCamera.transform.position = pos;
    }
}
