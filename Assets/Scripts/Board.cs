using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    // ���ӵĸ������飨��ʱ�߼���9��д��
    // todo:��̬���ɵ�ͼ�������ֶ���9�����ӣ�����view�㺬����
    // public ChessGrid[] chessGrids = new ChessGrid[GameRule.Instance.HGridNum * GameRule.Instance.VGridNum];
    public ChessGrid[] chessGrids = new ChessGrid[9];
    public ChessGrid baseGrid;

    [SerializeField] private TextMeshProUGUI textWinner;
    private string winner;
    public Button restartButton;
    public Button quitButton;

    [SerializeField] private Transform Transform;

    // ������������todo:�Ƿ���Ե���gameruleʵ�֣���
    public int chessCount;

    void Start()
    {
        chessCount = 0;
        chessGrids = GetComponentsInChildren<ChessGrid>();
        restartButton.onClick.AddListener(ReStart);
        quitButton.onClick.AddListener(Quit);
    }

    // ���ʱ���ã��������+���ݲ�䶯��todo���ɿ���ͬ����model�㴦��
    public void OnChessGridClicked(ChessGrid chessGrid)
    {
        chessCount++;
        if (CheckIfWin())
        {
            GameOver();
            return;
        }
        else if (chessCount == GameRule.Instance.HGridNum * GameRule.Instance.VGridNum)   // ûӮ��������
        {
            winner = "Nobody";
            GameOver();
            return;
        }
        GameRule.Instance.SwitchTurn();  //�������󻻱�
    }

    // ����Ƿ��ʤ��todo:�ж��㷨��Ҫ������չ��Ч�ʣ���ʱ��ֻд��3*3���д��Ķ���
    public bool CheckIfWin()
    {
        return ChessMatch(0, 1, 2) || ChessMatch(3, 4, 5) || ChessMatch(6, 7, 8) ||
               ChessMatch(0, 3, 6) || ChessMatch(1, 4, 7) || ChessMatch(2, 5, 8) ||
               ChessMatch(0, 4, 8) || ChessMatch(2, 4, 6);
    }
    // ����ƥ���飬�Ƿ�������
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

    // ������Ϸ,��ʾʤ��
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
