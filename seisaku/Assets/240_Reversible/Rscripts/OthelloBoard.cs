using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoundManager;

public class OthelloBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject sound;
    // Start is called before the first frame update
    public int CurrentTurn = 0;
    public GameObject ScoreBoard;
    public Text ScoreBoardText;
    public GameObject Template;
    public int players;
    private int BoardSize;
    public List<Color> PlayerChipColors;
    public List<Vector2> DirectionList;
    static OthelloBoard instance;
    public static OthelloBoard Instance { get { return instance; } }
    OthelloCell[,] OthelloCells;
    public int EnemyID { get { return (CurrentTurn + 1) % players; } }
    private bool isOnce = true;
    private int scene;
    public GameObject[] Scene;

    public Text[] ScoreText; 
    void Start()
    {
        instance = this;
        OthelloBoardIsSquareSize();
        scene = 0;
    }

    public void Initialization()
    {
        OthelloCells = new OthelloCell[BoardSize, BoardSize];
        float cellAnchorSize = 1.0f / BoardSize;
        for (int y = 0; y < BoardSize; y++)
        {
            for (int x = 0; x < BoardSize; x++)
            {
                CreateNewCell(x, y, cellAnchorSize);
            }
        }
        ScoreBoard.GetComponent<RectTransform>().SetSiblingIndex(BoardSize * BoardSize + 1);
        GameObject.Destroy(Template);
        InitializeGame();
    }

    public void Playernum(int x)
    {
        sound.GetComponent<SoundSystems>().PlaySE(0);
        players = x;
        BoardSize = x + 6;
        scene = 1;
    }

    public void BackButton()
    {
        Scene[1].SetActive(false);
        scene = 0;
    }

    private void CreateNewCell(int x, int y, float cellAnchorSize)
    {
        GameObject go = GameObject.Instantiate(Template, this.transform);
        RectTransform r = go.GetComponent<RectTransform>();
        r.anchorMin = new Vector2(x * cellAnchorSize, y * cellAnchorSize);
        r.anchorMax = new Vector2((x + 1) * cellAnchorSize, (y + 1) * cellAnchorSize);
        OthelloCell oc = go.GetComponent<OthelloCell>();
        OthelloCells[x, y] = oc;
        oc.Location.x = x;
        oc.Location.y = y;
    }
    private void OthelloBoardIsSquareSize()
    {
        RectTransform rect = this.GetComponent<RectTransform>();
        if (Screen.width > Screen.height)
        {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.height);
        }
        else
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.width);
    }
    public void InitializeGame()
    {
        ScoreBoard.gameObject.SetActive(false);
        for (int y = 0; y < BoardSize; y++)
        {
            for (int x = 0; x < BoardSize; x++)
            {
                OthelloCells[x, y].OwnerID = -1;
            }
        }

        switch (players)
        {
            case 2:
                TwoInitialPosition();
                break;
            case 3:
                ThreeInitialPosition();
                break;
            case 4:
                FourInitialPosition();
                break;
        }
    }

    //OthelloCells[, ].OwnerID = ;

    public void TwoInitialPosition()
    {
        OthelloCells[3, 3].OwnerID = 0;
        OthelloCells[4, 4].OwnerID = 0;
        OthelloCells[3, 4].OwnerID = 1;
        OthelloCells[4, 3].OwnerID = 1;
    }

    public void ThreeInitialPosition()
    {
        OthelloCells[3, 3].OwnerID = 0;
        OthelloCells[4, 4].OwnerID = 0;
        OthelloCells[5, 5].OwnerID = 0;
        OthelloCells[3, 4].OwnerID = 1;
        OthelloCells[4, 5].OwnerID = 1;
        OthelloCells[5, 3].OwnerID = 1;
        OthelloCells[3, 5].OwnerID = 2;
        OthelloCells[4, 3].OwnerID = 2;
        OthelloCells[5, 4].OwnerID = 2;
    }

    public void FourInitialPosition()
    {
        OthelloCells[3, 3].OwnerID = 0;
        OthelloCells[4, 4].OwnerID = 0;
        OthelloCells[6, 5].OwnerID = 0;
        OthelloCells[5, 6].OwnerID = 0;
        OthelloCells[3, 4].OwnerID = 1;
        OthelloCells[5, 5].OwnerID = 1;
        OthelloCells[4, 6].OwnerID = 1;
        OthelloCells[6, 3].OwnerID = 1;
        OthelloCells[4, 5].OwnerID = 2;
        OthelloCells[3, 6].OwnerID = 2;
        OthelloCells[5, 3].OwnerID = 2;
        OthelloCells[6, 4].OwnerID = 2;
        OthelloCells[3, 5].OwnerID = 3;
        OthelloCells[4, 3].OwnerID = 3;
        OthelloCells[5, 4].OwnerID = 3;
        OthelloCells[6, 6].OwnerID = 3;
    }

    internal bool CanPlaceHere(Vector2 location)
    {
        if (OthelloCells[(int)location.x, (int)location.y].OwnerID != -1)
            return false;

        for (int direction = 0; direction < DirectionList.Count; direction++)
        {
            Vector2 directionVector = DirectionList[direction];
            if (FindAllyChipOnOtherSide(directionVector, location, false) != null)
            {
                return true;
            }
        }
        return false;
    }
    internal void PlaceHere(OthelloCell othelloCell)
    {
        for (int direction = 0; direction < DirectionList.Count; direction++)
        {
            Vector2 directionVector = DirectionList[direction];
            OthelloCell onOtherSide = FindAllyChipOnOtherSide(directionVector, othelloCell.Location, false);
            if (onOtherSide != null)
            {
                ChangeOwnerBetween(othelloCell, onOtherSide, directionVector);
            }
        }
        OthelloCells[(int)othelloCell.Location.x, (int)othelloCell.Location.y].OwnerID = CurrentTurn;
    }
    private OthelloCell FindAllyChipOnOtherSide(Vector2 directionVector, Vector2 from, bool EnemyFound)
    {
        Vector2 to = from + directionVector;
        if (IsInRangeOfBoard(to) && OthelloCells[(int)to.x, (int)to.y].OwnerID != -1)
        {
            if (OthelloCells[(int)to.x, (int)to.y].OwnerID == OthelloBoard.Instance.CurrentTurn)
            {
                if (EnemyFound)
                    return OthelloCells[(int)to.x, (int)to.y];
                return null;
            }
            else
                return FindAllyChipOnOtherSide(directionVector, to, true);
        }
        return null;
    }
    private bool IsInRangeOfBoard(Vector2 point)
    {
        return point.x >= 0 && point.x < BoardSize && point.y >= 0 && point.y < BoardSize;
    }
    private void ChangeOwnerBetween(OthelloCell from, OthelloCell to, Vector2 directionVector)
    {
        for (Vector2 location = from.Location + directionVector; location != to.Location; location += directionVector)
        {
            OthelloCells[(int)location.x, (int)location.y].OwnerID = CurrentTurn;
        }
    }
    internal void EndTurn(bool isAlreadyEnded)
    {
        CurrentTurn = EnemyID;
        for (int y = 0; y < BoardSize; y++)
        {
            for (int x = 0; x < BoardSize; x++)
            {
                if (CanPlaceHere(new Vector2(x, y)))
                {
                    return;
                }
            }
        }
        if (isAlreadyEnded)
            GameOver();
        else
        {
            EndTurn(true);
        }
    }
    public void GameOver()
    {
        for (int y = 0; y < BoardSize; y++)
        {
            for (int x = 0; x < BoardSize; x++)
            {
                OthelloCells[x, y].GetComponent<Button>().interactable = false;
            }
        }

        switch (players)
        {
            case 2:
                ScoreTwoPeople();
                break;
            case 3:
                ScoreThreePeople();
                break;
            case 4:
                ScoreFourPeople();
                break;
        }
        ScoreBoard.gameObject.SetActive(true);
    }

    public void ScoreTwoPeople()
    {
        int white = CountScoreFor(0);
        int black = CountScoreFor(1);
        if (white > black)
            ScoreBoardText.text = "しろの勝ち \n" + white + ":" + black ;
        else if (black > white)
            ScoreBoardText.text = "くろの勝ち \n" + white + ":" + black ;
        else
            ScoreBoardText.text = "引き分け！ \n" + white + ":" + black;
    }

    public void ScoreThreePeople()
    {
        int white = CountScoreFor(0);
        int black = CountScoreFor(1);
        int red = CountScoreFor(2);
        if (white > black && white > red)
            ScoreBoardText.text = "しろの勝ち \n" + white + ":" + black + ":" + red;
        else if (black > white && black > red)
            ScoreBoardText.text = "くろの勝ち \n" + white + ":" + black + ":" + red;
        else if (red > black && red > white)
            ScoreBoardText.text = "あかの勝ち \n" + white + ":" + black + ":" + red;
        else
            ScoreBoardText.text = "引き分け！ \n" + white + ":" + black + ":" + red;
    }

    public void ScoreFourPeople()
    {
        int white = CountScoreFor(0);
        int black = CountScoreFor(1);
        int red = CountScoreFor(2);
        int blue = CountScoreFor(3);
        if (white > black && white > red && white > blue)
            ScoreBoardText.text = "しろの勝ち \n" + white + ":" + black + ":" + red + ":" + blue;
        else if (black > white && black > red && black > blue)
            ScoreBoardText.text = "くろの勝ち \n" + white + ":" + black + ":" + red + ":" + blue;
        else if (red > black && red > white && red > blue)
            ScoreBoardText.text = "あかの勝ち \n" + white + ":" + black + ":" + red + ":" + blue;
        else if (blue > white && blue > black && blue > red)
            ScoreBoardText.text = "あおの勝ち \n" + white + ":" + black + ":" + red + ":" + blue;
        else
            ScoreBoardText.text = "引き分け！ \n" + white + ":" + black + ":" + red + ":" + blue;
    }
    private int CountScoreFor(int owner)
    {
        int count = 0;
        for (int y = 0; y < BoardSize; y++)
        {
            for (int x = 0; x < BoardSize; x++)
            {
                if (OthelloCells[x, y].OwnerID == owner)
                {
                    count++;
                }
            }
        }
        return count;
    }
    
    public void Score()
    {
        int white = CountScoreFor(0);
        int black = CountScoreFor(1);
        int red = CountScoreFor(2);
        int blue = CountScoreFor(3);

        if (players > 2)
        {
            ScoreText[2].text = "" + red;
            if (players > 3)
            {
                ScoreText[3].text = "" + blue;
            }
        }
        if(players <= 2)
        {
            ScoreText[2].text = "";
            ScoreText[3].text = "";
        }
        else if(players <= 3)
        {
            ScoreText[3].text = "";
        }


        ScoreText[0].text = "" + white;
        ScoreText[1].text = "" + black;
    }

    // Update is called once per frame
    void Update()
    {
        if(scene == 0)
        {
            Scene[0].SetActive(true);
        }
        if(scene == 1) { 
            Scene[0].SetActive(false);
            Scene[1].SetActive(true);
            if (isOnce)
            {
                Initialization();
                //処理内容
                isOnce = false;
            }
        }
        Score();
    }
}
