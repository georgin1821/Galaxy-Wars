using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquadPanelController : MonoBehaviour
{
	private float amountUpgrade = 100;

	private string shipName;
	private int selectedShip;
	[SerializeField] TMP_Text powerText, upgradeInfo, coinsText, name;
	[SerializeField] Button shipBtn, squad1, squad2, squad3, squad4;
	Button[] ShipColectionBtns;
	[SerializeField] Button tab1, tab2, tab3;
	public GameObject upgradePanel;


	private void Start()
	{
		Button ship1 = getChildGameObject(upgradePanel, "Ship1").GetComponent<Button>();
		Button ship2 = getChildGameObject(upgradePanel, "Ship2").GetComponent<Button>();
		Button ship3 = getChildGameObject(upgradePanel, "Ship3").GetComponent<Button>();
		Button ship4 = getChildGameObject(upgradePanel, "Ship4").GetComponent<Button>();
		Button ship5 = getChildGameObject(upgradePanel, "Ship5").GetComponent<Button>();
		Button ship6 = getChildGameObject(upgradePanel, "Ship6").GetComponent<Button>();
		ShipColectionBtns = new[] { ship1, ship2, ship3, ship4, ship5, ship6 };
		 powerText.text = GameDataManager.Instance.ships[selectedShip].power.ToString();
		squad1.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[0].sprite;
		squad2.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[1].sprite;
		squad3.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[2].sprite;
		squad4.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[3].sprite;
		squad1.onClick.AddListener(delegate { ButtonClick(squad1); });        //disable eneable if unlocked ships btns

		for (int i = 0; i < GameDataManager.Instance.ships.Count; i++)
		{
			//ShipColectionBtns[i].interactable = GameDataManager.Instance.ships[0].isUnloked;
		}

		tab1.onClick.AddListener(delegate { Tabs(tab1); });
		tab2.onClick.AddListener(delegate { Tabs(tab2); });
		tab3.onClick.AddListener(delegate { Tabs(tab3); });
	}
	static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
	{
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
	public void SelectShip()
	{
		string shipName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

		switch (shipName)
		{
			case "Ship1":
				selectedShip = 0;
				UpdateUpgradeInfo();
				break;
			case "Ship2":
				selectedShip = 1;
				UpdateUpgradeInfo();
				break;
			case "Ship3":
				selectedShip = 1;
				UpdateUpgradeInfo();
				break;
			case "Ship4":
				selectedShip = 3;
				UpdateUpgradeInfo();
				break;
		}
	}
	private void UpdateUpgradeInfo()
	{
		//amountUpgrade = Mathf.Round(GameDataManager.Instance.ships.Rank * 100 * 1.1f);
		//powerText.text = GameDataManager.Instance.ships[selectedShip].startingPower.ToString();
		//name.text = GameDataManager.Instance.ships[selectedShip].name.ToString();

		upgradeInfo.text = amountUpgrade.ToString();
	}
	private void UpgradeShip()
	{
		{
			switch (selectedShip)
			{
				case 0:
					amountUpgrade = Mathf.Round(GameDataManager.Instance.ships[selectedShip].rank * 100 * 1.1f);
					if (GameDataManager.Instance.coins >= amountUpgrade)
					{
						Upgrade();
					}
					break;
				case 1:
					amountUpgrade = Mathf.Round(GameDataManager.Instance.ships[selectedShip].rank * 100 * 1.1f);
					if (GameDataManager.Instance.coins >= amountUpgrade)
					{
						Upgrade();
					}
					break;
			}
		}
	}
	private void Upgrade()
	{

		GameDataManager.Instance.selectedShip = selectedShip;
		int power = GameDataManager.Instance.ships[selectedShip].power + 100;
		GameDataManager.Instance.ships[selectedShip].power = power;
		GameDataManager.Instance.coins -= (int)amountUpgrade;
		GameDataManager.Instance.ships[selectedShip].rank++;
		powerText.text = GameDataManager.Instance.ships[selectedShip].power.ToString();
		amountUpgrade = Mathf.Round(GameDataManager.Instance.ships[selectedShip].rank * 100 * 1.1f);
		upgradeInfo.text = amountUpgrade.ToString();

		coinsText.text = GameDataManager.Instance.coins.ToString();

		GameDataManager.Instance.Save();
	}

	void ButtonClick(Button button)
	{
		if (button == squad1)
		{

		}
	}
	public void Tabs(Button button)
	{
		if(button == tab1)
		{
			//tab1.gameObject.GetComponent<Image>().color = new Color(1, 1,1, .4f);
			shipBtn.image.sprite = GameDataManager.Instance.squad[0].sprite;

		}
		if (button == tab2)
		{
			
			shipBtn.image.sprite = GameDataManager.Instance.squad[1].sprite;

		}
		if (button == tab3)
		{
			
			shipBtn.image.sprite = GameDataManager.Instance.squad[2].sprite;

		}
	}

}
