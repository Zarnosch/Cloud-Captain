﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

	[Header("Canvas")]
	public GameObject ParentCavas;
	public GameObject InteractionPane;

	[Header("Resources")]
	public Text MassText;
	public Text EnergyText;
	public Text MashineText;

	[Header("Build Pane")]
	public GameObject BuildPanePrefab;
	public Button BuildButtonPrefab;

	[Header("Selected Pane")]
	public GameObject SelectedPanelPrefab;
	public GameObject UpgradeDropdown;
	public GameObject UpgradeText;
	public Button BuildButton;

	[Header("")]
	[ReadOnlyAttribute]
	public GameObject selectedObj;

	private GameObject activeObjPane;

	public void ShowUpgadePanel() 
	{
		Setting.ObjectType objType = selectedObj.GetComponent<GameobjectType> ().ObjectType;

		Upgrade upgradeComponent = selectedObj.GetComponent<Upgrade> ();
		HealthManager healthStats = selectedObj.GetComponent<HealthManager> ();
		ShipMove shipMoveComponent = selectedObj.GetComponent<ShipMove> ();
		ShipBuilder shipBuilderComponent = selectedObj.GetComponent<ShipBuilder> ();

		var availUpgrades = upgradeComponent.AvaibleUpgrades;
		var installedUpgrades = upgradeComponent.UsedUpgrades;

		activeObjPane = Instantiate (SelectedPanelPrefab);
		activeObjPane.transform.SetParent (InteractionPane.transform);
		activeObjPane.GetComponent<RectTransform> ().offsetMax = new Vector2(0, 0);
		activeObjPane.GetComponent<RectTransform> ().offsetMin = new Vector2(0, 0);
		PaneManager paneManagerComponent = activeObjPane.GetComponent<PaneManager> ();

		// set stats
		paneManagerComponent.Name.text = objType.ToString();
		paneManagerComponent.Life.text = healthStats.GetCurHealth().ToString() + " / " + healthStats.GetMaxHealth().ToString();

		// show movement speed for all ships
		if (objType == Setting.ObjectType.BigShip || objType == Setting.ObjectType.MediumShip || objType == Setting.ObjectType.Scouter || objType == Setting.ObjectType.SmallShip) {
			paneManagerComponent.StatsMoveSpeed.text = shipMoveComponent.speed.ToString();
		} else {
			paneManagerComponent.StatsMoveSpeed.gameObject.transform.parent.gameObject.SetActive (false);
		}

		// set Range and Damage for all tower and ships
		if (objType != Setting.ObjectType.Mine && objType != Setting.ObjectType.Nexus && objType != Setting.ObjectType.PowerPlant &&
		    objType != Setting.ObjectType.Settlement && objType != Setting.ObjectType.Shipyard && objType != Setting.ObjectType.Workshop) {

			BulletSpawner bulletSpawnComponent = selectedObj.GetComponent<BulletSpawnerReference> ().Attacker;

			paneManagerComponent.StatsReach.text = bulletSpawnComponent.GetAttackRange ().ToString ();	
			paneManagerComponent.StatsDmg.text = bulletSpawnComponent.BulletDamage.ToString();
		} else {
			paneManagerComponent.StatsReach.gameObject.transform.parent.gameObject.SetActive (false);
			paneManagerComponent.StatsDmg.gameObject.transform.parent.gameObject.SetActive (false);
		}

		// build stuff
		if (objType == Setting.ObjectType.Nexus || objType == Setting.ObjectType.Shipyard) {
			BuildManager.ShipType[] buildableShips = shipBuilderComponent.BuildableShips;

			foreach (var buildable in buildableShips) {
				Button bB = Instantiate (BuildButton);
				bB.transform.SetParent(paneManagerComponent.ActionsPane.transform);
				bB.GetComponentInChildren<Text> ().text = buildable.ToString ();
				switch (buildable) {
					case BuildManager.ShipType.Settler:
						bB.onClick.AddListener (() => { BuildShip(shipBuilderComponent, BuildManager.ShipType.Settler); });
						break;
					case BuildManager.ShipType.Scout:
						bB.onClick.AddListener (() => { BuildShip(shipBuilderComponent, BuildManager.ShipType.Scout); });
						break;
					case BuildManager.ShipType.Small:
						bB.onClick.AddListener (() => { BuildShip(shipBuilderComponent, BuildManager.ShipType.Small); });
						break;
					case BuildManager.ShipType.Medium:
						bB.onClick.AddListener (() => { BuildShip(shipBuilderComponent, BuildManager.ShipType.Medium); });
						break;
					case BuildManager.ShipType.Big:
						bB.onClick.AddListener (() => { BuildShip(shipBuilderComponent, BuildManager.ShipType.Big); });
						break;
					default:
						Debug.LogError ("This should not happen!");
						break;
				}
			}

			shipBuilderComponent.RemoveQueueChangedListener (BuildQueuePanelUpdate);
			shipBuilderComponent.AddQueueChangedListener (BuildQueuePanelUpdate);

		} else {
			paneManagerComponent.BuildQueue.transform.parent.transform.parent.gameObject.SetActive (false);
		}

		Transform upgradePane = paneManagerComponent.UpgradePane.transform;
		UpdateUpgrades (availUpgrades, installedUpgrades, upgradePane);
	}

	public void BuildQueuePanelUpdate(GameObject updatedObj) {

		if (updatedObj == selectedObj) {
			PaneManager paneRef = activeObjPane.GetComponent<PaneManager> ();


			foreach (Transform child in paneRef.BuildQueue.transform) {
				Destroy (child.gameObject);
			}

			BuildManager.ShipInfo[] shipsInQueue = selectedObj.GetComponent<ShipBuilder> ().enqueuedShips.ToArray ();

			foreach (var buildable in shipsInQueue) {
				
				var queuedShip = Instantiate (UpgradeText);
				queuedShip.transform.SetParent(paneRef.BuildQueue.transform);
				queuedShip.GetComponent<Text> ().text = buildable.prefab.GetComponent<GameobjectType> ().ObjectType.ToString ();
			}
		}
	}

	public void BuildShip(ShipBuilder parentShipYard, BuildManager.ShipType buildShipType) {
		parentShipYard.BuildShipNoCost (buildShipType);
	}

	public void UpgradeSelected(int values, GameObject clickedButton, Transform parent)
	{
		if (values > 0) {
			Upgrade.EUpgrade[] availUpgrades = selectedObj.GetComponent<Upgrade> ().AvaibleUpgrades;
			Upgrade.EUpgrade[] installedUpgrades = selectedObj.GetComponent<Upgrade> ().UsedUpgrades;

			Upgrade.EUpgrade selectedUpgrade = availUpgrades [values - 1];

			selectedObj.GetComponent<Upgrade> ().LevelUp (selectedUpgrade);

			UpdateUpgrades (availUpgrades, installedUpgrades, parent);	
		}
	}

	public void UpdateUpgrades(Upgrade.EUpgrade[] availUpgrades, Upgrade.EUpgrade[] installedUpgrades, Transform upgradePane)
	{
		foreach (Transform child in upgradePane) {
			Destroy (child.gameObject);
		}
		
		for (int i = 0; i < selectedObj.GetComponent<Upgrade> ().GetNumMaxUpgrades(); i++) {
			if (installedUpgrades[i] != Upgrade.EUpgrade.None) {
				var uText = Instantiate (UpgradeText);
				uText.GetComponent<Text> ().text = installedUpgrades [i].ToString ();
				uText.transform.SetParent (upgradePane.transform);
			} else {
				var uDropDown = Instantiate (UpgradeDropdown);
				uDropDown.transform.SetParent (upgradePane.transform);
				List<string> upgrades = new List<string> ();
				upgrades.Add (Upgrade.EUpgrade.None.ToString());
				foreach (var upgrade in availUpgrades) {
					upgrades.Add (upgrade.ToString ());
				}
				uDropDown.GetComponent<Dropdown> ().AddOptions(upgrades);
				uDropDown.GetComponent<Dropdown> ().onValueChanged.AddListener( (eventData) => { UpgradeSelected(eventData, uDropDown, upgradePane); });
			}
		}
	}

	public void OpenPanelForObject(GameObject obj)
	{
		selectedObj = obj;
		if (selectedObj.layer == 12 || selectedObj.layer == 13) {
			ShowBuilPanel ();
		} else {
			ShowUpgadePanel ();		
		}
	}

	public void ShowBuilPanel() {
		GameObject buildPaneInst = Instantiate (BuildPanePrefab);
		buildPaneInst.transform.SetParent (InteractionPane.transform);
		buildPaneInst.GetComponent<RectTransform> ().offsetMax = new Vector2(0, 0);
		buildPaneInst.GetComponent<RectTransform> ().offsetMin = new Vector2(0, 0);

		selectedObj.layer = 12;

		if (selectedObj.layer == 12) // 12 Building 
		{
			foreach (var buildable in Setting.BUILDABLE_BUILDINGS) {
				Button buildButtonInst = Instantiate (BuildButtonPrefab);
				buildButtonInst.transform.SetParent (buildPaneInst.transform);
				buildButtonInst.GetComponentInChildren<Text> ().text = buildable.ToString ();
				switch (buildable) {
					case BuildManager.BuildingObject.Mine:
						buildButtonInst.onClick.AddListener (() => { BuildBuilding(selectedObj, BuildManager.BuildingObject.Mine); });
						break;
					case BuildManager.BuildingObject.PowerPlant:
						buildButtonInst.onClick.AddListener (() => { BuildBuilding(selectedObj, BuildManager.BuildingObject.PowerPlant); });
						break;
					case BuildManager.BuildingObject.Shipyard:
						buildButtonInst.onClick.AddListener (() => { BuildBuilding(selectedObj, BuildManager.BuildingObject.Shipyard); });
						break;
					case BuildManager.BuildingObject.Workshop:
						buildButtonInst.onClick.AddListener (() => { BuildBuilding(selectedObj, BuildManager.BuildingObject.Workshop); });
						break;
				}
			}
		} else if (selectedObj.layer == 13) // 13 Tower
		{
			foreach (var buildable in Setting.BUILDABLE_TOWER) {
				Button buildButtonInst = Instantiate (BuildButtonPrefab);
				buildButtonInst.transform.SetParent (buildPaneInst.transform);
				buildButtonInst.GetComponentInChildren<Text> ().text = buildable.ToString ();
				switch (buildable) {
					case BuildManager.BuildingObject.TeslaTower:
						buildButtonInst.onClick.AddListener (() => { BuildBuilding(selectedObj, BuildManager.BuildingObject.TeslaTower); });
						break;
					case BuildManager.BuildingObject.ArtilleryTower:
						buildButtonInst.onClick.AddListener (() => { BuildBuilding(selectedObj, BuildManager.BuildingObject.ArtilleryTower); });
						break;
				}
			}
		}
	}

	public void BuildBuilding(GameObject selectedObj, BuildManager.BuildingObject buildType) {
		BuildManager.Instance.TryPlaceBuilding (buildType, selectedObj.transform);
	}

	void Update() {
		MassText.text = PlayerManager.Instance.GetResources ().Matter.ToString();
		EnergyText.text = PlayerManager.Instance.GetResources ().Energy.ToString();
		MashineText.text = PlayerManager.Instance.GetResources ().Engine.ToString();
	}
}