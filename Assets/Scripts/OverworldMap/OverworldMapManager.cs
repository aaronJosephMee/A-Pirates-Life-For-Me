using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OverworldMapManager : MonoBehaviour
{
    public static OverworldMapManager Instance;
    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private List<ChoiceScriptableObject> choiceScriptableObjects;
    private Dictionary<ChoiceType, Sprite> _choiceSprites;
    private Dictionary<ChoiceType, String> _choiceDescriptions;
    private GameObject _canvas;
    public GameObject eventCanvas;
    public GameObject _canvas2; 
    
    // Variables used to space choice nodes equally when placed
    private float _mapLength = 1242.0f;
    private float _margin = 25.0f;
    private float _horizontalIconMargin;
    private float _verticalIconMargin = 70.0f;
    private int _numChoices = 15;
    private ButtonPositioner _buttonPositioner;

    // Variables used to keep track of and generate choices
    private ChoiceGenerator _choiceGenerator;
    private List<ChoiceNode> _currentChoiceNodes = new List<ChoiceNode>();
    private ChoiceNode _currentBoatLocation;
    private ChoiceNode _goalLocation;

    // Variables used for events
    [SerializeField] private List<EventScriptableObject> seedEvents;
    [SerializeField] private List<EventScriptableObject> storyEvents;
    [SerializeField] private List<EventScriptableObject> genericEvents;
    private Events _events;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject finalCombatScreen;
    private bool _wasEventChosen = false;
    private bool _advanceMap = false;
    private bool _resetMap = false;
    public GameObject victoryText;
    bool hasWon = false;
    
    void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        
        _events = new Events(seedEvents, storyEvents, genericEvents);
        SceneManager.sceneLoaded += OnSceneLoaded;
        _canvas = GameObject.Find("Canvas");
        _canvas2 = GameObject.Find("Canvas2");
        CreateChoiceDictionaries();
        _horizontalIconMargin = (_mapLength - (2 * _margin)) / (_numChoices + 2);
        _choiceGenerator = new ChoiceGenerator(_numChoices);
        _buttonPositioner = new ButtonPositioner(_canvas.transform.position, _numChoices, _verticalIconMargin, _horizontalIconMargin);
        GenerateGoal();
        GenerateStartingPoint();
        GenerateNextChoices();
        Instantiate(tutorial, _canvas2.transform);
    }

    public void AddEvent()
    {
        Instantiate(eventCanvas);
    }

    public void MarkMapForReset()
    {
        _resetMap = true;
    }

    public void AddToEventPool(EventScriptableObject eventToAdd){
        _events.AddEvent(eventToAdd);
    }
    public EventScriptableObject GetEvent()
    {
        return _choiceGenerator.GetCurrentEvent();
    }

    private void CreateChoiceDictionaries()
    {
        _choiceSprites = new Dictionary<ChoiceType, Sprite>();
        _choiceDescriptions = new Dictionary<ChoiceType, String>();
        foreach (ChoiceScriptableObject choice in choiceScriptableObjects)
        {
            _choiceSprites[choice.choiceType] = choice.sprite;
            _choiceDescriptions[choice.choiceType] = choice.description;
        }
        
    }

    private void GenerateNextChoices()
    {
        ClearPreviousChoiceNodes();
        if (!_choiceGenerator.CanGenerateMoreChoices())
        {
            _choiceGenerator.IncreaseChoiceDepth();
            return;
        }
        _currentChoiceNodes = _choiceGenerator.GenerateChoices(_events);
        List<Vector3> positions = _buttonPositioner.GetButtonPositions(_choiceGenerator.GetChoiceDepth() + 1, _currentChoiceNodes.Count);
        for (int i = 0; i < _currentChoiceNodes.Count; i++)
        {
            List<UnityAction> callbacks = GenerateCallbacks(_currentChoiceNodes[i]);
            String description = _choiceDescriptions[_currentChoiceNodes[i].ChoiceType];
            _currentChoiceNodes[i].AddButton(GenerateChoiceButton(), positions[i], _choiceSprites[_currentChoiceNodes[i].ChoiceType], description, callbacks);
        }

        _choiceGenerator.IncreaseChoiceDepth();
    }

    private List<UnityAction> GenerateCallbacks(ChoiceNode choiceNode)
    {
        List<UnityAction> callbacks = new List<UnityAction>()
        {
            () => _choiceGenerator.SetPreviousChoice(choiceNode.ChoiceType),
            () => choiceNode.TravelToNode(_choiceSprites[ChoiceType.Ship], GetBoatCallback()),
            () => UpdateBoatNode(choiceNode),
            () => _advanceMap = true
        };
        if (choiceNode.ChoiceType == ChoiceType.Event)
        {
            callbacks.Add(() => _events.RemoveEvent(_choiceGenerator.GetCurrentEvent()));
            callbacks.Add(() => _wasEventChosen = true);
            // if (!_choiceGenerator.GetCurrentEvent().isMinigame)
            // {
            //     
            // }
            callbacks.Add(() => GameManager.instance.LoadScene(choiceNode.SceneName, _choiceGenerator.GetCurrentEvent().sceneIdx));
        }
        else
        {
            callbacks.Add(() => GameManager.instance.LoadScene(choiceNode.SceneName));
        }
        
        return callbacks;
    }

    private void ClearPreviousChoiceNodes()
    {
        foreach (ChoiceNode choiceNode in _currentChoiceNodes)
        {
            if (choiceNode != _currentBoatLocation)
            {
                Destroy(choiceNode.GetButton());
            }
        }
        _currentChoiceNodes.Clear();
    }

    private void GenerateGoal()
    {
        List<Vector3> goalPositon = _buttonPositioner.GetButtonPositions(_numChoices + 1, 1);
        _goalLocation = new ChoiceNode(ChoiceType.Goal, SceneName.NoScene);
        String description = _choiceDescriptions[ChoiceType.Goal];
        _goalLocation.AddButton(GenerateChoiceButton(), goalPositon[0], _choiceSprites[ChoiceType.Goal], description, GetGoalCallback());
    }

    private void GenerateStartingPoint()
    {
        List<Vector3> startPosition = _buttonPositioner.GetButtonPositions(0, 1);
        _currentBoatLocation = new ChoiceNode(ChoiceType.Ship, SceneName.NoScene);
        String description = _choiceDescriptions[ChoiceType.Ship];
        _currentBoatLocation.AddButton(GenerateChoiceButton(), startPosition[0], _choiceSprites[ChoiceType.Ship], description, GetBoatCallback());
    }

    private GameObject GenerateChoiceButton()
    {
        GameObject button = Instantiate(choiceButtonPrefab, _canvas.transform.position, Quaternion.identity, _canvas.transform);
        return button;
    }

    private void UpdateBoatNode(ChoiceNode newBoatNode)
    {
        Destroy(_currentBoatLocation.GetButton());
        _currentBoatLocation = newBoatNode;
    }
    
    private List<UnityAction> GetBoatCallback()
    {
        return new List<UnityAction>()
        {
            () => GameManager.instance.LoadScene(SceneName.HubShip)
        };
    }

    private List<UnityAction> GetGoalCallback()
    {
        // TODO: Implement logic for goal state
        void GoalAction()
        {
            if (_choiceGenerator.IsAtGoal())
            {
                print("You made it to the goal!");
                Instantiate(finalCombatScreen);
                hasWon = true;
            }
            else
            {
                print("You have not made it to the goal yet.");
            }
        }

        return new List<UnityAction>() {GoalAction};
    }

    public void TransitionBackToMap()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.instance.DisplayStoredPopUp();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneName.OverworldMap.GetSceneString())
        {
            if (_resetMap)
            {
                ResetMap();
                _resetMap = false;
                Instantiate(tutorial, _canvas2.transform);
                return;
            }
            _canvas = GameObject.Find("Canvas");
            _canvas2 = GameObject.Find("Canvas2");
            ReloadMapIcons();
            SetFogLevel();
            _advanceMap = false;
            if (hasWon)
            {
                GameObject placement = Instantiate(victoryScreen);
            }
        }

        if (_wasEventChosen)
        {
            AddEvent();
            _wasEventChosen = false;
        }
    }

    private void SetFogLevel()
    {
        GameObject fog = GameObject.Find("Fog");
        if (_choiceGenerator.GetChoiceDepth() >= _numChoices)
        {
            Destroy(fog);
        }
        else
        {
            ParticleSystem partSystem = fog.GetComponent<ParticleSystem>();
            // Set shape
            Vector3 newScale = partSystem.shape.scale;
            newScale.x -= 28 * (_choiceGenerator.GetChoiceDepth() - 1);
            ParticleSystem.ShapeModule oldShape = partSystem.shape;
            oldShape.scale = newScale;
            // Set position
            Vector3 offset = new Vector3((_choiceGenerator.GetChoiceDepth() - 1)* 14f, 0.0f, 0.0f);
            fog.transform.position = fog.transform.position + offset;
            // Set fog particle amount
            ParticleSystem.MainModule oldMain = partSystem.main;
            oldMain.maxParticles -= 100 * (_choiceGenerator.GetChoiceDepth() - 1);
            partSystem.Simulate(1);
            partSystem.Play();
        }

    }

    private void ReloadMapIcons()
    {
        LoadChoiceNodeButton(_currentBoatLocation, GetBoatCallback());
        LoadChoiceNodeButton(_goalLocation, GetGoalCallback());
        if (_advanceMap)
        {
            GenerateNextChoices();
        }
        else
        {
            foreach (ChoiceNode choiceNode in _currentChoiceNodes)
            {
                LoadChoiceNodeButton(choiceNode, GenerateCallbacks(choiceNode));
            }
        }
    }

    private void ResetMap()
    {
        _choiceGenerator = null;
        _currentChoiceNodes = new List<ChoiceNode>();
        _currentBoatLocation = null;
        _goalLocation = null;
        hasWon = false;

        _events = new Events(seedEvents, storyEvents, genericEvents);
        _wasEventChosen = false;
        _advanceMap = false;
        _canvas = GameObject.Find("Canvas");
        _canvas2 = GameObject.Find("Canvas2");
        _choiceGenerator = new ChoiceGenerator(_numChoices);
        _buttonPositioner = new ButtonPositioner(_canvas.transform.position, _numChoices, _verticalIconMargin, _horizontalIconMargin);
        GenerateGoal();
        GenerateStartingPoint();
        GenerateNextChoices();
    }

    private void LoadChoiceNodeButton(ChoiceNode choiceNode, List<UnityAction> callbacks)
    {
        choiceNode.ReAddButton(GenerateChoiceButton(), _choiceSprites[choiceNode.ChoiceType], _choiceDescriptions[choiceNode.ChoiceType], callbacks);
    }

    public int GetChoiceDepth()
    {
        return _choiceGenerator.GetChoiceDepth();
    }
}
