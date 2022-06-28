
using EXO.Patches.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Core;

namespace EXO
{
    public class LoadOrder : Attribute
    {
        public int Priority;

        public LoadOrder(int priority)
        {
            Priority = priority;
        }
    }    
    public class BaseModule
    {       

        /// <summary>
        ///   Runs after Game Initialization.
        /// </summary>
        public virtual void OnApplicationStart()
        {
        }

        /// <summary>
        ///   Runs after OnApplicationStart.
        /// </summary>
        public virtual void OnApplicationLateStart()
        {
        }

        /// <summary>
        ///   Runs after UI Manager Initialization.
        /// </summary>
        public virtual void OnUIManagerInit()
        {
        }

        /// <summary>
        ///   Runs after QuickMenu Initialization.
        /// </summary>
        public virtual void OnQuickMenuInit()
        {

        }
        /// <summary>
        ///   This Runs When the UI Menu is being Formed.
        /// </summary>
        public virtual void FormUI()
        {

        }

        /// <summary>
        ///   Runs when a Scene has Loaded and is passed the Scene's Build Index and Name.
        /// </summary>
        public virtual void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
        }

        /// <summary>
        ///   Runs when a Scene has Initialized and is passed the Scene's Build Index and Name.
        /// </summary>
        public virtual void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
        }

        /// <summary>
        ///   Runs when a Scene has Unloaded and is passed the Scene's Build Index and Name.
        /// </summary>
        public virtual void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
        }

        /// <summary>
        ///   Can run multiple times per frame. Mostly used for Physics.
        /// </summary>
        public virtual void OnFixedUpdate()
        {
        }

        /// <summary>
        ///   Runs once per frame.
        /// </summary>
        public virtual void OnUpdate()
        {
        }

        /// <summary>
        ///   Runs once per second.
        /// </summary>
        public virtual void OnSecondPassed()
        {
        }

        /// <summary>
        ///   Runs once per frame after OnUpdate and OnFixedUpdate have finished.
        /// </summary>
        public virtual void OnLateUpdate()
        {
        }

        /// <summary>
        ///   Can run multiple times per frame. Mostly used for Unity's IMGUI.
        /// </summary>
        public virtual void OnGUI()
        {
        }

        /// <summary>
        ///   Runs when the Game is told to Close.
        /// </summary>
        public virtual void OnApplicationQuit()
        {
        }

        /// <summary>
        ///   Runs when Melon Preferences get saved.
        /// </summary>
        public virtual void OnPreferencesSaved()
        {
        }

        /// <summary>
        ///   Runs when Melon Preferences get loaded.
        /// </summary>
        public virtual void OnPreferencesLoaded()
        {
        }
    }    
}
