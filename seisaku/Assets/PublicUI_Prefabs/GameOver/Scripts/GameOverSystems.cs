using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace PublicUI
{
    public class GameOverSystems : MonoBehaviour
    {
        [SerializeField]
        private GameObject fade;

        private Scene loadSceneNow;

        private void Awake()
        {
            this.gameObject.SetActive(false);
            loadSceneNow = SceneManager.GetActiveScene();
        }

        public void AppearGameOverUI()
        {
            Time.timeScale = 0;
            this.gameObject.SetActive(true);
        }

        public void OnClickRetry()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(loadSceneNow.name);
        }

        public async void OnClickRetire()
        {
            fade.GetComponent<FadeSystems>().FadeOut();
            await Task.Delay(4000);
            SceneManager.LoadScene("HomeScene");
        }
    }
}