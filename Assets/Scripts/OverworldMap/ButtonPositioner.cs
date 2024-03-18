using System.Collections.Generic;
using UnityEngine;

public class ButtonPositioner
{
    private Vector3 _canvasPosition;
    private int _numChoices;
    private float _horizontalIconMargin;
    private float _verticalIconMargin;
    
    public ButtonPositioner(Vector3 canvasPosition, int numChoices, float horizontalIconMargin, float verticalIconMargin)
    {
        _canvasPosition = canvasPosition;
        _numChoices = numChoices;
        _horizontalIconMargin = horizontalIconMargin;
        _verticalIconMargin = verticalIconMargin;
    }
    public List<Vector3> GetButtonPositions(int choiceDepth, int numChoices)
    {
        List<Vector3> positions = new List<Vector3>();
        float horizontalOffset = GetPositionOffset(choiceDepth, _numChoices + 2, _horizontalIconMargin);
        for (int i = 0; i < numChoices; i++)
        {
            float verticalOffset = GetPositionOffset(i, numChoices, _verticalIconMargin);
            Vector3 totalOffset = new Vector3(horizontalOffset, verticalOffset, 0.0f);
            positions.Add(_canvasPosition + totalOffset);
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
}