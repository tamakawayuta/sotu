using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class FieldMainGB : MonoBehaviour
    {
        public void OnClickField(int index)
        {
            this.gameObject.GetComponent<Image>().color = Color.black;
            GameObject.Find("Fields").GetComponent<FieldSystemGB>().UpdateGameState(index);
        }
    }
}