using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{ 
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            // option 01
            character.animator.SetFloat("Horizontal", horizontalValue);
            character.animator.SetFloat("Vertical", verticalValue);

            // option 02, snap
            
        }
    }
}
