using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquadPanelController : MonoBehaviour
{
    private float amountUpgrade = 100;

    private string shipName;
    private int selectedShip;
    [SerializeField] TMP_Text powerText, upgradeInfo, coinsText;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Button shipBtn, squad1, squad2, squad3;
     Button[] ShipColectionBtns;
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
        //powerText.text = GameDataManager.Instance.shipsPower[selectedShip].ToString();
        squad1.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[0].ships.shipSprite;
        squad2.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[1].ships.shipSprite;
        squad3.gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.squad[2].ships.shipSprite;

        //disable eneable if unlocked ships btns
        for (int i = 0; i < GameDataManager.Instance.ships.Length; i++)
        {
            ShipColectionBtns[i].interactable = GameDataManager.Instance.isShipUnlocked[i];
        }
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
        shipName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

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
                selectedShip = 1;
                UpdateUpgradeInfo();
                break;
        }
    }
    private void UpdateUpgradeInfo()
    {
        amountUpgrade = Mathf.Round(GameDataManager.Instance.shipsRank[selectedShip] * 100 * 1.1f);
        powerText.text = GameDataManager.Instance.shipsPower[selectedShip].ToString();
        shipBtn.image.sprite = sprites[selectedShip];

        upgradeInfo.text = amountUpgrade.ToString();
    }
    private void UpgradeShip()
    {
        {
            switch (selectedShip)
            {
                case 0:
                    amountUpgrade = Mathf.Round(GameDataManager.Instance.shipsRank[selectedShip] * 100 * 1.1f);
                    if (GameDataManager.Instance.coins >= amountUpgrade)
                    {
                        Upgrade();
                    }
                    break;
                case 1:
                    amountUpgrade = Mathf.Round(GameDataManager.Instance.shipsRank[selectedShip] * 100 * 1.1f);
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
        int power = GameDataManager.Instance.shipsPower[selectedShip] + 100;
        GameDataManager.Instance.shipsPower[selectedShip] = power;
        GameDataManager.Instance.coins -= (int)amountUpgrade;
        GameDataManager.Instance.shipsRank[selectedShip]++;
        powerText.text = GameDataManager.Instance.shipsPower[selectedShip].ToString();
        amountUpgrade = Mathf.Round(GameDataManager.Instance.shipsRank[selectedShip] * 100 * 1.1f);
        upgradeInfo.text = amountUpgrade.ToString();

        coinsText.text = GameDataManager.Instance.coins.ToString();

        GameDataManager.Instance.Save();
    }

}
