using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using SoundManager;

namespace PublicUI
{
    public class PauseSystems : MonoBehaviour
    {
        [SerializeField]
        private GameObject fade;
        [SerializeField]
        private AudioClip buttonSE;
        [SerializeField]
        private GameObject soundManager;

        private AudioSource source;
        private Scene loadSceneNow;

        private void Awake()
        {
            source = this.gameObject.GetComponent<AudioSource>();

            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            loadSceneNow = SceneManager.GetActiveScene();

            Time.timeScale = 1;
        }

        public void OnClickFront()
        {
            soundManager.GetComponent<SoundSystems>().StopBGM();
            source.PlayOneShot(buttonSE);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnClickRestart()
        {
            Time.timeScale = 1;
            soundManager.GetComponent<SoundSystems>().RestartBGM();
            source.PlayOneShot(buttonSE);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }

        public async void OnClickRetry()
        {
            source.PlayOneShot(buttonSE);
            await Task.Delay(400);
            SceneManager.LoadScene(loadSceneNow.name);
        }

        public async void OnClickRetire()
        {
            source.PlayOneShot(buttonSE);
            fade.GetComponent<FadeSystems>().FadeOut();
            await Task.Delay(4000);
            SceneManager.LoadScene("HomeScene");
        }
    }
}