using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRule : MonoBehaviour
{
    // ��ͼ��߸�����todo��δ������չ�ռ䣬��ֱ�ӵ������̣�
    public int HGridNum = 3;
    public int VGridNum = 3;

    public bool isPlayerTurn;
    public Chess playerChess;
    public Chess aiChess;
    public Sprite playerChessSprite;    // ��ҵ�����ͼ��
    public Sprite aiChessSprite;        // ���֣�ai��������ͼ��

    public bool isGameOver;

    public Board board;                 // �������ã�view�㣩

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

    // �л��غ�
    public void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        // ����� AI �Ļغϣ����� AI �����߼�
        if (!isPlayerTurn && !isGameOver)
        {
            // �ɿ������һ��delay�����ڼ�Ҫ�����û����
            AIPlay();
        }
    }

    // ��ȡû���ӵĿ�λ��(����Ϊ��3*3��ѭ�����Ŀ��ܻ������⣩
    private ChessGrid[] GetEmptyChessGrids()
    {
        // ��ȡ���� ChessGrid
        ChessGrid[] allGrids = FindObjectsOfType<ChessGrid>();

        // ���˳��յ� ChessGrid
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

    // AI��Ϊ
    private void AIPlay()
    {
        ChessGrid[] emptyGrids = GetEmptyChessGrids();

        // �����㷨������Ӯ��Ӯ������ܶ���£������������ⲻ���·���todo��Ӧ���и�������㷨������չ��n*n��
        if (emptyGrids.Length > 0)
        {
            // ���AI���Ƿ��п���������ʤ��λ��
            ChessGrid winningGrid = FindWinningGrid(aiChess, emptyGrids);
            if (winningGrid != null)
            {
                winningGrid.DrawChess();
                return;
            }

            // �������Ƿ��м�����ʤ��λ��
            ChessGrid blockingGrid = FindWinningGrid(playerChess, emptyGrids);
            if (blockingGrid != null)
            {
                blockingGrid.DrawChess();
                return;
            }

            // ���ѡ��һ���յ�λ��
            int randomIndex = Random.Range(0, emptyGrids.Length);
            ChessGrid selectedGrid = emptyGrids[randomIndex];
            selectedGrid.DrawChess();
        }
    }

    // Ѱ�ҿ��Ի�ʤ�ĸ���
    private ChessGrid FindWinningGrid(Chess targetChess, ChessGrid[] emptyGrids)
    {
        foreach (ChessGrid grid in emptyGrids)
        {
            // ȷ��������ģ����һ����
            grid.chess = targetChess;

            // ����Ƿ��ʤ
            if (board.CheckIfWin())
            {
                // �ָ���״̬
                grid.chess = Chess.None;
                return grid;
            }

            // �ָ���״̬
            grid.chess = Chess.None;
        }

        return null;
    }




}
