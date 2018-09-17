using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Spawner : View
{
    public Transform[] greenSpanerTrans;

    private int DelayToDestroy = 2;

    public override string Name
    {
        get { return Consts.V_Spawner; }
    }



    public void SpawnSoldier(Arm arm, Camp camp, Vector3 pos)
    {
        GameObject go = Game.Instance.ObjectPool.Spawn(arm.ToString() + "_" + camp.ToString());
        go.transform.position = pos;
        Soldier soldier = go.GetComponent<Soldier>();
        soldier.HpChanged += Soldier_HpChanged;
        soldier.Dead += Soldier_Dead;
    }
    public void SpawnSkill(SkillType skillType, Camp camp, Vector3 pos)
    {
        GameObject go = Game.Instance.ObjectPool.Spawn(skillType.ToString() + "_" + camp.ToString());
        go.transform.position = pos + Vector3.up * 10;
        StartCoroutine(DestroyCoroutine(go.GetComponent<Skill>(), go.GetComponent<Skill>().DelayToDestroy));
    }



    private void Soldier_HpChanged(int arg1, int arg2)
    {
    }
    private void Soldier_Dead(Role obj)
    {
        Soldier soldier = obj as Soldier;
        StartCoroutine(DestroyCoroutine(soldier, soldier.DelayToDestroy));
    }
    private void Skill_Dead(Skill obj)
    {
        StartCoroutine(DestroyCoroutine(obj, obj.DelayToDestroy));
    }

    IEnumerator DestroyCoroutine(ReusableObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        Game.Instance.ObjectPool.Unspawn(obj.gameObject);
    }
    IEnumerator RandomSpawnEnemyCoroutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(5);
            List<Transform> SpawnerList = new List<Transform>(greenSpanerTrans);
            int count = UnityEngine.Random.Range(1, 4);
            for (int i = 0; i < count; i++)
            {
                int index = UnityEngine.Random.Range(0, SpawnerList.Count);
                SpawnSoldierArgs e = new SpawnSoldierArgs() { arm = (Arm)UnityEngine.Random.Range(1, 6), camp = Camp.GREEN, pos = SpawnerList[index].position };
                SendEvent(Consts.E_SpawnSoldier, e);
                SpawnerList.RemoveAt(index);
            }
        }


    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
        AttentionEvents.Add(Consts.E_SpawnSoldier);
        AttentionEvents.Add(Consts.E_SpawnSkill);
        AttentionEvents.Add(Consts.E_ShowSHop);
        AttentionEvents.Add(Consts.E_Win);
        AttentionEvents.Add(Consts.E_Lose);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e0 = data as SceneArgs;
                if (e0.SceneIndex == 2)
                    Game.Instance.Sound.PlayBGM("Game");
                StartCoroutine(RandomSpawnEnemyCoroutine());
                break;

            case Consts.E_SpawnSoldier:
                SpawnSoldierArgs e1 = data as SpawnSoldierArgs;
                SpawnSoldier(e1.arm, e1.camp, e1.pos);
                break;

            case Consts.E_SpawnSkill:
                SpawnSkillArgs e2 = data as SpawnSkillArgs;
                SpawnSkill(e2.skillType, e2.camp, e2.pos);
                break;

            case Consts.E_ShowSHop:
                MVC.GetView<UIShop>().Show();
                break;

            case Consts.E_Win:
                MVC.GetView<UIWin>().Show();
                break;

            case Consts.E_Lose:
                MVC.GetView<UILose>().Show();
                break;

            default:
                break;
        }
    }

}
