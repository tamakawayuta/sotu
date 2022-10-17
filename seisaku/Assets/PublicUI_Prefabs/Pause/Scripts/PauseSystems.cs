using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace PublicUI
{
    public class PauseSystems : MonoBehaviour
    {
        [SerializeField]
        private GameObject fade;

        private Scene loadSceneNow;

        private void Awake()
        {
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            loadSceneNow = SceneManager.GetActiveScene();
        }

        public void OnClickFront()
        {
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnClickRestart()
        {
            Time.timeScale = 1;
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }

        public void OnClickRetry()
        {
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