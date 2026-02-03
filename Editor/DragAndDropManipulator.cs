using System;
using System.Collections.Generic;
using TacticsCore.Units;
using UnityEngine;
using UnityEngine.UIElements;

namespace TacticsCore.Editor
{
    public class DragAndDropManipulator : PointerManipulator
    {
        public bool BeganDrag { get; set; }
        private Vector2 TargetStartPosition { get; set; }
        private Vector3 PointerStartPosition { get; set; }
        private bool Enabled { get; set; }
        private VisualElement Root { get; }

        public DragAndDropManipulator(VisualElement target, VisualElement root)
        {
            this.target = target;
            Root = root;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
            // It's a safe bet to register it on the root element to cover all initial layout calculations
            target.RegisterCallback<GeometryChangedEvent>(OnLayoutComplete);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
            target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
            target.UnregisterCallback<GeometryChangedEvent>(OnLayoutComplete);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
            target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }
        
        private void OnLayoutComplete(GeometryChangedEvent evt)
        {
            SetPositionOfTargetToSpecificSlot(Enum.Parse<BattleUnitPosition>(target.name));
        }

        // this method stores the starting position of target and the pointer,
        // makes target capture the pointer, and denotes that a drag is now in progress
        private void PointerDownHandler(PointerDownEvent evt)
        {
            TargetStartPosition = target.resolvedStyle.translate;
            PointerStartPosition = evt.position;
            target.CapturePointer(evt.pointerId);
            Enabled = true;
        }
        
        // This method checks whether a drag is in progress and whether target has captured the pointer.
        // if both are true, calculates a new position for target within the bounds of the window.
        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (Enabled && target.HasPointerCapture(evt.pointerId))
            {
                Vector3 pointerDelta = evt.position - PointerStartPosition;

                target.style.translate = new Vector2(
                    Mathf.Clamp(TargetStartPosition.x + pointerDelta.x, 0, target.panel.visualTree.worldBound.width),
                    Mathf.Clamp(TargetStartPosition.y + pointerDelta.y, 0, target.panel.visualTree.worldBound.height));
                BeganDrag = true;
            }
        }

        // This method checks whether a drag is in progress and whether target has captured the pointer.
        // if both are true, makes target release the pointer.
        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (Enabled && target.HasPointerCapture(evt.pointerId))
            {
                target.ReleasePointer(evt.pointerId);
            }
        }

        // This method checks whether a drag is in progress. If true, queries the root
        // of the visual tree to find all slots, decides which slot is the closest one
        // that overlaps target, and sets the position of target so that it rests on top
        // of that slot. Sets the position of target back to its original position
        // if there is no overlapping slot.
        private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
        {
            if (Enabled)
            {
                VisualElement slotsContainer = Root.Q<VisualElement>("slots");
                UQueryBuilder<VisualElement> allSlots = slotsContainer.Query<VisualElement>(className: "slot");
                UQueryBuilder<VisualElement> overlappingSlots = allSlots.Where(OverlapsTarget);
                VisualElement closestOverlappingSlot = FindClosestSlot(overlappingSlots);
                Vector3 closestPos = Vector3.zero;
                if (closestOverlappingSlot != null)
                {
                    BattleUnitPosition targetUnitPosition = Enum.Parse<BattleUnitPosition>(target.name);
                    BattleUnitPosition occupantUnitPosition = Enum.Parse<BattleUnitPosition>(closestOverlappingSlot.name);
                    
                    closestPos = RootSpaceOfSlot(closestOverlappingSlot);
                    closestPos = new Vector2(closestPos.x - 5, closestPos.y - 5);

                    VisualElement slotOccupant = Root.Query<VisualElement>(className: "party-unit-button").Where(s => s.name == closestOverlappingSlot.name).ToList()[0];
                    slotOccupant.style.translate = RootSpaceOfSlot(FindSlot(targetUnitPosition));
                    // swap units in slots
                    // for example
                    // [ ]    [ ]                           [ ]    [ ]
                    // [x] -> [o] moving x to o's slot      [o]    [x] x would be the target and o would be the occupant
                    // [ ]    [ ]                           [ ]    [ ]
                    Debug.Log($"Swapping targetUnitPos {targetUnitPosition} with OccupantPos {occupantUnitPosition}");
                    // TODO: make these functions overridable and implement it within PartyCustomEditor
                    // PartyCustomEditor.SwapUnit(targetUnitPosition, occupantUnitPosition);
                    slotOccupant.name = target.name;


                    target.name = closestOverlappingSlot.name;
                }
                
                target.style.translate = closestOverlappingSlot != null ? closestPos : TargetStartPosition;
                
                Enabled = false;
                BeganDrag = false;
            }
        }

        private bool OverlapsTarget(VisualElement slot) => target.worldBound.Overlaps(slot.worldBound);

        private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
        {
            List<VisualElement> slotsList = slots.ToList();
            float bestDistanceSq = float.MaxValue;
            VisualElement closest = null;
            foreach (VisualElement slot in slotsList)
            {
                Vector3 displacement = RootSpaceOfSlot(slot) - target.resolvedStyle.translate;
                float distanceSq = displacement.sqrMagnitude;
                if (distanceSq < bestDistanceSq)
                {
                    bestDistanceSq = distanceSq;
                    closest = slot;
                }
            }
            return closest;
        }

        private VisualElement FindSlot(BattleUnitPosition battleUnitPosition)
        {
            VisualElement slotsContainer = Root.Q<VisualElement>("slots");
            UQueryBuilder<VisualElement> allSlots = slotsContainer.Query<VisualElement>(className: "slot");
            return allSlots.Where(slot => slot.name == battleUnitPosition.ToString());
        }

        public void SetPositionOfTargetToSpecificSlot(BattleUnitPosition battleUnitPosition)
        {
            target.style.translate = RootSpaceOfSlot(FindSlot(battleUnitPosition)) + new Vector3(-5, -5);
        }

        private Vector3 RootSpaceOfSlot(VisualElement slot)
        {
            Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
            return Root.WorldToLocal(slotWorldSpace);
        }
    }
}