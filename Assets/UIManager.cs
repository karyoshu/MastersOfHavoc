using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{

    public RectTransform BuildMenu;
    public RectTransform UpgradeMenu;
    public Text ResourceCount;
    public Slider healthSlider;
    public Text healthText;
    public Text NextWaveTimer;

    public Animator anim;

    public Text UpgradeCost;
    public Button UpgradeButton;

    public TurretShopItem[] shopItems;
    public GameObject PausePanel;
    public void ToggleUpgradeMenu(bool value)
    {
        UpgradeMenu.gameObject.SetActive(value);
        if (value)
        {
            UpgradeMenu.gameObject.SetActive(true);
            UpgradeMenu.anchoredPosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) + new Vector3(0, 120);
            UpgradeMenu.DOScale(0, 0);
            UpgradeMenu.DOScale(1, 0.1f);
            int cost = BuildManager.Instance.GetUpgradeCost(BuildManager.Instance.SelectedTurret);
            UpgradeCost.text = cost.ToString();
            UpgradeButton.interactable = PlayerStats.Instance.CurrentResources >= cost;
        }
        else
        {
            UpgradeMenu.DOScale(0, 0.2f).OnComplete(() =>
            UpgradeMenu.gameObject.SetActive(false));
        }
    }

    public void TogglePauseMenu(bool v)
    {
        PausePanel.SetActive(v);
    }

    public void ToggleBuildMenu(bool value)
    {
        if (value)
        {
            BuildMenu.gameObject.SetActive(true);
            BuildMenu.anchoredPosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) + new Vector3(0, 120);
            BuildMenu.DOScale(0, 0);
            BuildMenu.DOScale(1, 0.1f);
            foreach (TurretShopItem item in shopItems)
            {
                item.PurchaseButton.interactable = PlayerStats.Instance.CurrentResources >= item.cost;
            }
        }
        else
        {
            BuildMenu.DOScale(0, 0.2f).OnComplete(() =>
            BuildMenu.gameObject.SetActive(false));
        }
    }

    public void UpdateResourceCount()
    {
        ResourceCount.text = PlayerStats.Instance.CurrentResources.ToString();
    }

    public void UpdateHealth()
    {
        healthSlider.value = PlayerStats.Instance.Health / PlayerStats.Instance.MAX_HEALTH;
        healthText.text = string.Format("{0} / {1}", PlayerStats.Instance.Health, PlayerStats.Instance.MAX_HEALTH);
    }

    public void BuildTurret(TurretShopItem item)
    {
        BuildManager.Instance.BuildTurret(item.type);
    }

    public void DestroyTurret()
    {
        BuildManager.Instance.DestroyTurret();
    }

    public void ShowWaveClearedMessage()
    {
        anim.Play("WaveCleared");
    }

    public void UpgradeTurret()
    {
        BuildManager.Instance.UpgradeTurret();
    }

    public void Update()
    {
        NextWaveTimer.text = string.Format("{0:00.00}", EnemyManager.Instance.CountDown);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleBuildMenu(false);
            ToggleUpgradeMenu(false);
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        UpdateHealth();
        UpdateResourceCount();
    }
}