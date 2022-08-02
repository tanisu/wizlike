public enum TILE_TYPE
{
    GROUND,
    WALL,
    PLAYER
}

public enum DIRECTION
{
    TOP,
    RIGHT,
    BOTTOM,
    LEFT,
    MAX
}

public enum WALL_TYPE
{
    NONE,               //0
    TOP,                //1
    RIGHT,              //2
    BOTTOM,             //3
    LEFT,               //4
    TOP_RIGHT,          //5
    RIGHT_BOTTOM,       //6
    BOTTOM_LEFT,        //7
    LEFT_TOP,           //8
    TOP_BOTTOM,         //9
    RIGHT_LEFT,         //a
    RIGHT_BOTTOM_LEFT,  //b
    TOP_BOTTOM_LEFT,    //c
    TOP_RIGHT_LEFT,     //d
    TOP_RIGHT_BOTTOM,   //e
    TOP_RIGHT_BOTTOM_LEFT                 //f
}

public enum WALL_CATE
{
    WALL_CATE_NONE,
    WALL_CATE_WALL,
    WALL_CATE_DOOR,
    WALL_CATE_MAX
}

public enum EventType
{
    NONE,
    STAIRS,
    TRAPS,
    MESSAGES,
    WARP,
    SHOP
}

public enum ShopType
{
    STORE,
    INN,
    BAR,
    CASTLE
}

public enum State
{
    WAIT,
    MOVE,
    BATTLE,
    SHOPPING
}



public enum Jobs
{
    FIG,
    MAG,
    PRI,
    THI,
    BIS,
    SAM,
    NIN,
    BIR,
    MAX
}