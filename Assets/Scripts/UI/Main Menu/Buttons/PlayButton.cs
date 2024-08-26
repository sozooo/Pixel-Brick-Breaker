using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MenuButton
{
    [SerializeField] private MainMenu _mainMenu;

    protected override void Iteract()
    {
        _mainMenu.LoadScene();
    }
}
