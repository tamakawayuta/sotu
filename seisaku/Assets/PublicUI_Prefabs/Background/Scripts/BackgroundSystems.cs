using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PublicUI
{
    public class BackgroundSystems : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] backgroundImages;

        private void Awake()
        {
            if (backgroundImages.Length == 0)
            {
                Debug.LogError("Please set background sprites");
                return;
            }

            for (var i = backgroundImages.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = backgroundImages[i];
                backgroundImages[i] = backgroundImages[j];
                backgroundImages[j] = tmp;
            }

            this.gameObject.GetComponent<Image>().sprite = backgroundImages[0];
        }
    }
}