using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DescriptionObject : MonoBehaviour
{
    string sceneName;
    public Text description;

    // Start is called before the first frame update
    void Start()
    {

        switch (SceneData.sceneNames.ToString())
        {
            case "Neurasthenia":
                sceneName = "Maingame";
                Neurasthenia();
                break;
            case "HitAndBlow":
                sceneName = "MaingameHB";
                HitAndBlow();
                break;
            case "GetBlock":
                sceneName = "MaingameGB";
                GetBlock();
                break;
            case "JigsawPuzzle":
                sceneName = "MaingameJP";
                JigsawPuzzle();
                break;
            case "QuicklyImage":
                sceneName = "MaingameQI";
                QuicklyImage();
                break;
            case "Flash":
                sceneName = "MaingameFL";
                Flash();
                break;
            case "Launchingbusiness":
                sceneName = "Launching_business";
                Launchingbusiness();
                break;
            case "Reversible":
                sceneName = "Reversible";
                Reversible();
                break;
            case "RepeatedlyHitting":
                sceneName = "RepeatedlyHitting";
                RepeatedlyHitting();
                break;
            case "MusicGame":
                sceneName = "MusicGame";
                MusicGame();
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Neurasthenia()
    {
        description.text = "盤面のカードを2枚選んで、ペアを揃えていこう！\n外れるとライフが減るので、0になるまでにどれだけ沢山ペアを揃えられるか挑戦だ！ ";
    }

    public void HitAndBlow()
    {
        description.text = "隠されている4枚の画像を正しい順番に並べよう！\n4枚の画像は同じものが2枚以上使われることもあるから注意してね！ \n選んだ画像と順番が正しかったらヒット！ \n順番が違うならブローになるよ！ \n8回以内に、正しい画像と順番を見つけよう！ ";
    }

    public void GetBlock()
    {

    }

    public void JigsawPuzzle()
    {

    }

    public void QuicklyImage()
    {

    }

    public void Flash()
    {

    }

    public void Launchingbusiness()
    {

    }

    public void Reversible()
    {

    }

    public void RepeatedlyHitting()
    {

    }

    public void MusicGame()
    {

    }
}