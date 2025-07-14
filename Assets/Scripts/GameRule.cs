using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRule : MonoBehaviour
{
    // 地图宽高各几格（todo：未来有拓展空间，如直接调整棋盘）
    public int HGridNum = 3;
    public int VGridNum = 3;

    public bool isPlayerTurn;
    public Chess playerChess;
    public Chess aiChess;
    public Sprite playerChessSprite;    // 玩家的棋子图标
    public Sprite aiChessSprite;        // 对手（ai）的棋子图标

    public bool isGameOver;

    public Board board;                 // 棋盘引用（view层）

    public static GameRule Instance { get; private set; } 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        isPlayerTurn = true;
        isGameOver = false;
        board = FindObjectOfType<Board>();
    }

    // 切换回合
    public void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        // 如果是 AI 的回合，调用 AI 落子逻辑
        if (!isPlayerTurn && !isGameOver)
        {
            // 可考虑添加一点delay，但期间要屏蔽用户点击
            AIPlay();
        }
    }

    // 获取没落子的空位置(若调为非3*3，循环消耗可能会有问题）
    private ChessGrid[] GetEmptyChessGrids()
    {
        // 获取所有 ChessGrid
        ChessGrid[] allGrids = FindObjectsOfType<ChessGrid>();

        // 过滤出空的 ChessGrid
        List<ChessGrid> emptyGrids = new List<ChessGrid>();
        foreach (ChessGrid grid in allGrids)
        {
            if (grid.chess == Chess.None)
            {
                emptyGrids.Add(grid);
            }
        }

        return emptyGrids.ToArray();
    }

    // AI行为
    private void AIPlay()
    {
        ChessGrid[] emptyGrids = GetEmptyChessGrids();

        // 以下算法规则：能赢则赢，其次能堵则堵，最次随机，避免不败下法（todo：应该有更优秀的算法可以拓展到n*n）
        if (emptyGrids.Length > 0)
        {
            // 检查AI方是否有可以立即获胜的位置
            ChessGrid winningGrid = FindWinningGrid(aiChess, emptyGrids);
            if (winningGrid != null)
            {
                winningGrid.DrawChess();
                return;
            }

            // 检查玩家是否有即将获胜的位置
            ChessGrid blockingGrid = FindWinningGrid(playerChess, emptyGrids);
            if (blockingGrid != null)
            {
                blockingGrid.DrawChess();
                return;
            }

            // 随机选择一个空的位置
            int randomIndex = Random.Range(0, emptyGrids.Length);
            ChessGrid selectedGrid = emptyGrids[randomIndex];
            selectedGrid.DrawChess();
        }
    }

    // 寻找可以获胜的格子
    private ChessGrid FindWinningGrid(Chess targetChess, ChessGrid[] emptyGrids)
    {
        foreach (ChessGrid grid in emptyGrids)
        {
            // 确定势力并模拟下一落子
            grid.chess = targetChess;

            // 检查是否获胜
            if (board.CheckIfWin())
            {
                // 恢复空状态
                grid.chess = Chess.None;
                return grid;
            }

            // 恢复空状态
            grid.chess = Chess.None;
        }

        return null;
    }




}
