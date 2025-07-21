using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
        }

        public void PlayPlayerDamageSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.damageSFXPlayer);
        }

        public void PlayEnemyDamageSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.damageSFXEnemy);
        }

        public void PlayEnemyGroanSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.groanSFXEnemy);
        }

        public void PlayEnemyDeathSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.deathSFXEnemy);
        }

        public void PlayAttack1SoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.attack1);
        }

        public void PlayAttack2SoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.attack2);
        }

        public void PlayAttack3SoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.attack3);
        }

        public void PlayEnemyAttackSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.enemyAttack);
        }



    }

