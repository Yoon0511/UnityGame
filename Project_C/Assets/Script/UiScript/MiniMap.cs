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
        
        // 플레이어 위치를 Terrain 기준으로 정규화 (0~1)
        float normX = Mathf.InverseLerp(TerrainPos.x, TerrainPos.x + TerrainSize.x, playerWorldPos.x);
        float normY = Mathf.InverseLerp(TerrainPos.z, TerrainPos.z + TerrainSize.z, playerWorldPos.z);

        // 미니맵 이미지 내 위치 계산 (좌하단 기준)
        float mapWidth = MiniMapImg.rect.width;
        float mapHeight = MiniMapImg.rect.height;

        float iconX = normX * mapWidth - mapWidth / 2f;
        float iconY = normY * mapHeight - mapHeight / 2f;

        // 플레이어 아이콘 위치 이동
        PlayerIcon.localPosition = new Vector2(iconX, iconY);

        // 미니맵을 반대로 이동해서 아이콘이 중심에 오게 함 (확대비율적용)
        MiniMapImg.localPosition = -PlayerIcon.localPosition * Zoom;

        // 미니맵 확대
        MiniMapImg.localScale = Vector3.one * Zoom;

        // 미니맵 회전
        float playerYAngle = Shared.GameMgr.CAMERAMOVE.transform.eulerAngles.y;
        MiniMapRoation.localRotation = Quaternion.Euler(0, 0, playerYAngle);
    }
}
