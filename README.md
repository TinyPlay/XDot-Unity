# XDot Framework
Welcome to the **XDot Framework** for Unity. This is a simple and lightweight MVP framework with event system and context-based constructors.

<p align="center">
  <img src="/XDot Framework/Assets/Resources/splash.png" width="100%" title="XDot Framework for Unity">
</p>

**XDot Framework** was developed for Unity and is supported on versions above **2017+** *(however, in theory it may work on versions below)*. You can use this set of libraries absolutely free of charge.

**The developers of the library:** https://github.com/TinyPlay <br/>
**Pixel Incubator Discord:** https://discord.com/invite/Ee9sYsttWP <br/>
**VK Community:** https://vk.com/pixelincubator <br/>

## Features
**XDot Framework** A full-featured, lightweight and easy-to-use MVC/MVP framework with many useful features in development, such as:
* Full-Featured Event System with Reactive Field support and Event Container;
* Content Loading System with Addressables, Resources and Direct Content Loading Support;
* View Preloading at the fly;
* Powerful Data-Providers to store your models in JSON, AES Encrypted JSON, Binary or AES Encrypted Binary;
* Full-Featured demo scene for MVP and Events;

## Vendor
Basically **XDot Framework needs UniTask library**. This library is already included in the project.
You can remove UniTask and provide alternative way to load your views in the presenter.

## Theory
**MVP - ModelViewPresenter pattern**, where **M** - class with object data, **P** - presenter class (controller) where we control all object and link model with view, and **V** - view class where we provide model data updates and controller manipulation results.

## Installation
**Unpack *.unitypackage** from latest release of this repository to your unity project **or just clone this repo and open as Unity Project**.

*Project tested at Unity 2020.3*

## Scene Installer
This is a basic class with any project initializations at the scene.
Your scene may contains only SceneInstaller and preload all content at fly.

**Basic Scene Installer Example:**
```csharp
using Demo.Camera;
using Demo.Input;
using Demo.Player;
using UnityEngine;
using XDot.Core;

namespace Demo
{
    internal class SceneInstaller : BaseInstaller
    {
        private InputPresenter _input;
        private PlayerPresenter _player;
        private CameraPresenter _camera;
        
        // Initialize All Basic Presenters on Scene Start
        public override void OnSceneStart()
        {
            // Initialize Input Presenter
            _input = new InputPresenter(new InputPresenter.Context { });
            
            // Initialize Character Presenter
            _player = new PlayerPresenter(new PlayerPresenter.Context { });
            _player.Initialized.AddListener(InitializeCamera);
        }

        // Initialize Camera After Player Initialization
        private void InitializeCamera()
        {
            _camera = new CameraPresenter(new CameraPresenter.Context
            {
                Target = _player.GetView<PlayerView>().GetViewTransform()
            });
        }
    }
}
```

## MVP Triad
Any objects in your game must be contain at least one element from the Triad. Basically you have MVP (Model, View, Presenter) classes.

