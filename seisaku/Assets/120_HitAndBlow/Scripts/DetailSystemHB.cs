using UnityEngine;

namespace HitAndBlow
{
    public class DetailSystemHB : MonoBehaviour
    {
        private void Awake()
        {
            DisappearDetail();
        }

        public void MoveDetail()
        {
            var posX = this.gameObject.GetComponent<RectTransform>().localPosition.x + 100f;

            this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(posX,0f,0f);
        }


        public void ApeearDetail()
        {
            this.gameObject.SetActive(true);
        }

        public void DisappearDetail()
        {
            this.gameObject.SetActive(false);
        }
    }
}