using System;
using UnityEngine;

namespace XDot.Core
{
    internal class BaseInstaller : MonoBehaviour, ISceneInstaller
    {
        private void Awake()
        {
            
            OnSceneAwake();
        }
        public virtual void OnSceneAwake(){}

        private void Start()
        {
            OnSceneStart();
        }
        public virtual void OnSceneStart(){}
    }
}