using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class ShopUIController : MonoBehaviour
{
	public TMP_Text titletitleTxt,aniText;
	public Button supplyBtn;
	public Button currencyBtn, coin1;
	public GameObject pane1, pane2, panelBox1, paneAnim;
	public Button boxBtn;
	public Animator anim;
	
	private void OnEnable()
	{
		supplyBtn?.onClick.AddListener(delegate { OpenPanel(supplyBtn); });
		currencyBtn?.onClick.AddListener(delegate { OpenPanel(currencyBtn); });
		boxBtn?.onClick.AddListener(delegate { OpenBox(boxBtn); });
		coin1.onClick.AddListener(PurchaseCoins1000);
		DisablePanels();

	}

	private void DisablePanels()
	{
		pane1.gameObject.SetActive(false);
		pane2.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		supplyBtn.onClick.RemoveListener(delegate { OpenPanel(supplyBtn); });
		currencyBtn.onClick.RemoveListener(delegate { OpenPanel(currencyBtn); });
		coin1?.onClick.RemoveListener(PurchaseCoins1000);
	}
	private void OpenPanel(Button button)
	{
		if (button == supplyBtn)
		{
			titletitleTxt.text = "Supply";
			pane1.SetActive(true);
			pane2.gameObject.SetActive(false);
			supplyBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
			currencyBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .4f);
		}
		if (button == currencyBtn)
		{
			pane1.gameObject.SetActive(false);
			pane2.gameObject.SetActive(true);
			supplyBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .4f);
			currencyBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

			titletitleTxt.text = "Currency";
		}
	}
	private void OpenBox(Button button)
	{
		panelBox1.SetActive(true);
	}
	public void CloseSmallPanel(GameObject gameObject)
	{
		gameObject.gameObject.SetActive(false);
	}

	private void PurchaseCoins1000()
	{
		int cost = 100;
		anim.Play("buy_anim");
		if (GameDataManager.Instance.gems >= cost)
		{
			GameDataManager.Instance.coins += 1000;
			GameDataManager.Instance.gems -= 100;
			MainSceneMenuController.instance.UpdateUI();
		}
		else
		{
			aniText.SetText("not enough gems");
		}
	}
}
