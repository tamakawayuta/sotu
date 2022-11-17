using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class FieldMainGB : MonoBehaviour
    {
        private static Sprite selectSpriteNow;
        FieldSystemGB field;

        private void Awake()
        {
            selectSpriteNow = null;
            field = GameObject.Find("Fields").GetComponent<FieldSystemGB>();
        }

        public void OnClickField(int index)
        {
            if (selectSpriteNow != null && this.gameObject.GetComponent<Image>().sprite != selectSpriteNow)
            {
                return;
            }

            selectSpriteNow = this.gameObject.GetComponent<Image>().sprite;
            this.gameObject.GetComponent<Image>().color = Color.black;
            this.gameObject.GetComponent<Button>().enabled = false;
            field.RemoveCanSelectSprites(selectSpriteNow);
            field.AddSelectedIndex(index);
        }

        public void ClearSelectSprite()
        {
            selectSpriteNow = null;
        }
    }
}