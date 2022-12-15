using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flash
{
    public class SelectButtonEventsFL : MonoBehaviour
    {
        public void OnClickSelect()
        {
            GameObject.Find("GameDirector").GetComponent<CardSystemsFL>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);
            this.gameObject.GetComponent<Button>().enabled = false;
        }
    }
}