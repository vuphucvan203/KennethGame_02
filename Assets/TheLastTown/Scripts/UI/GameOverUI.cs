using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : KennMonoBehaviour
{
    [SerializeField] protected RectTransform panel;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        panel = transform.Find("Panel").GetComponent<RectTransform>();
    }

    private void Update()
    {
        bool gameOver = Player.Instance.controller.Soldier.isDead;
        //if (Player.Instance.controller.Soldier.isDead)
        //{
        //    panel.gameObject.SetActive(true);
        //}
        //else panel.gameObject.SetActive(false);
    }
}
