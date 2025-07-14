using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessGrid : MonoBehaviour
{
    public Button currentButton;    // 当前脚本所在的按钮的引用
    public Image currentImage;      // 当前脚本所在的图片的引用
    public Chess chess;             // 当前格子的棋子
    public bool isChessed;          // 当前格子是否落子


    void Start()
    {
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(DrawChess);  // 绑定绘制函数
        currentImage = GetComponent<Image>();
        isChessed = false;
    }

    // 落子时的绘制函数
    public void DrawChess()
    {
        // 已有落子或游戏已结束，直接返回，不做绘制
        if (isChessed || GameRule.Instance.isGameOver) return;

        if (GameRule.Instance.isPlayerTurn)
        {
            currentImage.sprite = GameRule.Instance.playerChessSprite;
            currentImage.color = new Color(1, 1, 1, 1);
            chess = GameRule.Instance.playerChess;
        }
        else
        {
            currentImage.sprite = GameRule.Instance.aiChessSprite;
            currentImage.color = new Color(1, 1, 1, 1);
            chess = GameRule.Instance.aiChess;
        }
        isChessed = true;
        // 触发棋盘对应的点击效果
        Board board = GetComponentInParent<Board>();
        board.OnChessGridClicked(this);
    }

    // 重开游戏时清空格子染回白色
    public void ReStart()
    {
        isChessed = false;
        chess = Chess.None;
        currentImage.sprite = null;
        currentImage.color = new Color(1, 1, 1, 1);
    }
}
