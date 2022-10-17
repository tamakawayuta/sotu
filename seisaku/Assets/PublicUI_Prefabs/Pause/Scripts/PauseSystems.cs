using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Pause
{
    public class PauseSystems : MonoBehaviour
    {
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

        public void OnClickRetire()
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}