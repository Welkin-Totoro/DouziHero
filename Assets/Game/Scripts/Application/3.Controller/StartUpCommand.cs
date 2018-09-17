using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型 Model
        RegisterModel(new GameModel());
        //RegisterModel(new RoundModel());

        //注册命令 Controller
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(Consts.E_SpawnSoldier, typeof(SpawnSoldierCommand));
        RegisterController(Consts.E_SpawnSkill, typeof(SpawnSkillCommand));

        
        //初始化
        GameModel gm = GetModel<GameModel>();
        gm.Initialize();

        //进入开始界面
        Game.Instance.LoadScene(1);
    }
}