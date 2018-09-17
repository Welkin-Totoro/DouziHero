using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Consts
{
    //Level 目录
    //public static readonly string LevelDir = Application.dataPath + @"\Game\Res\Levels";

    //Args
    public const string GameProgress = "GameProgress";
    public const float DontClosedDistance = 0.1f;
    public const float RangeClosedDistance = 0.7f;

    //Model
    public const string M_GameModel = "M_GameModel";
    public const string M_RoundModel = "M_RoundModel";

    //View
    public const string V_Start = "V_Start";
    public const string V_Options = "V_Options";
    //public const string V_Game = "V_Game";
    public const string V_InsBtn = "V_InsBtn";
    public const string V_SkillBtn = "V_SkillBtn";
    public const string V_Energy = "V_Energy";
    public const string V_HpShow = "V_HpShow";

    public const string V_CountDown = "V_CountDown";
    public const string V_Shop = "V_Shop";
    public const string V_Win = "V_Win";
    public const string V_Lose = "V_Lose";
    public const string V_System = "V_System";
    public const string V_Complete = "V_Complete";

    public const string V_Spawner = "V_Spawner";

    //Controller
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene";//SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs

    //public const string E_CountDownComplete = "E_CountDownComplete";

    //public const string E_StartRound = "E_StartRound";//StartRoundArgs
    public const string E_SpawnSoldier = "E_SpawnSoldier";
    public const string E_SpawnSkill = "E_SpawnSkill";
    public const string E_ShowSHop = "E_ShowSHop";
    public const string E_Win = "E_Win";
    public const string E_Lose = "E_Lose";



}

//public enum GameSpeed
//{
//    One,
//    Two
//}

public enum Camp
{
    YELLOW,
    GREEN
}

public enum Arm
{
    NULL,

    Swordsman,
    Knight,
    LanceKnight,
    Hunter,
    Horseman,

    Leader,

}

public enum SkillType
{
    NULL,

    FireBall,
    ArrowRain,
    Lighting
}

public enum TowerType
{
    small,
    big
}