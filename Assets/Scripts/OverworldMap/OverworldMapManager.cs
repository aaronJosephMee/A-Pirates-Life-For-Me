using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldMapManager : MonoBehaviour
{
    public static OverworldMapManager instance;
    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private List<ChoiceScriptableObject> _choiceScriptableObjects;
    private Dictionary<ChoiceType, Sprite> _choiceSprites;
    private GameObject _canvas;
    
    // Variables used to space choice nodes equally when placed
    private float _mapLength = 1042.0f;
    private float _margin = 25.0f;
    private float _horizontalIconMargin;
    private float _verticalIconMargin = 70.0f;
    private int _numChoices = 6;

    // Variables used to keep track of and generate choices
    private ChoiceGenerator _choiceGenerator;
    private List<ChoiceNode> _currentChoiceNodes = new List<ChoiceNode>();
    private ChoiceNode _currentBoatLocation;
    private ChoiceNode _goalLocation;

    
    void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _canvas = GameObject.Find("Canvas");
        _choiceSprites = CreateChoiceSpritesDictionary();
        _horizontalIconMargin = (_mapLength - (2 * _margin)) / (_numChoices + 2);
        _choiceGenerator = new ChoiceGenerator(_numChoices);
        GenerateGoal();
        GenerateStartingPoint();
        GenerateNextChoices();
    }

    private Dictionary<ChoiceType, Sprite> CreateChoiceSpritesDictionary()
    {
        Dictionary<ChoiceType, Sprite> dictionary = new Dictionary<ChoiceType, Sprite>();
        foreach (ChoiceScriptableObject choice in _choiceScriptableObjects)
        {
            dictionary[choice.choiceType] = choice.sprite;
        }
        return dictionary;
    }

    private void GenerateNextChoices()
    {
        ClearPreviousChoiceNodes();
        print(_choiceGenerator.CanGenerateMoreChoices());
        if (!_choiceGenerator.CanGenerateMoreChoices())
        {
            _choiceGenerator.IncreaseChoiceDepth();
            return;
        }
        _currentChoiceNodes = _choiceGenerator.GenerateChoices();
        List<Vector3> positions = GetButtonPositions(_choiceGenerator.GetChoiceDepth() + 1, _currentChoiceNodes.Count);
        for (int i = 0; i < _currentChoiceNodes.Count; i++)
        {
            int index = i;
            List<UnityAction> callbacks = new List<UnityAction>()
            {
                () => _choiceGenerator.SetPreviousChoice(_currentChoiceNodes[index].ChoiceType),
                () => _currentChoiceNodes[index].TravelToNode(_choiceSprites[ChoiceType.Ship], GetBoatCallback()),
                () => UpdateBoatNode(_currentChoiceNodes[index]),
                () => TransitionToNewScene(_currentChoiceNodes[index].SceneName)
            };
            _currentChoiceNodes[i].AddButton(GenerateChoiceButton(), positions[i], _choiceSprites[_currentChoiceNodes[i].ChoiceType], callbacks);
        }

        _choiceGenerator.IncreaseChoiceDepth();
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
    
    private List<Vector3> GetButtonPositions(int choiceDepth, int numChoices)
    {
        List<Vector3> positions = new List<Vector3>();
        float horizontalOffset = GetPositionOffset(choiceDepth, _numChoices + 2, _horizontalIconMargin);
        for (int i = 0; i < numChoices; i++)
        {
            float verticalOffset = GetPositionOffset(i, numChoices, _verticalIconMargin);
            Vector3 totalOffset = new Vector3(horizontalOffset, verticalOffset, 0.0f);
            positions.Add(_canvas.transform.position + totalOffset);
        }

        return positions;
    }

    private float GetPositionOffset(int depth, int numElements, float elementMargin)
    {
        float layer = depth - Mathf.Floor(numElements / 2.0f);
        float offset = elementMargin * layer;
        float evenOffset = numElements % 2 == 0 ? elementMargin / 2.0f : 0.0f;
        return offset + evenOffset;
    }

    private void GenerateGoal()
    {
        float horizontalOffset = GetPositionOffset(_numChoices + 1, _numChoices + 2, _horizontalIconMargin);
        Vector3 position = _canvas.transform.position + new Vector3(horizontalOffset, 0.0f, 0.0f);
        _goalLocation = new ChoiceNode(ChoiceType.Goal, SceneName.NoScene);
        _goalLocation.AddButton(GenerateChoiceButton(), position, _choiceSprites[ChoiceType.Goal], GetGoalCallback());
    }

    private void GenerateStartingPoint()
    {
        float horizontalOffset = GetPositionOffset(0, _numChoices + 2, _horizontalIconMargin);
        Vector3 position = _canvas.transform.position + new Vector3(horizontalOffset, 0.0f, 0.0f);
        _currentBoatLocation = new ChoiceNode(ChoiceType.Ship, SceneName.NoScene);
        _currentBoatLocation.AddButton(GenerateChoiceButton(), position, _choiceSprites[ChoiceType.Ship], GetBoatCallback());
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
        // TODO: Implement logic to return the player to the boat hub world
        return new List<UnityAction>();
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

    private void TransitionToNewScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.GetSceneString());
    }

    public void TransitionBackToMap()
    {
        SceneManager.LoadScene(SceneName.OverworldMap.GetSceneString());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name == SceneName.OverworldMap.GetSceneString())
        {
            _canvas = GameObject.Find("Canvas");
            LoadChoiceNodeButton(_currentBoatLocation, GetGoalCallback());
            LoadChoiceNodeButton(_goalLocation, GetGoalCallback());
            GenerateNextChoices();

        }
    }

    private void LoadChoiceNodeButton(ChoiceNode choiceNode, List<UnityAction> callbacks)
    {
        choiceNode.ReAddButton(GenerateChoiceButton(), _choiceSprites[choiceNode.ChoiceType], callbacks);
    }
}
