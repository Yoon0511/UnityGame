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
public enum PLAYER_ANI_STATE
{
    NONE,
    IDLE,
    WALK,
    RUN,
    ATTACK,
    DIE,
    SKILL_SLASH,
    SKILL_BUFF,
    SKILL_SPELLCAST,
    SKILL_RUSH,
    GUARD,
    ENUM_END
}
public enum BUFF_TYPE
{
    HP,
    MP,
    SPEED,
    ATK,
    DEF,
    SHIELD,
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
    RUN,
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
    MAXEXP,
    LEVEL,
    CRITICAL_CANCE,
    CRITICAL_MULTIPLIER,
    SHIELD,
    ENUM_END
}

public enum SKILL_TYPE
{
    NONE,
    ACTIVE,
    PASSIVE,
    BUFF,
    ENUM_NED
}

public enum DRAGON_STATE
{
    NONE,
    IDLE,
    MOVE,
    ATTACK,
    DIE,
    ENUM_END
}

public enum DRAGON_ANI_STATE
{
    NONE,
    IDLE,
    FORWARD_MOVE,
    BITE_ATTACK,
    PROJECTILE_ATTACK,
    CAST_SPELL,
    BREATH,
    DIE,
    ENUM_END
}

public enum DRAGON_SKILL
{
    BREATH,
    RUSH,
    FALLING_ROCK,
    ROAR,
    PROJECTILE,
    FIRESTOME,
    ENUM_END
}

public enum CHARACTER_TYPE
{
    NONE,
    PLAYER,
    MONSTER,
    NPC,
    ENUM_END
}

public enum QUEST_TYPE
{
    NONE,
    HUNTING,
    COMMON,
    STORE,
    MEETING,
    ENUM_END
}

public enum SKILL_STATE
{
    NONE,
    READY,
    RUNNING,
    END,
    ENUM_END
}

public enum DEBUFF_TYPE
{
    NONE,
    STUN,
    BLEED,
    ENUM_END
}

public enum DAMAGEFONT_TYPE
{
    NONE,
    CRITICAL,
    GREEN,
    YELLOW,
    ENUM_END
}

public enum ATKRANGE_TYPE
{
    NONE,
    RECT,
    CIRCLE,
    CONE,
    ENUM_END
}

public enum QUESTUI_OPNE_TYPE
{
    NONE,
    CANSTART_QUEST,
    PROGRESS_QUEST,
    COMPLETE_QUEST,
    ENUM_END
}

public enum MONSTER_ID
{
    NONE,
    GOLEM,
    DRAGON,
    WOLF,
    ENUM_END
}

public enum QUEST_REWARD_TYPE
{
    NONE,
    EXP,
    GOLD,
    ENUM_END
}

public enum SYSTEM_MSG_TYPE
{
    NONE,
    UI,
    QUEST_COMPLETE,
    ENUM_END
}