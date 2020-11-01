using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Link : MonoBehaviour
{
    public string link;

    public void OpenLink()
    {
        Application.OpenURL(link);
    }


}
