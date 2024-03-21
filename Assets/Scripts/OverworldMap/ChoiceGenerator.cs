using System.Collections.Generic;

namespace DefaultNamespace.OverworldMap
{
    public class ChoiceGenerator
    {
        private int _numChoices;
        private bool _isEventForced = false;
        private int _currentChoiceDepth = 0;
        private ChoiceType _previousChoice = ChoiceType.Ship;
        private EventScriptableObject _currentEvent;

        public ChoiceGenerator(int numChoices)
        {
            _numChoices = numChoices;
        }
        
        public void SetPreviousChoice(ChoiceType choiceType)
        {
            _previousChoice = choiceType;
        }

        public bool CanGenerateMoreChoices()
        {
            return _currentChoiceDepth < _numChoices;
        }

        public void IncreaseChoiceDepth()
        {
            _currentChoiceDepth++;
        }

        public int GetChoiceDepth()
        {
            return _currentChoiceDepth;
        }

        public bool IsAtGoal()
        {
            return _currentChoiceDepth >= _numChoices + 1;
        }
        
        public List<ChoiceNode> GenerateChoices(Events events)
        {
            List<ChoiceType> nextChoiceTypes = GenerateNextChoiceTypes(events.IsEventPoolEmpty());
            List<ChoiceNode> choiceNodes = new List<ChoiceNode>();
            foreach (ChoiceType choiceType in nextChoiceTypes)
            {
                // TODO: Add logic to generate choice node based on specific combat/event/shop being generated
                switch (choiceType)
                {
                    case ChoiceType.Shop:
                        choiceNodes.Add(new ChoiceNode(choiceType, SceneName.Shop));
                        break;
                    case ChoiceType.Event:
                        _currentEvent = events.GetEvent();
                        choiceNodes.Add(new ChoiceNode(choiceType, _currentEvent.scene));
                        break;
                    case ChoiceType.Combat:
                        choiceNodes.Add(new ChoiceNode(choiceType, SceneName.CombatNight));
                        break;
                    default:
                        choiceNodes.Add(new ChoiceNode(choiceType, SceneName.ButtonMashing));
                        break;
                }
            }

            return choiceNodes;
        }

        private List<ChoiceType> GenerateNextChoiceTypes(bool isEventPoolEmpty)
        {
            if (_isEventForced)
            {
                return new List<ChoiceType>() {ChoiceType.Event};
            }
        
            List<ChoiceType> possibleChoices = new List<ChoiceType>()
            {
                ChoiceType.Event, 
                ChoiceType.Shop, 
                ChoiceType.Combat
            };

            if (_previousChoice != ChoiceType.Ship)
            {
                possibleChoices.Remove(_previousChoice);
            }

            if (isEventPoolEmpty)
            {
                possibleChoices.Remove(ChoiceType.Event);
            }

            return possibleChoices;
        }

        public EventScriptableObject GetCurrentEvent()
        {
            return _currentEvent;
        }
    }
}