**Simple Presenter Example:**
```csharp
using System;
using Demo.Camera.Handlers;
using UnityEngine;
using XDot.ContentLoader;
using XDot.Core;
using XDot.Events;
using XDot.Handlers;

namespace Demo.Camera
{
    // Simple Camera Presenter
    internal class CameraPresenter : BasePresenter
    {
        public struct Context : IContext
        {
            public Transform Target;            // Camera Target
        }

        private bool _isLoaded = false;
        private GameObject _cameraObject;

        private readonly GameEvent<CameraHandler> _cameraUpdate = new GameEvent<CameraHandler>();
        private Vector3 _camPosition = Vector3.zero;
        private Quaternion _camRotation = Quaternion.identity;
        
        // Presenter Initialization
        public CameraPresenter(Context context) : base(context)
        {
            // Load Model
            CameraModel cameraModel = new CameraModel();
            SetModel(cameraModel);
            LoadModel<CameraModel>(Application.persistentDataPath+"/CameraSettings.dat", handler =>
            {
                Debug.Log($"Failed to load Camera Model. {handler.Message}");
            });
            
            // Load View
            LoadView("Demo/CameraView", LoaderType.Resources, OnViewLoaded, handler => throw new Exception($"Failed to load Camera View. {handler.Message}"));
            
            // Bind GameLoop Events
            EventContainer.Get<GameEvent<FixedUpdateHandler>>().AddListener(OnFixedUpdateHandler);
            EventContainer.Bind(_cameraUpdate);
        }

        // Camera View Loaded
        private void OnViewLoaded(GameObject viewObject)
        {
            // Create Instance of Camera
            _cameraObject = GameObject.Instantiate(viewObject);
            
            // Setup Camera View
            CameraView camView = _cameraObject.GetComponent<CameraView>();
            camView.SetContext(new CameraView.Context
            {
                BasePosition = new CameraHandler
                {
                    Position = _camPosition,
                    Rotation = _camRotation
                }
            });
            SetView(camView);
            
            _isLoaded = true;
        }

        // Game Loop Event (FixedUpdate method analog)
        private void OnFixedUpdateHandler(FixedUpdateHandler updateData)
        {
            Transform currentTarget = GetContext<Context>().Target;
            if(currentTarget==null || !_isLoaded)
                return;
            
            Vector3 dirFromTarget = Quaternion.Euler(new Vector3(GetModel<CameraModel>().CameraAngle, 0f, 0f)) * Vector3.back;
            _camPosition = currentTarget.position + dirFromTarget * GetModel<CameraModel>().CameraDistance;
            _camRotation = Quaternion.LookRotation(-dirFromTarget, Vector3.up);
            _cameraUpdate?.Invoke(new CameraHandler
            {
                Position = _camPosition,
                Rotation = _camRotation
            });
        }
    }
}
```

**Now, Look at View Example:**
```csharp
using System;
using Demo.Camera.Handlers;
using XDot.Core;
using XDot.Events;

namespace Demo.Camera
{
    // All Views are child of BaseView
    internal class CameraView : BaseView
    {
        public struct Context : IContext
        {
            public CameraHandler BasePosition;
        }
        private Context _context;

        // SetContext - it's an analog of constructor for
        // MonoBased Views
        public void SetContext(Context context)
        {
            _context = context;
            
            // Setup Base Position
            transform.SetPositionAndRotation(_context.BasePosition.Position, _context.BasePosition.Rotation);

            // Add Handlers
            EventContainer.Get<GameEvent<CameraHandler>>()?.AddListener(OnCameraUpdate);
        }

        // Unbind Events form Container on Destroy
        private void OnDestroy()
        {
            EventContainer.Unbind(EventContainer.Get<GameEvent<CameraHandler>>());
        }

        // Camera Update Handler
        private void OnCameraUpdate(CameraHandler cameraData)
        {
            transform.SetPositionAndRotation(cameraData.Position, cameraData.Rotation);
        }
    }
}
```

**And our simple Model Example:**
```csharp
using XDot.Core;

namespace Demo.Camera
{
    // Simple Model Class for Camera
    [System.Serializable]
    internal class CameraModel : BaseModel
    {
        public float CameraAngle = 45f;
        public float CameraDistance = 10f;
    }
}
```


## Events Container
You can bind any events using Events Container class.
Don't forgot to unbind events before unload scene.

**Usage Example:**
```csharp
// For example we have some method for bindings
private void EventContainerSample()
{
    // Simple Events Binding
    GameEvent<MyEventHandler> _myEvent = new GameEvent<MyEventHandler>();
    EventContainer.Bind(_myEvent);
    
    // Add Events Listener
    _myEvent.AddListener(OnEventFired);
    
    // Invoke Events from Container
    // By direct ref or using container
    _myEvent.Invoke(new MyEventHandler{
        Message = "Direct Message"
    });
    
    // Invoke throw container
    EventContainer.Get<MyEventHandler>().Invoke(new MyEventHandler{
        Message = "Container Message"
    });
    
    // Simple Events Unbinding
    EventContainer.Unbind(_myEvent);
}

// Here we grab event data
private void OnEventFired(MyEventHandler handlerData){
    Debug.Log(handlerData);
}
```

**Handler Example:**
```csharp
// For all events we need some handlers
// for example Struct Handler
public struct MyEventHandler
{
    // Here we provide event data
    public string Message;
}
```

## Demo Usage
Just open the Demo scene from: *"Assets/Demo/"*

## Full Documentation
Read full documentation for XDot Framework <a href="https://github.com/TinyPlay/XDot-Unity/wiki">here</a>.

**Support Contacts:** <a href="mailto:ceo@tpgames.ru">ceo@tpgames.ru</a>