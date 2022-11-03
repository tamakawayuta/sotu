using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class FieldEventsCH : MonoBehaviour
    {
        private int index;

        public void OnClickField()
        {
            Debug.Log(this.index);
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }
    }
}