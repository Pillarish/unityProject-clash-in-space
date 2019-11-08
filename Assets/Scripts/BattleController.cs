using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Camera cam;
    public GameObject warriorPrefab;
    public GameObject archerPrefab;
    public AvailableArmy army;
    public string selectedTroop;
    public Text warriorNumber;
    public Text archerNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        updateCount("all");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

                if (army.checkAvailable(selectedTroop)) {

                    instantiatePrefab(selectedTroop, hit);
                    updateCount(selectedTroop);
                }
            }
        }
    }

    private void instantiatePrefab(string minionName, RaycastHit hit)
    {
        switch(minionName) {
            case "warrior":
                Instantiate(warriorPrefab, hit.point, Quaternion.identity);
                break;
            case "archer":
                Instantiate(archerPrefab, hit.point, Quaternion.identity);
                break;
        }
        army.useTroop(minionName);
    }

    public void selectTroop(string minionName)
    {
        // check some kind of whitelist maybe?
        selectedTroop = minionName;
    }

    private void updateCount(string minionName)
    {
        switch(minionName) {
            case "warrior":
                this.warriorNumber.text = "" + army.warrior;
                break;
            case "archer":
                this.archerNumber.text = "" + army.archer;
                break;
            case "all":
                this.warriorNumber.text = "" + army.warrior;
                this.archerNumber.text = "" + army.archer;
                break;
        }
    }
}
