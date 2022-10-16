using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neurasthenia
{
    public class PanelSystems : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        public void switchPanelActive()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
            Debug.Log(this.gameObject.activeSelf);
        }
    }
}