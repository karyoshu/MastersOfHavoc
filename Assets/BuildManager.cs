using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager> {


    public List<TurretBluePrint> Turrets;
    public GameObject BuildEffect;
    public GameObject destroyEffect;
    private Node SelectedNode;
    public Turret SelectedTurret;

    public void BuildTurret(TurretType turretType)
    {
        Time.timeScale = 1;
        if (SelectedNode.turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }

        TurretBluePrint turretToBuild = Turrets.Find(turret => turret.TurretType == turretType);
        if (turretToBuild == null)
        {
            Debug.LogFormat("Turret of type {0} not found", turretType);
            return;
        }

        if (turretToBuild.cost <= PlayerStats.Instance.CurrentResources)
        {
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, SelectedNode.GetBuildPosition(), SelectedNode.transform.rotation);
            turret.transform.SetParent(SelectedNode.transform);
            SelectedNode.turret = turret;
            GameObject effect = Instantiate(BuildEffect, SelectedNode.GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            PlayerStats.Instance.CurrentResources -= turretToBuild.cost;
        }
        UIManager.Instance.ToggleBuildMenu(false);
    }

    public void OnNodeSelection(Node node)
    {
        SelectedNode = node;
        Time.timeScale = 0.2f;
        if (node.turret != null)
        {
            SelectedTurret = node.turret.GetComponent<Turret>();
            UIManager.Instance.ToggleUpgradeMenu(true);
        }
        else
        {
            UIManager.Instance.ToggleBuildMenu(true);
        }
    }

    public void UpgradeTurret()
    {
        Time.timeScale = 1;
        if (GetUpgradeCost(SelectedTurret) <= PlayerStats.Instance.CurrentResources)
        {
            PlayerStats.Instance.CurrentResources -= GetUpgradeCost(SelectedTurret);
            SelectedTurret.Upgrade();
        }
        UIManager.Instance.ToggleUpgradeMenu(false);
    }

    public void DestroyTurret()
    {
        Time.timeScale = 1;
        PlayerStats.Instance.CurrentResources += (Turrets.Find(t => t.TurretType == SelectedTurret.type).cost / 2);
        Destroy(SelectedNode.turret);
        GameObject effect = Instantiate(destroyEffect, SelectedNode.transform.position, SelectedNode.transform.rotation);
        Destroy(effect, 5f);
        UIManager.Instance.ToggleUpgradeMenu(false);
    }

    public int GetUpgradeCost(Turret turret)
    {
        return Turrets.Find(t => t.TurretType == turret.type).cost + turret.Rank * (2 + turret.Rank);
    }
}
