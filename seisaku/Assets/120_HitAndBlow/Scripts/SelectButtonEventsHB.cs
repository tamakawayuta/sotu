using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HitAndBlow
{
    public class SelectButtonEventsHB : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] images;

        private Sprite selectSpriteNow;
        GameObject selectNow;

        private void Awake()
        {
            for (var i = 0; i < images.Length; i++)
            {
                this.gameObject.transform.GetChild(i).GetComponent<Image>().sprite = this.images[i];
                this.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        private void Update()
        {
            if (selectNow != null)
            {
                EventSystem.current.SetSelectedGameObject(selectNow);
            }
        }

        public void OnClickSelect(int cardNum)
        {
            this.selectSpriteNow = this.gameObject.transform.GetChild(cardNum).GetComponent<Image>().sprite;
            selectNow = this.gameObject.transform.GetChild(cardNum).gameObject;
        }

        public Sprite GetSelectSpriteNow()
        {
            return this.selectSpriteNow;
        }
    }
}