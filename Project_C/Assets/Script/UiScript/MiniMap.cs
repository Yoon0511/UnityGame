using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public RectTransform MiniMapImg;
    public Terrain Terrain;

    public RectTransform MiniMapRoation;
    public RectTransform PlayerIcon;
    GameObject Player;

    Vector3 TerrainSize;
    Vector3 TerrainPos;

    public float Zoom;
    // Start is called before the first frame update
    void Start()
    {
        Player = Shared.GameMgr.PLAYEROBJ;

        TerrainSize = Terrain.terrainData.size;
        TerrainPos = Terrain.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdatePlayerIcon();
    }

    void UpdatePlayerIcon()
    {
        Vector3 playerWorldPos = Player.transform.position;
        
        // �÷��̾� ��ġ�� Terrain �������� ����ȭ (0~1)
        float normX = Mathf.InverseLerp(TerrainPos.x, TerrainPos.x + TerrainSize.x, playerWorldPos.x);
        float normY = Mathf.InverseLerp(TerrainPos.z, TerrainPos.z + TerrainSize.z, playerWorldPos.z);

        // �̴ϸ� �̹��� �� ��ġ ��� (���ϴ� ����)
        float mapWidth = MiniMapImg.rect.width;
        float mapHeight = MiniMapImg.rect.height;

        float iconX = normX * mapWidth - mapWidth / 2f;
        float iconY = normY * mapHeight - mapHeight / 2f;

        // �÷��̾� ������ ��ġ �̵�
        PlayerIcon.localPosition = new Vector2(iconX, iconY);

        // �̴ϸ��� �ݴ�� �̵��ؼ� �������� �߽ɿ� ���� �� (Ȯ���������)
        MiniMapImg.localPosition = -PlayerIcon.localPosition * Zoom;

        // �̴ϸ� Ȯ��
        MiniMapImg.localScale = Vector3.one * Zoom;

        // �̴ϸ� ȸ��
        float playerYAngle = Shared.GameMgr.CAMERAMOVE.transform.eulerAngles.y;
        MiniMapRoation.localRotation = Quaternion.Euler(0, 0, playerYAngle);
    }
}
