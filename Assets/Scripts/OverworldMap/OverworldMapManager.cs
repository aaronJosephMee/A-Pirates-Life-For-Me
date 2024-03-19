using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
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
    private GameObject _canvas;
    public GameObject eventCanvas;
    
    // Variables used to space choice nodes equally when placed
    private float _mapLength = 1042.0f;
    private float _margin = 25.0f;
    private float _horizontalIconMargin;
    private float _verticalIconMargin = 70.0f;
    private int _numChoices = 12;
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
    private bool _wasEventChosen = false;
    private bool _advanceMap = false;
    private bool _resetMap = false;
    
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
        _choiceSprites = CreateChoiceSpritesDictionary();
        _horizontalIconMargin = (_mapLength - (2 * _margin)) / (_numChoices + 2);
        _choiceGenerator = new ChoiceGenerator(_numChoices);
        _buttonPositioner = new ButtonPositioner(_canvas.transform.position, _numChoices, _verticalIconMargin, _horizontalIconMargin);
        GenerateGoal();
        GenerateStartingPoint();
        GenerateNextChoices();
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

    private Dictionary<ChoiceType, Sprite> CreateChoiceSpritesDictionary()
    {
        Dictionary<ChoiceType, Sprite> dictionary = new Dictionary<ChoiceType, Sprite>();
        foreach (ChoiceScriptableObject choice in choiceScriptableObjects)
        {
            dictionary[choice.choiceType] = choice.sprite;
        }
        return dictionary;
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
            _currentChoiceNodes[i].AddButton(GenerateChoiceButton(), positions[i], _choiceSprites[_currentChoiceNodes[i].ChoiceType], callbacks);
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
            if (!_choiceGenerator.GetCurrentEvent().isMinigame)
            {
                callbacks.Add(() => _wasEventChosen = true);
            }
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
        _goalLocation.AddButton(GenerateChoiceButton(), goalPositon[0], _choiceSprites[ChoiceType.Goal], GetGoalCallback());
    }

    private void GenerateStartingPoint()
    {
        List<Vector3> startPosition = _buttonPositioner.GetButtonPositions(0, 1);
        _currentBoatLocation = new ChoiceNode(ChoiceType.Ship, SceneName.NoScene);
        _currentBoatLocation.AddButton(GenerateChoiceButton(), startPosition[0], _choiceSprites[ChoiceType.Ship], GetBoatCallback());
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
        GameManager.instance.LoadScene(SceneName.OverworldMap);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneName.OverworldMap.GetSceneString())
        {
            if (_resetMap)
            {
                ResetMap();
                _resetMap = false;
                return;
            }
            _canvas = GameObject.Find("Canvas");
            ReloadMapIcons();
            _advanceMap = false;
        }

        if (_wasEventChosen)
        {
            AddEvent();
            _wasEventChosen = false;
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

        _events = new Events(seedEvents, storyEvents, genericEvents);
        _wasEventChosen = false;
        _advanceMap = false;
        _canvas = GameObject.Find("Canvas");
        _choiceGenerator = new ChoiceGenerator(_numChoices);
        _buttonPositioner = new ButtonPositioner(_canvas.transform.position, _numChoices, _verticalIconMargin, _horizontalIconMargin);
        GenerateGoal();
        GenerateStartingPoint();
        GenerateNextChoices();
    }

    private void LoadChoiceNodeButton(ChoiceNode choiceNode, List<UnityAction> callbacks)
    {
        choiceNode.ReAddButton(GenerateChoiceButton(), _choiceSprites[choiceNode.ChoiceType], callbacks);
    }
}
