using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionObject : MonoBehaviour
{
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {

        switch (SceneData.sceneNames.ToString())
        {
            case "Neurasthenia":
                sceneName = "Maingame";
                break;
            case "HitAndBlow":
                sceneName = "MaingameHB";
                break;
            case "GetBlock":
                sceneName = "MaingameGB";
                break;
            case "JigsawPuzzle":
                sceneName = "MaingameJP";
                break;
            case "QuicklyImage":
                sceneName = "MaingameQI";
                break;
            case "Flash":
                sceneName = "MaingameFL";
                break;
            case "Launchingbusiness":
                sceneName = "Launching_business";
                break;
            case "Reversible":
                sceneName = "Reversible";
                break;
            case "RepeatedlyHitting":
                sceneName = "RepeatedlyHitting";
                break;
            case "MusicGame":
                sceneName = "MusicGame";
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
}