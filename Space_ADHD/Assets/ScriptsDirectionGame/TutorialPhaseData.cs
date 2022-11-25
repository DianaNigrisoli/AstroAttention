using System;
using UnityEngine;

namespace Assets.ScriptsDirectionGame
{
    public class TutorialPhaseData
    {
        private String tutorialRobotText;
        private String tutorialScreenText;
        private Boolean waitForUserInput;
        private float waitSeconds;
        private GameObject tutorialTargetingObject;
        private Vector3 tutorialTargetingObjectPosition;
        private Vector3 tutorialTargetingObjectRotation;

        public TutorialPhaseData(string tutorialRobotText, string tutorialScreenText, bool waitForUserInput, float waitSeconds, GameObject tutorialTargetingObject, Vector3 tutorialTargetingObjectPosition, Vector3 tutorialTargetingObjectRotation)
        {
            this.tutorialRobotText = tutorialRobotText;
            this.tutorialScreenText = tutorialScreenText;
            this.waitForUserInput = waitForUserInput;
            this.waitSeconds = waitSeconds;
            this.tutorialTargetingObject = tutorialTargetingObject;
            this.tutorialTargetingObjectPosition = tutorialTargetingObjectPosition;
            this.tutorialTargetingObjectRotation = tutorialTargetingObjectRotation;
        }

        public string TutorialRobotText => tutorialRobotText;

        public string TutorialScreenText => tutorialScreenText;

        public bool WaitForUserInput => waitForUserInput;

        public float WaitSeconds => waitSeconds;

        public GameObject TutorialTargetingObject => tutorialTargetingObject;

        public Vector3 TutorialTargetingObjectPosition => tutorialTargetingObjectPosition;

        public Vector3 TutorialTargetingObjectRotation => tutorialTargetingObjectRotation;
    }
}