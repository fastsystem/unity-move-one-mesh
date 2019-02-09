using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui : MonoBehaviour
{
    public GameObject GameObject1;
    public GameObject GameObject2;

    public void OnGUI()
    {
        GUI.color = (this.GameObject1.activeSelf) ? Color.white : Color.black;
        var cmv = GUI.Button(new Rect(Screen.width - 150 - 10, 10, 150, 25), "Create One Mesh");
        GUI.color = (this.GameObject2.activeSelf) ? Color.white : Color.black;
        var pv = GUI.Button(new Rect(Screen.width - 150 - 10, 40, 150, 25), "Create Many Object");

        if (cmv)
        {
            this.GameObject1.SetActive(true);
            this.GameObject2.SetActive(false);
        }

        if (pv)
        {
            this.GameObject1.SetActive(false);
            this.GameObject2.SetActive(true);
        }
    }
}
