﻿using KBEngine;
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CBFrame.Core;
using CBFrame.Sys;
using SyncFrame;

public class GameWorld : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        installEvents();
    }

    void installEvents()
    {
        // in world
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        KBEngine.Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
 //       KBEngine.Event.registerOut("onRecieveFrame", this, "onRecieveFrame");
 
    }

    void OnDestroy()
    {
        KBEngine.Event.deregisterOut(this);
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void onEnterWorld(KBEngine.Entity entity)
    {
        Debug.Log("WorldEvent:entity." + entity.id + ",claseName:" + entity.className);

        if (entity.className == "Avatar")
        {
            GameData.Instance.RoomPlayers.Add((KBEngine.Avatar)entity);

            GameData.Instance.RoomPlayers = GameData.Instance.RoomPlayers.OrderBy(s => s.component1.seatNo).ToList();
        }

    }
    public void onLeaveWorld(KBEngine.Entity entity)
    {
        if (entity.renderObj == null)
            return;

        UnityEngine.GameObject.Destroy((UnityEngine.GameObject)entity.renderObj);
        entity.renderObj = null;
    }

//     public void onRecieveFrame(KBEngine.Entity entity,FRAME_DATA frameMsg)
//     {
//         if (entity.renderObj == null)
//             return;
// 
//          Debug.Log("world::frameid:"+ frameMsg.frameid +",----------onRecieveFrame tick : " + DateTime.Now.TimeOfDay.ToString());
//     }


}
