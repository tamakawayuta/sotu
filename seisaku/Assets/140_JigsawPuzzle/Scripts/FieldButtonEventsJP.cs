using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JigsawPuzzle
{
    public class FieldButtonEventsJP : MonoBehaviour
    {
        private PuzzleSystemJP field;

        private void Awake()
        {
            this.field = GameObject.Find("Background_Image").GetComponent<PuzzleSystemJP>();
        }

        public void OnClickfield()
        {
            field.AddSelected(this.gameObject);
        }
    }
}