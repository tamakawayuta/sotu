using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SceneNames;
using PublicUI;

namespace Home
{
    public class SelectButtonEventsHome : MonoBehaviour
    {
        public Text name;
        private string gameName;
        private int buttonIndex;

        private void Start()
        {
            GameSelect();
            name.text = gameName;
        }
        public async void OnClickSelect()
        {
            GameSelect();
            var fade = GameObject.Find("FadeUI");
            fade.GetComponent<FadeSystems>().FadeOut();
            await Task.Delay(3000);
            SceneManager.LoadScene("DescriptionScene");
        }

        public void SetButtonIndex(int value)
        {
            this.buttonIndex = value;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void GameSelect()
        {
            
            switch (buttonIndex)
            {
                case 0:
                    SceneData.sceneNames = SceneName.Neurasthenia;
                    gameName = "(仮)真剣衰弱";
                    break;
                case 1:
                    SceneData.sceneNames = SceneName.HitAndBlow;
                    gameName = "(仮)ヒット＆ブロー";
                    break;
                case 2:
                    SceneData.sceneNames = SceneName.GetBlock;
                    gameName = "(仮)ブロック取り";
                    break;
                case 3:
                    SceneData.sceneNames = SceneName.JigsawPuzzle;
                    gameName = "(仮)ジグソーパズル";
                    break;
                case 4:
                    SceneData.sceneNames = SceneName.Flash;
                    gameName = "(仮)フラッシュ";
                    break;
                case 5:
                    SceneData.sceneNames = SceneName.QuicklyImage;
                    gameName = "(仮)瞬間画像当て";
                    break;
                case 6:
                    SceneData.sceneNames = SceneName.Launchingbusiness;
                    gameName = "旗揚げゲーム";
                    break;
                case 7:
                    SceneData.sceneNames = SceneName.Reversible;
                    gameName = "リバーシ";
                    break;
                case 8:
                    SceneData.sceneNames = SceneName.RepeatedlyHitting;
                    gameName = "(仮)連打ゲーム";
                    break;
                case 9:
                    SceneData.sceneNames = SceneName.MusicGame;
                    gameName = "(仮)音ゲー";
                    break;
                default:
                    Debug.LogError("IllegalValue");
                    break;
            }
        }
    }
}