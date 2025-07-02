using UnityEngine;

public enum STATE
{
    NONE,
    IDLE,
    WALK,
    RUN,
    ATTACK,
    DIE,
    RIDING,
    AUTOMOVE,
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
    RIDING,
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
    PETITEM,
    ENUM_END
}

public enum EQUITMENT_TYPE
{
    NONE,
    WEAPON,
    AMOR,
    ACCESSORIES,
    PET,
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

public enum TREANT_STATE
{
    NONE,
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
public enum TREANT_ANI_STATE
{
    NONE,
    RUN,
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
    SPWAN,
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

public enum QUEST_STATE
{
    NONE,
    START,
    PROGRESS,
    COMPLETE,
    END,
    ENUM_END,
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
    TREANT,
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

public enum RIDING_STATE
{
    NONE,
    IDLE,
    WALK,
    RUN,
    ENUM_END
}

public enum SERIVE_TYPE
{
    NONE,
    ENHANCE,
    STORE,
    ENUM_END
}

public enum POTION_TYPE
{
    NONE,
    HP_POTION,
    MP_POTION,
    ENUM_END
}

public enum ITEM_GRADE
{
    NONE,
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY,
    ENUM_END
}

public enum SCENE
{
    TITLE,
    LOADING,
    INGAME,
    ENUM_END
}

public enum PLAYER_SKILL_ID
{
    NONE,
    POISON,
    POWERUP,
    SLASH,
    RUSH,
    FALLINGROCK,
    SELFHEAL,
    SHIELD,
    RIDING,
    ENUM_END
}

public enum SOUNDEFECT
{
    NONE,

    ENUM_END
}