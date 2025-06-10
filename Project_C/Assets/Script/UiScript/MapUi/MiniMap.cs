using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public RectTransform MiniMapImg;
    public Terrain Terrain;

    public RectTransform MiniMapRoation;
    public RectTransform PlayerIcon;
    public RectTransform MiniMapUi;
    protected GameObject Player;

    Vector3 TerrainSize;
    Vector3 TerrainPos;

    public float Zoom;

    [SerializeField]
    List<Monster> ListMonster;
    List<NPC> ListNPC;
    RectTransform playerBox;

    protected bool IsRotate = true;
    protected bool IsZoom = true;
    void CreatePlayerCenterBox()
    {
        GameObject boxObj = new GameObject("PlayerCenterBox", typeof(RectTransform), typeof(Image));
        boxObj.transform.SetParent(MiniMapImg, false);

        playerBox = boxObj.GetComponent<RectTransform>();
        playerBox.name = "PlayerCenterBox";

        Image img = boxObj.GetComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 0f); // ����

        playerBox.anchorMin = new Vector2(0.5f, 0.5f);
        playerBox.anchorMax = new Vector2(0.5f, 0.5f);
        playerBox.pivot = new Vector2(0.5f, 0.5f);
    }
    void Start()
    {
        TerrainSize = Terrain.terrainData.size;
        TerrainPos = Terrain.transform.position;

        ListMonster = Shared.GameMgr.GetListMonster();
        ListNPC = Shared.GameMgr.GetListNPC();

        CreatePlayerCenterBox();
    }

    public virtual void Init(GameObject _player)
    {
        Player = _player;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MiniMapUpdate();
    }

    public void MiniMapUpdate()
    {
        if(Player == null)
        {
            return;
        }

        UpdatePlayerIcon();
        UpdateMonsterIcon();
        UpdateNPCIcon();
        UpdatePlayerCenterBox();
    }

    protected void UpdatePlayerIcon()
    {
        Vector3 playerWorldPos = Player.transform.position;

        // �÷��̾� ������ ��ġ �̵�
        PlayerIcon.localPosition = TransMiniMapPos(playerWorldPos);

        // �̴ϸ��� �ݴ�� �̵��ؼ� �������� �߽ɿ� ���� �� (Ȯ���������)
        MiniMapImg.localPosition = -PlayerIcon.localPosition * Zoom;

        // �̴ϸ� Ȯ��
        if(IsZoom)
        {
            MiniMapImg.localScale = new Vector3(Zoom, Zoom, 1);
        }

        // �̴ϸ� ȸ��
        if(IsRotate)
        {
            float playerYAngle = Shared.GameMgr.CAMERAMOVE.transform.eulerAngles.y;
            MiniMapRoation.localRotation = Quaternion.Euler(0, 0, playerYAngle);
        }
    }

    Vector2 TransMiniMapPos(Vector3 _pos)
    {
        // �÷��̾� ��ġ�� Terrain �������� ����ȭ (0~1)
        float normX = Mathf.InverseLerp(TerrainPos.x, TerrainPos.x + TerrainSize.x, _pos.x);
        float normY = Mathf.InverseLerp(TerrainPos.z, TerrainPos.z + TerrainSize.z, _pos.z);

        // �̴ϸ� �̹��� �� ��ġ ��� (���ϴ� ����)
        float mapWidth = MiniMapImg.rect.width;
        float mapHeight = MiniMapImg.rect.height;

        float iconX = normX * mapWidth - mapWidth / 2f;
        float iconY = normY * mapHeight - mapHeight / 2f;

        return new Vector2(iconX, iconY);
    }

    protected void UpdateNPCIcon()
    {
        if (ListNPC == null || ListNPC.Count == 0)
        {
            //ListMonster = Shared.GameMgr.GetListMonster();
            return;
        }
        for (int i = 0; i < ListNPC.Count; ++i)
        {
            CharcaterIconUpdate(ListNPC[i]);
        }
    }

    void UpdateMonsterIcon()
    {
        if (ListMonster == null || ListMonster.Count == 0)
        {
            //ListMonster = Shared.GameMgr.GetListMonster();
            return;
        }

        for(int i = 0;i<ListMonster.Count;++i)
        {
            CharcaterIconUpdate(ListMonster[i]);
        }
    }

    bool IsInsideMiniMap(Vector2 iconLocalPos)
    {
        if (playerBox == null) return true;

        Vector2 boxPos = playerBox.localPosition;
        Vector2 halfSize = playerBox.sizeDelta / 2f;

        bool outsideX = iconLocalPos.x < boxPos.x - halfSize.x || iconLocalPos.x > boxPos.x + halfSize.x;
        bool outsideY = iconLocalPos.y < boxPos.y - halfSize.y || iconLocalPos.y > boxPos.y + halfSize.y;

        return outsideX || outsideY; // �ϳ��� ����� true (���� ��)
    }
    protected void UpdatePlayerCenterBox()
    {
        if (playerBox == null) return;

        // Ȯ�� ���� �ݿ�
        float width = MiniMapUi.rect.width / Zoom;
        float height = MiniMapUi.rect.height / Zoom;

        playerBox.sizeDelta = new Vector2(width, height);
        playerBox.localPosition = PlayerIcon.localPosition; // �߽��� �÷��̾� ��ġ��
    }

    void CharcaterIconUpdate(Character _char)
    {
        Vector2 pos = TransMiniMapPos(_char.transform.position);
        //Character character = ListMonster[i].GetComponent<Character>();
        MiniMapIcon icon = _char.GetMiniMapIcon(this);
        if (!IsInsideMiniMap(pos))
        {
            _char.ShowMiniMapIcon = true;
            if (icon != null)
            {
                icon.SetPos(pos);
            }
            else
            {
                GameObject iconObj = Shared.PoolMgr.GetObject("MiniMapIcon");
                iconObj.transform.SetParent(MiniMapImg.transform, false);
                MiniMapIcon newIcon = iconObj.GetComponent<MiniMapIcon>();
                _char.SetMiniMapIcon(this, newIcon);
                _char.UpdateMiniMapIcon();
                newIcon.SetPos(pos);
            }
        }
        else
        {
            _char.ShowMiniMapIcon = false;
            _char.RemoveMiniMapIcon(this);
        }
    }

    public void OnOpenWorldMap()
    {
        Shared.UiMgr.WORLDMAPUI.SetActive(true);
    }

    public void OnZoomIn()
    {
        Zoom += 0.3f;
        Zoom = Mathf.Clamp(Zoom, 1.0f, 3.5f);
    }

    public void OnZoomOut()
    {
        Zoom -= 0.3f;
        Zoom = Mathf.Clamp(Zoom, 1.0f, 3.5f);
    }
}
