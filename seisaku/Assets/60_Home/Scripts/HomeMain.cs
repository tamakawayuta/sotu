using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Home
{
    public class HomeMain : MonoBehaviour
    {
        [SerializeField]
        private GameObject button;
        [SerializeField]
        private Sprite[] gameImages;


        private void Awake()
        {
            for (var i = 0; i < gameImages.Length; i++)
            {
                GameObject obj = Instantiate(button);
                obj.GetComponent<SelectButtonEventsHome>().SetButtonIndex(i);
                obj.GetComponent<Image>().sprite = gameImages[i];
                obj.transform.SetParent(this.gameObject.transform);
            }
        }
    }
}