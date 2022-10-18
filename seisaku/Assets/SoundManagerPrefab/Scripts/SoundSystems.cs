using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundManager
{
    public class SoundSystems : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] bgm;
        [SerializeField]
        private AudioClip[] se;

        private AudioSource bgmSource;
        private AudioSource seSource;

        private void Awake()
        {
            bgmSource = this.gameObject.GetComponents<AudioSource>()[0];
            seSource = this.gameObject.GetComponents<AudioSource>()[1];

            PlayBGM(0);
        }

        public void PlayBGM(int index)
        {
            bgmSource.clip = bgm[index];
            bgmSource.Play();
        }

        public void PlaySE(int index)
        {
            seSource.PlayOneShot(se[index]);
        }

        public void StopBGM()
        {
            bgmSource.Pause();
        }

        public void RestartBGM()
        {
            bgmSource.UnPause();
        }
    }
}