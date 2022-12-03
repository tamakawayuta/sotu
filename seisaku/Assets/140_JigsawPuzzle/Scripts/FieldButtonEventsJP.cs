using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JigsawPuzzle
{
    public class FieldButtonEventsJP : MonoBehaviour
    {
        private PuzzleSystemJP field;
        private int index;

        private void Awake()
        {
            this.field = GameObject.Find("Background_Image").GetComponent<PuzzleSystemJP>();
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public int GetIndex()
        {
            return this.index;
        }

        public void OnClickfield()
        {
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().color = Color.white;
            field.AddSelected(this.gameObject);
        }
    }
}