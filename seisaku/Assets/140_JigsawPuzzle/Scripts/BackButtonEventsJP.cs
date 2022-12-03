using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawPuzzle
{
    public class BackButtonEventsJP : MonoBehaviour
    {
        public void OnClickBack()
        {
            this.gameObject.SetActive(false);
        }
    }
}