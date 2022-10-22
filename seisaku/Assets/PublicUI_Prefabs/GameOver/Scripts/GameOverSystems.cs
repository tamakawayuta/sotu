using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using SoundManager;

namespace PublicUI
{
    public class GameOverSystems : MonoBehaviour
    {
        [SerializeField]
        private GameObject fade;
        [SerializeField]
        private AudioClip buttonSE;
        [SerializeField]
        private AudioClip fanfareSE;
        [SerializeField]
        private bool isPlatFanfare;
        [SerializeField]
        private GameObject soundManager;

        private AudioSource source;
        private Scene loadSceneNow;

        private GameObject scoreText;

        private void Awake()
        {
            source = this.gameObject.GetComponent<AudioSource>();

            scoreText = this.gameObject.transform.GetChild(2).gameObject;
            scoreText.GetComponent<Text>().text = "";

            this.gameObject.SetActive(false);
            loadSceneNow = SceneManager.GetActiveScene();
        }

        public void AppearGameOverUI(int score)
        {
            Time.timeScale = 0;
            this.gameObject.SetActive(true);

            if (isPlatFanfare)
            {
                source.PlayOneShot(fanfareSE);
            }

            scoreText.GetComponent<Text>().text = "ÉXÉRÉA: " + score.ToString();
            soundManager.GetComponent<SoundSystems>().StopBGM();
        }

        public async void OnClickRetry()
        {
            Time.timeScale = 1;
            source.PlayOneShot(buttonSE);
            await Task.Delay(350);
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