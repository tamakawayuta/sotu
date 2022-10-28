using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OthelloCell : MonoBehaviour
{

    int ownerID = -1;
    public Image ChipImage;
    public Vector2 Location;
    public Text CellEffectText;
    public int OwnerID
    {
        get { return ownerID; }
        set
        {
            ownerID = value;
            ChipImage.color = OthelloBoard.Instance.PlayerChipColors[ownerID + 1];
            if (ownerID == -1)
                this.GetComponent<Button>().interactable = true;
            else
                this.GetComponent<Button>().interactable = false;
        }
    }
    public void CellPressed()
    {
        if (OthelloBoard.Instance.CanPlaceHere(this.Location))
        {
            OthelloBoard.Instance.PlaceHere(this);
            OthelloBoard.Instance.EndTurn(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
