using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;
        protected override void Awake()
        {
            base.Awake();

            // do more stuff for player only
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();

            // FOR TESTING PURPOSES, COMMENT THIS OUT WHEN MENU AND NETWORK ARE SETUP, THEN UNCOMMENT THE
            // IF BLOCK UNDER OnNetworkSpawn() FOR CORRECT FUNCTIONALITY
            PlayerCamera.instance.player = this;

        }

        protected override void Update()
        {
            base.Update();


            // handles not being able to move not owner, uncomment when connecting with netcode
            // if (!IsOwner)
            // {
            //     Debug.Log("not owner!");
            //     return;
            // }


            //handle all of our character's movement
            playerLocomotionManager.HandleAllMovement();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            // if this is the player object owned by this client, uncomment when implementing netcode
            // if (IsOwner)
            // {
            //     PlayerCamera.instance.player = this;
            // }
        }

        protected override void LateUpdate()
        {
            // owner check, uncomment
            // if (!isOwner)
            // {
            //     return;
            // }
            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
            
        }
    }
}