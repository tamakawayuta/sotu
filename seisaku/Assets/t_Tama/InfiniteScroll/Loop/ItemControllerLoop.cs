using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Home;

[RequireComponent(typeof(InfiniteScroll))]
public class ItemControllerLoop : UIBehaviour, IInfiniteScrollSetup
{
	private bool isSetuped = false;

	public void OnPostSetupItems()
	{
		GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
		isSetuped = true;
	}

	public void OnUpdateItem(int itemCount, GameObject obj)
	{
		if(isSetuped == true) return;

		/*var item = obj.GetComponentInChildren<Item>();
		item.UpdateItem(itemCount);
		*/
		var item = obj.GetComponentInChildren<SelectButtonEventsHome>();
		item.SetButtonIndex(itemCount);
	}
}
