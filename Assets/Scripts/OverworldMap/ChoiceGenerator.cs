using System.Collections.Generic;

namespace DefaultNamespace.OverworldMap
{
    public class ChoiceGenerator
    {
        private int _numChoices;
        private bool _isEventForced = false;
        private int _currentChoiceDepth = 0;
        private ChoiceType _previousChoice = ChoiceType.Ship;

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
        
        public List<ChoiceNode> GenerateChoices()
        {
            List<ChoiceType> nextChoiceTypes = GenerateNextChoiceTypes();
            List<ChoiceNode> choiceNodes = new List<ChoiceNode>();
            foreach (ChoiceType choiceType in nextChoiceTypes)
            {
                // TODO: Add logic to generate choice node based on specific combat/event/shop being generated
                choiceNodes.Add(new ChoiceNode(choiceType, SceneName.ButtonMashing));
            }

            return choiceNodes;
        }

        private List<ChoiceType> GenerateNextChoiceTypes()
        {
            if (_isEventForced)
            {
                return new List<ChoiceType>() {ChoiceType.Event};
            }
        
            List<ChoiceType> possibleChoices = new List<ChoiceType>()
            {
                ChoiceType.Event, 
                ChoiceType.Shop, 
                ChoiceType.Combat, 
                ChoiceType.EliteCombat, 
                ChoiceType.Healing
            };

            if (_previousChoice != ChoiceType.Ship)
            {
                possibleChoices.Remove(_previousChoice);
            }

            return possibleChoices;
        }
    }
}