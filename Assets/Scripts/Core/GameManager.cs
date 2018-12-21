using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    public GameObject UnitSelection;

    void Awake () {
        if (instance == null)
            instance = this;

        GameObject UnitSelectionGO = (GameObject)Instantiate(UnitSelection);
        UnitSelectionGO.transform.parent = gameObject.transform;

    }
}
