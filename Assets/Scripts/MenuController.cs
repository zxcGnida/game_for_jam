using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController instance { get; private set; }

    public TextMeshProUGUI metalText;
    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI fuelInShip;
    public TextMeshProUGUI metalInShip;
    public TextMeshProUGUI timeTillNextWave;

    GameObject _menu;
    bool _menuIsActive;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        EventManager.onValueChanged.AddListener(ChangeText);
        metalInShip.text = "Metal stored: " + Inventory.metalInShip;
        fuelInShip.text = "Fuel stored: " + Inventory.fuelInShip;
    }

    private void OnDestroy()
    {
        EventManager.onValueChanged.RemoveListener(ChangeText);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_menuIsActive)
            {
                _menu.SetActive(true);
                Time.timeScale = 0f;
                _menuIsActive = true;
                return;
            }
            _menu.SetActive(false);
            _menuIsActive = false;
            Time.timeScale = 1f;
        }
    }

    void ChangeText()
    {
        metalText.text = "Metal: " + Inventory.metalInHands;
        fuelText.text = "Fuel: " + Inventory.fuelInHands;
        metalInShip.text = "Metal stored: " + Inventory.metalInShip;
        fuelInShip.text = "Fuel stored: " + Inventory.fuelInShip;
        timeTillNextWave.text = "Next wave in " + PlanetGameManager.instance.timeTillEnemies.ToString("0.0");
    }
}
