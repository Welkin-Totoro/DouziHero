using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]

public class Game : ApplicationBase<Game>
{
    //全局访问功能
    [HideInInspector]
    public ObjectPool ObjectPool = null;
    [HideInInspector]
    public Sound Sound = null;
    [HideInInspector]
    public StaticData StaticData = null;

    //全局方法
    public void LoadScene(int level)
    {
        //退出旧场景
        SceneArgs e = new SceneArgs();
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;

        SendEvent(Consts.E_ExitScene, e);

        //加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void OnEnable()
    {
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        ObjectPool = ObjectPool.Instance;
        Sound = Sound.Instance;
        StaticData = StaticData.Instance;

        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));
        //游戏入口
        SendEvent(Consts.E_StartUp);

    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SceneArgs e = new SceneArgs() { SceneIndex = scene.buildIndex };
        SendEvent(Consts.E_EnterScene, e);
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    //Debug.Log("OnLevelWasLoaded:" + level); 

    //    SceneArgs e = new SceneArgs();
    //    e.SceneIndex = level;

    //    SendEvent(Consts.E_EnterScene, e);
    //}



}


