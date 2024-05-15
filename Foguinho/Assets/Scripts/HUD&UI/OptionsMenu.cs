using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public bool optionsMenuIsOpen = false;
    public void OnOptions()
    {
        gameObject.SetActive(!optionsMenuIsOpen);
        optionsMenuIsOpen = !optionsMenuIsOpen;
    }
}
