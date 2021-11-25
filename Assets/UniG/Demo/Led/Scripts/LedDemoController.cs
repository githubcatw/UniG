using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UniG.Demos {
    public class LedDemoController : MonoBehaviour {
        public int health = 100;
        private bool initialized;
        public Color healthCol;
        private Color healthGood;
        private Color healthMid;
        private Color healthCrit;

        // Start is called before the first frame update
        void Start() {
            // Initialize and set target device
            Led.Init("UniG Demo App");
            Led.SetTargetDevice((int)Led.DeviceLightingType.All);
            // Highlight the WASD keys
            Led.SetLightingForKeyWithKeyName(KeyCode.W, 100, 100, 100);
            Led.SetLightingForKeyWithKeyName(KeyCode.A, 100, 100, 100);
            Led.SetLightingForKeyWithKeyName(KeyCode.S, 100, 100, 100);
            Led.SetLightingForKeyWithKeyName(KeyCode.D, 100, 100, 100);
            // Get the health colors
            healthGood = GetColor("Lives/Good", Led.UnityToPercentage(Color.green));
            healthMid = GetColor("Lives/Medium", new Color(100, 50, 0));
            healthCrit = GetColor("Lives/Bad", Led.UnityToPercentage(Color.red));
            // Set initialized to true
            initialized = true;
        }

        // Called when the app quits
        private void OnApplicationQuit() {
            // Shut down the LED SDK
            Led.Shutdown();

        }

        // Update is called once per frame
        void Update() {
            // Update the health color based on the user's health
            if (health > 60) healthCol = healthGood;
            else if (health < 60 && health > 30) healthCol = healthMid;
            else if (health < 30) healthCol = healthCrit;
            // If we have initialized the SDK:
            if (initialized) {
                // Update key lighting based on the user's health
                UpdateKeyOnHealth(KeyCode.ONE, 10);
                UpdateKeyOnHealth(KeyCode.TWO, 20);
                UpdateKeyOnHealth(KeyCode.THREE, 30);
                UpdateKeyOnHealth(KeyCode.FOUR, 40);
                UpdateKeyOnHealth(KeyCode.FIVE, 50);
                UpdateKeyOnHealth(KeyCode.SIX, 60);
                UpdateKeyOnHealth(KeyCode.SEVEN, 70);
                UpdateKeyOnHealth(KeyCode.EIGHT, 80);
                UpdateKeyOnHealth(KeyCode.NINE, 90);
                UpdateKeyOnHealth(KeyCode.ZERO, 100);
            }
        }

        // Update a key's color based on the user's health
        void UpdateKeyOnHealth(KeyCode kb, int thresh) {
            // If we reached the given threshold, set the color of the given key to the health color
            if (health >= thresh) Led.SetLightingForKeyWithKeyName(kb, (int)healthCol.r, (int)healthCol.g, (int)healthCol.b);
            // Else, set it to black
            else Led.SetLightingForKeyWithKeyName(kb, 0, 0, 0);
        }

        // Take damage
        public void TakeDamage() {
            // Subtract 10 from the user's health, if it isn't empty
            if (health != 0) health -= 10;
            // Set the color of every zone on every peripheral to red
            Led.SetLightingForTargetZone(Led.DeviceType.Headset, 0, 100, 0, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Headset, 1, 100, 0, 0);

            Led.SetLightingForTargetZone(Led.DeviceType.Mouse, 0, 100, 0, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Mouse, 1, 100, 0, 0);

            Led.SetLightingForTargetZone(Led.DeviceType.Mousemat, 0, 100, 0, 0);

            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 0, 100, 0, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 1, 100, 0, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 2, 100, 0, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 3, 100, 0, 0);
        }

        // Gain health
        public void GainHealth() {
            // Add 10 to the user's health, if it isn't full
            if (health != 100) health += 10;
            // Set the color of every zone on every peripheral to green
            Led.SetLightingForTargetZone(Led.DeviceType.Headset, 0, 0, 100, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Headset, 1, 0, 100, 0);

            Led.SetLightingForTargetZone(Led.DeviceType.Mouse, 0, 0, 100, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Mouse, 1, 0, 100, 0);

            Led.SetLightingForTargetZone(Led.DeviceType.Mousemat, 0, 0, 100, 0);

            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 0, 0, 100, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 1, 0, 100, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 2, 0, 100, 0);
            Led.SetLightingForTargetZone(Led.DeviceType.Speaker, 3, 0, 100, 0);
        }

        // Helper function that gets a color from the user's preferences
        public Color GetColor(string path, Color defaultColor) {
            // Initialize default values
            int r = (int)defaultColor.r;
            int g = (int)defaultColor.g;
            int b = (int)defaultColor.b;
            // Get the color from the options
            Led.GetConfigOption(path, ref r, ref g, ref b);
            // Return the received color (or default if none)
            return new Color(r, g, b);
        }
    }
}
