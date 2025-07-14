using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    // 落子的格子数组（暂时逻辑按9个写）
    // todo:动态生成地图，避免手动搓9个板子，避免view层含数据
    // public ChessGrid[] chessGrids = new ChessGrid[GameRule.Instance.HGridNum * GameRule.Instance.VGridNum];
    public ChessGrid[] chessGrids = new ChessGrid[9];
    public ChessGrid baseGrid;

    [SerializeField] private TextMeshProUGUI textWinner;
    private string winner;
    public Button restartButton;
    public Button quitButton;

    [SerializeField] private Transform Transform;

    // 已落子数量（todo:是否可以调到gamerule实现？）
    public int chessCount;

    void Start()
    {
        chessCount = 0;
        chessGrids = GetComponentsInChildren<ChessGrid>();
        restartButton.onClick.AddListener(ReStart);
        quitButton.onClick.AddListener(Quit);
    }

    // 点击时调用，界面调整+数据层变动（todo：可考虑同样在model层处理）
    public void OnChessGridClicked(ChessGrid chessGrid)
    {
        chessCount++;
        if (CheckIfWin())
        {
            GameOver();
            return;
        }
        else if (chessCount == GameRule.Instance.HGridNum * GameRule.Instance.VGridNum)   // 没赢但是满了
        {
            winner = "Nobody";
            GameOver();
            return;
        }
        GameRule.Instance.SwitchTurn();  //点击结算后换边
    }

    // 检查是否获胜（todo:判断算法需要考虑拓展与效率，赶时间只写了3*3，有待改动）
    public bool CheckIfWin()
    {
        return ChessMatch(0, 1, 2) || ChessMatch(3, 4, 5) || ChessMatch(6, 7, 8) ||
               ChessMatch(0, 3, 6) || ChessMatch(1, 4, 7) || ChessMatch(2, 5, 8) ||
               ChessMatch(0, 4, 8) || ChessMatch(2, 4, 6);
    }
    // 棋子匹配检查，是否三连子
    bool ChessMatch(int i, int j, int k)
    {
        if (chessGrids[i].chess == chessGrids[j].chess && chessGrids[j].chess == chessGrids[k].chess)
        {
            if (chessGrids[k].chess == GameRule.Instance.playerChess)
            {
                winner = "player";
                return true;
            }
            else if (chessGrids[k].chess == GameRule.Instance.aiChess)
            {
                winner = "ai";
                return true;
            }
        }
        return false;
    }

    // 结束游戏,显示胜者
    public void GameOver()
    {
        textWinner.text = $"{winner} is win!";
        GameRule.Instance.isGameOver = true;
        for (int i = 0; i < chessGrids.Length; i++)
        {
            chessGrids[i].isChessed = true;
        }
    }

    public void ReStart()
    {
        chessCount = 0;
        GameRule.Instance.isGameOver = false;
        for (int i = 0; i < chessGrids.Length; i++)
        {
            chessGrids[i].ReStart();
        }
        textWinner.text = "";
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
