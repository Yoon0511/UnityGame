using UnityEngine;

public enum STATE
{
    NONE,
    IDLE,
    WALK,
    RUN,
    ATTACK,
    DIE,
    ENUM_END
}

public enum BUFF_TYPE
{
    HP,
    MP,
    SPEED,
    ATK,
    DEF,
    ENUM_END
}

public enum ITEM_TYPE
{
    NONE,
    EQUIPMENT,
    USE,
    ENUM_END
}

public enum EQUITMENT_TYPE
{
    NONE,
    WEAPON,
    AMOR,
    ACCESSORIES,
    ENUM_END
}

public enum MONSTER_STATE
{
    NONE,
    IDLE,
    MOVE,
    PATROL,
    CHASE,
    ATTACK,
    DIE,
    ENUM_END
}

public enum MONSTER_ANI_STATE
{
    NONE,
    IDLE,
    MOVE,
    ATTACK,
    DIE,
    ENUM_END
}
public enum STAT_TYPE
{
    NONE,
    HP,
    MP,
    SPEED,
    ATK,
    DEF,
    MAXHP,
    MAXMP,
    EXP,
    LEVEL,
    ENUM_END
}

public enum SKILL_TYPE
{
    NONE,
    ACTIVE,
    PASSIVE,
    ENUM_NED
}