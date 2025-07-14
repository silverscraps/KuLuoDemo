using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessGrid : MonoBehaviour
{
    public Button currentButton;    // ��ǰ�ű����ڵİ�ť������
    public Image currentImage;      // ��ǰ�ű����ڵ�ͼƬ������
    public Chess chess;             // ��ǰ���ӵ�����
    public bool isChessed;          // ��ǰ�����Ƿ�����


    void Start()
    {
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(DrawChess);  // �󶨻��ƺ���
        currentImage = GetComponent<Image>();
        isChessed = false;
    }

    // ����ʱ�Ļ��ƺ���
    public void DrawChess()
    {
        // �������ӻ���Ϸ�ѽ�����ֱ�ӷ��أ���������
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
        // �������̶�Ӧ�ĵ��Ч��
        Board board = GetComponentInParent<Board>();
        board.OnChessGridClicked(this);
    }

    // �ؿ���Ϸʱ��ո���Ⱦ�ذ�ɫ
    public void ReStart()
    {
        isChessed = false;
        chess = Chess.None;
        currentImage.sprite = null;
        currentImage.color = new Color(1, 1, 1, 1);
    }
}
