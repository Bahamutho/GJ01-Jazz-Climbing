﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlatformerPhysics)),ExecuteInEditMode]
public class PlayerController : MonoBehaviour 
{
    // Move to PlayerInput
    [SerializeField]
    public ControlScheme ControlScheme;
    private PlatformerPhysics platformer;

	// Use this for initialization
	void Awake () 
    {
        platformer = GetComponent<PlatformerPhysics>();

        #region Controls

        if (ControlScheme == null)
        {
            ControlScheme = ControlScheme.CreateScheme<JazzClimbingPlayerActions>();

            // Make this into a nice function
            ControlScheme.Actions[(int)JazzClimbingPlayerActions.Jump].Keys.Add(ControlKey.PCKey(KeyCode.Space));
            ControlScheme.Actions[(int)JazzClimbingPlayerActions.Jump].Keys.Add(ControlKey.XboxButton(XboxCtrlrInput.XboxButton.A));

            ControlScheme.Actions[(int)JazzClimbingPlayerActions.PlayInstrument].Keys.Add(ControlKey.PCKey(KeyCode.E));
            ControlScheme.Actions[(int)JazzClimbingPlayerActions.PlayInstrument].Keys.Add(ControlKey.XboxButton(XboxCtrlrInput.XboxButton.B));
        }

        ControlManager.Instance.ControlSchemes[0] = ControlScheme;

        DontDestroyOnLoad(ControlManager.Instance);

        #endregion
	}
	
	// Update is called once per frame
	void Update () 
    {
        #if UNITY_EDITOR
        // Check all the animations and update the animation info
        if (!UnityEditor.EditorApplication.isPlaying)
            return;

        #endif

        if (Input.GetKeyUp(KeyCode.P))
        {
            RespawnPlayer();
            
        }
            

        platformer.SetMovementInput(ControlScheme.Horizontal.Value(), ControlScheme.Vertical.Value(), ControlScheme.Actions[(int)JazzClimbingPlayerActions.Jump].IsPressed(), ControlScheme.Actions[(int)JazzClimbingPlayerActions.Jump].IsDown(), false);//ControlScheme.Actions[(int)JazzClimbingPlayerActions.Dash].IsPressed());
	}

    private void RespawnPlayer()
    {
        platformer.playerState = PlatformerPhysics.PlayerState.Airborne;
        PlayerSpawn.Respawn();
    }

    //private IEnumerator RespawnWhenFallingTooLong()
    //{
    //    float time = Time.timeSinceLevelLoad;
    //    platformer.playerState = PlatformerPhysics.PlayerState.Falling;
    //    while (platformer.playerState == PlatformerPhysics.PlayerState.Falling)
    //    {
    //        float dt = Time.timeSinceLevelLoad - time;
    //        if ((dt) > platformer.FallSettings.MaxFallTimeToRespawn)
    //        {
    //            Debug.Log("Respawn");
    //            PlayerSpawn.Respawn();
    //        }
    //        yield return null;
    //    }
    //}
}
