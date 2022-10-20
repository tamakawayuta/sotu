using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HitAndBlow {
    public class RecordSystemHB : MonoBehaviour
    {
        [SerializeField]
        private GameObject texts;
        [SerializeField]
        private GameObject images;

        private GameObject selectImagesNow;

        private Queue<GameObject> InfoTexts = new Queue<GameObject>();
        private Queue<GameObject> InfoImages = new Queue<GameObject>();

        private void Awake()
        {
            GameObject[] imagesChildren = new GameObject[images.transform.childCount];
            for (var i = 0; i < images.transform.childCount; i++)
            {
                imagesChildren[i] = images.transform.GetChild(i).gameObject;
                if (i == 0)
                {
                    continue;
                }

                for (var j = 0; j < 4; j++)
                {
                    imagesChildren[i].transform.GetChild(j).GetComponent<Button>().enabled = false;
                }

            }

            for (var i = 0; i < texts.transform.childCount; i++)
            {
                InfoTexts.Enqueue(texts.transform.GetChild(i).gameObject);
                InfoImages.Enqueue(imagesChildren[i]);
            }

            selectImagesNow = InfoImages.Dequeue();
        }

        private void Update()
        {
            
        }

        public void DrawTextInfo(int hit,int blow)
        {
            GameObject info = InfoTexts.Dequeue();
            info.GetComponent<Text>().text = hit.ToString() + " " + blow.ToString();
        }

        private void SetImageInfoNow()
        {
            SetActiveImageInfo(false);
            selectImagesNow = InfoImages.Dequeue();
            SetActiveImageInfo(true);
        }

        private void SetActiveImageInfo(bool value)
        {
            for (var i = 0; i < 4; i++)
            {
                selectImagesNow.transform.GetChild(i).GetComponent<Button>().enabled = value;
            }
        }
    }
}