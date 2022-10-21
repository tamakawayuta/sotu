using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HitAndBlow
{
    public class CheckButtonEventsHB : MonoBehaviour
    {
        [SerializeField]
        private GameObject images;

        private void Awake()
        {
            this.gameObject.GetComponent<Image>().color = Color.gray;
            this.gameObject.GetComponent<Button>().enabled = false;
        }

        private void Update()
        {
            if (images.GetComponent<RecordImageSystemsHB>().GetIsImageSetFull())
            {
                this.gameObject.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Button>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = Color.gray;
                this.gameObject.GetComponent<Button>().enabled = false;
            }
        }

        public void OnClickCheck()
        {
            images.GetComponent<RecordImageSystemsHB>().SetImageNow();
        }
    }
}