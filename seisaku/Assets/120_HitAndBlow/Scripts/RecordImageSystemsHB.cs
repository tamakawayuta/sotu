using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HitAndBlow
{
    public class RecordImageSystemsHB : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectButton;

        private Queue<GameObject> images = new Queue<GameObject>();
        private Sprite[] selectedSprite = new Sprite[4];
        private GameObject imageNow;
        private bool isImageSetFull = false;

        private void Awake()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    this.gameObject.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().enabled = false;
                }
            }

            for (var i = 0; i < 8; i++)
            {
                images.Enqueue(this.gameObject.transform.GetChild(i).gameObject);
            }

            imageNow = images.Dequeue();
        }

        private void Update()
        {
            if (selectButton.GetComponent<SelectButtonEventsHB>().GetSelectSpriteNow() != null)
            {
                for (var i = 0; i < 4; i++)
                {
                    imageNow.transform.GetChild(i).GetComponent<Button>().enabled = true;
                }
            }
        }

        public void OnClickRecord(int index)
        {
            imageNow.transform.GetChild(index).GetComponent<Image>().sprite = selectButton.GetComponent<SelectButtonEventsHB>().GetSelectSpriteNow();
            selectedSprite[index] = imageNow.transform.GetChild(index).GetComponent<Image>().sprite;
            imageNow.transform.GetChild(index).GetComponent<Image>().color = Color.white;
            SetIsImageSetFull();
        }

        public Sprite[] GetSelectedSprites() {
            return this.selectedSprite;
        }

        public void SetImageNow()
        {
            selectedSprite = new Sprite[4];
            SetActiveImageNow(false);
            imageNow = images.Dequeue();
            SetActiveImageNow(true);
            this.isImageSetFull = false;
        }

        public bool GetIsImageSetFull()
        {
            return this.isImageSetFull;
        }

        private void SetIsImageSetFull()
        {
            for (var i = 0; i < 4; i++)
            {
                if (imageNow.transform.GetChild(i).GetComponent<Image>().sprite == null)
                {
                    isImageSetFull = false;
                    return;
                }
            }

            isImageSetFull = true;
        }

        private void SetActiveImageNow(bool val)
        {
            for (var i = 0; i < 4; i++)
            {
                imageNow.transform.GetChild(i).GetComponent<Button>().enabled = val;
            }
        }
    }
}