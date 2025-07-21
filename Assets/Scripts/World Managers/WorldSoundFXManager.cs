using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class WorldSoundFXManager : CharacterSoundFXManager
    {
        public static WorldSoundFXManager instance;

        [Header("Action Sounds")]
        public AudioClip rollSFX;
        public AudioClip damageSFXPlayer;
        public AudioClip damageSFXEnemy;
        public AudioClip attack1;
        public AudioClip attack2;
        public AudioClip attack3;
        public AudioClip enemyAttack;

        [Header("Enemy Noises")]
        public AudioClip groanSFXEnemy;
        public AudioClip deathSFXEnemy;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
