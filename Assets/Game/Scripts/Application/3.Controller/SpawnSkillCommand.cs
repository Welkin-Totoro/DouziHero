using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SpawnSkillCommand : Controller
{
    public override void Execute(object data)
    {
        SpawnSkillArgs e = data as SpawnSkillArgs;
        GameModel gm = GetModel<GameModel>();

        //TODO update the count of skill

        switch (e.skillType)
        {
            case SkillType.NULL:
                throw new ArgumentNullException("skillType");
            case SkillType.FireBall:
                gm.FireBallCount--;
                gm.isUsedFireBall = true;
                break;
            case SkillType.ArrowRain:
                gm.ArrowRainCount--;
                gm.isUsedArrowRain = true;
                break;
            case SkillType.Lighting:
                gm.LightingCount--;
                gm.isUsedLighting = true;
                break;
            default:
                throw new ArgumentException("Wrong SkillType", "skillType");
        }
    }
}
