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
    }
}