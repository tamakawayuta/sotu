using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flash
{
    public class CountSystemFL : MonoBehaviour
    {
        private int count = 0;

        public void AddCount()
        {
            this.count++;
            UpdateCountText();
        }

        public int GetCount()
        {
            return this.count;
        }

        private void UpdateCountText()
        {
            this.gameObject.GetComponent<Text>().text = this.count.ToString();
        }
    }
}