using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanManager : MonoBehaviour
{
    public static SokobanManager instance;

    float squareSideLength;

    public GameObject wallPrefab;
    public GameObject cratePrefab;
    public GameObject playerPrefab;
    public GameObject floorPrefab;
    public GameObject markPrefab;

    public GameObject victoryMessage;

    int playerPositionX;
    int playerPositionY;
    SokobanSquare[,] sokobanLevel;

    void Start()
    {
        instance = this;

        squareSideLength = floorPrefab.GetComponent<BoxCollider2D>().size.x;

        LoadLevel(IntersceneMemory.instance.themeIndex);
        DrawLevel();
    }

    public void TryToMove(string direction)
    {
        //Debug.Log("trying to move " + direction);
        if (IsMoveValid(direction))
        {
            //Debug.Log("��� ��������");
            MoveCharacter(direction);            
        }
        else
        {
            //Debug.Log("��� ����������");
        }
        ClearLevel();
        DrawLevel();
        if (IsItWin())
        {
            //���������� �������� ���������
            //� ��������, ��� ����� ������� ��������� ������. �������� ������ ���������, �������� ��������, 
            //������ ����������, � ����� ����� ������ �������� ��� ������.
            //� ��������, ����� ���� �� �������� ����������� ���������� ���������... �� �� ����� �����
            //� ���� ���� ��������, ����� ������ ������ �� ���
            victoryMessage.SetActive(true);
            Destroy(SokobanInputManager.instance);
        }
    }

    bool IsMoveValid(string direction)
    {
        //�������� ���������� ����. ������ ������ � ������ � �� ������� ���� ��� ���������� ���� ���� �� ��� � ������ ����
        //��������� ��������� ���������� ����, � ����� ���������
        //��� ���������� ��� ������ � ����������� �������� �� ������. ��� �� ��� � ���������� �� ��� ������

        if (direction == "right")
        {
            if (playerPositionX + 1 > sokobanLevel.GetUpperBound(0))
            {
                return false;
            }
            if (sokobanLevel[playerPositionX + 1, playerPositionY].isWall)
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX + 1, playerPositionY].isCrate) && 
                    (playerPositionX + 2 > sokobanLevel.GetUpperBound(0)))
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX + 1, playerPositionY].isCrate) &&
                    (sokobanLevel[playerPositionX + 2, playerPositionY].isCrate))
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX + 1, playerPositionY].isCrate) &&
                    (sokobanLevel[playerPositionX + 2, playerPositionY].isWall))
            {
                return false;
            }
        }
        if (direction == "left")
        {
            if (playerPositionX - 1 < 0)
            {
                return false;
            }
            if (sokobanLevel[playerPositionX - 1, playerPositionY].isWall)
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX - 1, playerPositionY].isCrate) &&
                    (playerPositionX - 2 < 0))
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX - 1, playerPositionY].isCrate) &&
                    (sokobanLevel[playerPositionX - 2, playerPositionY].isCrate))
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX - 1, playerPositionY].isCrate) &&
                    (sokobanLevel[playerPositionX - 2, playerPositionY].isWall))
            {
                return false;
            }
        }
        if (direction == "up")
        {
            if (playerPositionY + 1 > sokobanLevel.GetUpperBound(1))
            {
                return false;
            }
            if (sokobanLevel[playerPositionX, playerPositionY + 1].isWall)
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX, playerPositionY + 1].isCrate) &&
                    (playerPositionY + 2 > sokobanLevel.GetUpperBound(1)))
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX, playerPositionY + 1].isCrate) &&
                    (sokobanLevel[playerPositionX, playerPositionY + 2].isCrate))
            {
                return false;
            }
            if ((sokobanLevel[playerPositionX, playerPositionY + 1].isCrate) &&
                    (sokobanLevel[playerPositionX, playerPositionY + 2].isWall))
            {
                return false;
            }
        }
        if (direction == "down")
        {
            if (playerPositionY - 1 < 0)
            {
                //Debug.Log("����� ����� ������");
                return false;
            }
            if (sokobanLevel[playerPositionX, playerPositionY - 1].isWall)
            {
                //Debug.Log("����� �����");
                return false;
            }
            if ((sokobanLevel[playerPositionX, playerPositionY - 1].isCrate) &&
                    (playerPositionY - 2 < 0))
            {
                //Debug.Log("����� ���� � ����� ������");
                return false;
            }
            if ((sokobanLevel[playerPositionX, playerPositionY - 1].isCrate) &&
                    (sokobanLevel[playerPositionX, playerPositionY - 2].isCrate))
            {
                //Debug.Log("����� ��� �����");
                return false;
            }
            if ((sokobanLevel[playerPositionX, playerPositionY - 1].isCrate) &&
                    (sokobanLevel[playerPositionX, playerPositionY - 2].isWall))
            {
                //Debug.Log("����� ���� � �����");
                return false;
            }
        }

        return true;
    }

    void MoveCharacter(string direction)
    {
        //������� ��������� � ������ �����������. � �������� ���� �� ��� ����
        //��� ������, ��� ���������� �������� ��������� ��� � �������
        //����� ���������� ����� ���������� ��� ������ ��������� �������� �����, � ������� - ������� �����
        //����� ������������ ����� ��������� ��������� ��� ���������

        if (direction == "right")
        {
            playerPositionX++;
            if (sokobanLevel[playerPositionX, playerPositionY].isCrate)
            {
                sokobanLevel[playerPositionX + 1, playerPositionY].isCrate = true;
                sokobanLevel[playerPositionX, playerPositionY].isCrate = false;
            }
        }
        if (direction == "left")
        {
            playerPositionX--;
            if (sokobanLevel[playerPositionX, playerPositionY].isCrate)
            {
                sokobanLevel[playerPositionX - 1, playerPositionY].isCrate = true;
                sokobanLevel[playerPositionX, playerPositionY].isCrate = false;
            }
        }
        if (direction == "up")
        {
            playerPositionY++;
            if (sokobanLevel[playerPositionX, playerPositionY].isCrate)
            {
                sokobanLevel[playerPositionX, playerPositionY + 1].isCrate = true;
                sokobanLevel[playerPositionX, playerPositionY].isCrate = false;
            }
        }
        if (direction == "down")
        {
            playerPositionY--;
            if (sokobanLevel[playerPositionX, playerPositionY].isCrate)
            {
                sokobanLevel[playerPositionX, playerPositionY - 1].isCrate = true;
                sokobanLevel[playerPositionX, playerPositionY].isCrate = false;
            }
        }
    }

    bool IsItWin()
    {
        //��������� ��, ���������� �� ������� ������. � ������ - �� ���������� ����� �� ��� �����
        //���� �������� ����� ��� ���� �� �� ����� - ���������� ����, ����� ���.
        foreach (SokobanSquare square in sokobanLevel)
        {
            if ((square.isCrate) != (square.isMarked))
            {
                return false;
            }
        }
        return true;
    }
    
    void ClearLevel()
    {
        //����� ���������� ���������� ������� ��� ������������ �������. 
        foreach(GameObject oldSquare in GameObject.FindGameObjectsWithTag("square"))
        {
            Destroy(oldSquare);
        }
    }

    void DrawLevel()
    {
        Vector3 levelCenter = new Vector3(squareSideLength * sokobanLevel.GetUpperBound(0) / 2, 
            squareSideLength * sokobanLevel.GetUpperBound(1) / 2);

        for (int i = 0; i <= sokobanLevel.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= sokobanLevel.GetUpperBound(1); j++)
            {
                Instantiate(floorPrefab, new Vector3(-levelCenter.x + i * squareSideLength, 
                    -levelCenter.y + j * squareSideLength), floorPrefab.transform.rotation);
            }
            for (int j = 0; j <= sokobanLevel.GetUpperBound(1); j++)
            {
                if (sokobanLevel[i, j].isCrate)
                {
                    Instantiate(cratePrefab, new Vector3(-levelCenter.x + i * squareSideLength, 
                        -levelCenter.y + j * squareSideLength), cratePrefab.transform.rotation);
                }
            }
            for (int j = 0; j <= sokobanLevel.GetUpperBound(1); j++)
            {
                if (sokobanLevel[i, j].isMarked)
                {
                    Instantiate(markPrefab, new Vector3(-levelCenter.x + i * squareSideLength, 
                        -levelCenter.y + j * squareSideLength), markPrefab.transform.rotation);
                }
            }
            for (int j = 0; j <= sokobanLevel.GetUpperBound(1); j++)
            {
                if (sokobanLevel[i, j].isWall)
                {
                    Instantiate(wallPrefab, new Vector3(-levelCenter.x + i * squareSideLength, 
                        -levelCenter.y + j * squareSideLength), wallPrefab.transform.rotation);
                }
            }
        }

        Instantiate(playerPrefab, new Vector3(-levelCenter.x + playerPositionX * squareSideLength,
            -levelCenter.y + playerPositionY * squareSideLength), playerPrefab.transform.rotation);
    }

    void LoadLevel(int levelNumber)
    {
        if (levelNumber == 0)
        {
            playerPositionX = 1;
            playerPositionY = 1;

            sokobanLevel = new SokobanSquare[7, 7];
            sokobanLevel[0, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 2] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 5] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 2] = new SokobanSquare(false, false, true);
            sokobanLevel[2, 3] = new SokobanSquare(false, false, true);
            sokobanLevel[2, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[4, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[4, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 3] = new SokobanSquare(false, true, false);
            sokobanLevel[4, 4] = new SokobanSquare(false, true, false);
            sokobanLevel[4, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 2] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 5] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 6] = new SokobanSquare(true, false, false);
        }

        if (levelNumber == 1)
        {
            playerPositionX = 0;
            playerPositionY = 2;

            sokobanLevel = new SokobanSquare[7, 6];

            sokobanLevel[0, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 2] = new SokobanSquare(false, false, true);
            sokobanLevel[3, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 5] = new SokobanSquare(true, false, false);
            sokobanLevel[4, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[4, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 4] = new SokobanSquare(false, true, false);
            sokobanLevel[4, 5] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[5, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 5] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[6, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 5] = new SokobanSquare(false, false, false);
        }

        if (levelNumber == 2)
        {
            playerPositionX = 0;
            playerPositionY = 5;

            sokobanLevel = new SokobanSquare[8, 7];

            sokobanLevel[0, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[0, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[0, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 2] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[1, 4] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[1, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 2] = new SokobanSquare(true, false, false);
            sokobanLevel[2, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 4] = new SokobanSquare(false, false, true);
            sokobanLevel[2, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[2, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[3, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 4] = new SokobanSquare(false, false, true);
            sokobanLevel[3, 5] = new SokobanSquare(false, false, false);
            sokobanLevel[3, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[4, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[4, 4] = new SokobanSquare(false, false, true);
            sokobanLevel[4, 5] = new SokobanSquare(false, true, false);
            sokobanLevel[4, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 0] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 1] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 2] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 3] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[5, 5] = new SokobanSquare(false, true, false);
            sokobanLevel[5, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[6, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[6, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[6, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[6, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[6, 5] = new SokobanSquare(false, true, false);
            sokobanLevel[6, 6] = new SokobanSquare(true, false, false);
            sokobanLevel[7, 0] = new SokobanSquare(false, false, false);
            sokobanLevel[7, 1] = new SokobanSquare(false, false, false);
            sokobanLevel[7, 2] = new SokobanSquare(false, false, false);
            sokobanLevel[7, 3] = new SokobanSquare(false, false, false);
            sokobanLevel[7, 4] = new SokobanSquare(true, false, false);
            sokobanLevel[7, 5] = new SokobanSquare(true, false, false);
            sokobanLevel[7, 6] = new SokobanSquare(true, false, false);
        }
    }
}

public class SokobanSquare
{
    public bool isWall; // ������ �� ���? ���� �� - ������ �����������, �������� �����. ���� ��� - ���������, ������ ���.
    public bool isMarked; // ����� �� ��� �����, ��� ����� ��������� ���� ����. ���� �� - ������ �����, ���� ��� - ������� ������.
    public bool isCrate; //  ���� �� ��� ����?

    public SokobanSquare(bool inputIsWall, bool inputIsMarked, bool inputIsCrate)
    {
        isWall = inputIsWall;
        isMarked = inputIsMarked;
        isCrate = inputIsCrate;
    }
}