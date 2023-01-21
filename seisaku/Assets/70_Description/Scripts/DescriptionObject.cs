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
        description.text = "�Ֆʂ̃J�[�h��2���I��ŁA�y�A�𑵂��Ă������I\n�O���ƃ��C�t������̂ŁA0�ɂȂ�܂łɂǂꂾ����R�y�A�𑵂����邩���킾�I ";
    }

    public void HitAndBlow()
    {
        description.text = "�B����Ă���4���̉摜�𐳂������Ԃɕ��ׂ悤�I\n4���̉摜�͓������̂�2���ȏ�g���邱�Ƃ����邩�璍�ӂ��ĂˁI \n�I�񂾉摜�Ə��Ԃ�������������q�b�g�I \n���Ԃ��Ⴄ�Ȃ�u���[�ɂȂ��I \n8��ȓ��ɁA�������摜�Ə��Ԃ������悤�I ";
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