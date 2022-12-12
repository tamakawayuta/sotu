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
            GameObject.Find("GameDirector").GetComponent<CardSystemsFL>().SetSelectedObject(this.gameObject.GetComponent<Image>().sprite);
        }
    }
}