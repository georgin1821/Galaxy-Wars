using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class ShopUIController : MonoBehaviour
{
	public TMP_Text titletitleTxt;
	public Button supplyBtn;
	public Button currencyBtn;
	public GameObject pane1, pane2, panelBox1;
	public Button boxBtn;

	private void OnEnable()
	{
		supplyBtn?.onClick.AddListener(delegate { OpenPanel(supplyBtn); });
		currencyBtn?.onClick.AddListener(delegate { OpenPanel(currencyBtn); });
		boxBtn?.onClick.AddListener(delegate { OpenBox(boxBtn); });
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
		gameObject.gameObject. SetActive(false);
	}


}
