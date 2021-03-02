using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Этот класс закрывает игру по нажатию кнопки.
/// </summary>
public class CloseButtonScript : MonoBehaviour
{
    public void CloseApp()
    {
        Application.Quit();
    }
}
