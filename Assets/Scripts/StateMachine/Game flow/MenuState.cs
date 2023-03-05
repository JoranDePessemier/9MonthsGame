using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    private MainMenuView _menuView;

    public MenuState(MainMenuView menuView)
    {
        _menuView = menuView;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _menuView.gameObject.SetActive(true);
        _menuView.PlayClicked += FadeMenu;

    }

    public override void OnExit()
    {
        base.OnExit();
        _menuView.gameObject.SetActive(true);
        _menuView.PlayClicked -= FadeMenu;
    }

    private void FadeMenu(object sender, EventArgs e)
    {
        _menuView.BackGround.FadeOut();
        FadeMusic();
        _menuView.BackGround.FadeComplete += NextScene;
    }

    private void FadeMusic()
    {
        _menuView.FadeInMusic();
    }

    private void NextScene(object sender, EventArgs e)
    {
        _menuView.NextSection();
    }



}
