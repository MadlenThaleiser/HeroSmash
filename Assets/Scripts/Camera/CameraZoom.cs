using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is created when the match starts and is linked to the main camera. It shows all the characters in the optimal zoom so that they are as big as possible.
    /// </summary>
    public class CameraZoom : MonoBehaviour
    {
        /// <summary>
        /// The minimum size of the camera zoom.
        /// </summary>
        private const int MIN_SIZE = 50;

        /// <summary>
        /// The camera's focus should be more on the top and not on the ground so we always see the entire character.
        /// </summary>
        private const int BUFFER_TOP = 20;

        /// <summary>
        /// Needed so the characters aren't standing too close to the camera's bounds.
        /// </summary>
        private const int BUFFER = 10;

        /// <summary>
        /// The previous camera size. Needed for detecting big differences in size when a character stops being tracked.
        /// </summary>
        private float previousSize;

        /// <summary>
        /// Tells us whether we're currently doing a smooth camera transition.
        /// </summary>
        private bool doingSmoothTransition = false;

        /// <summary>
        /// The transition timer for smooth transitions.
        /// </summary>
        private double smoothTransitionTimer = 0;

        /// <summary>
        /// Calculates the size of the camera depending on the player's coordinates on the screen.
        /// </summary>
        public void setBounds(GameObject[] players)
        {
            // TODO: These bounds are currently hardcoded for the school level.
            const int BOUNDS_LEFT = -400;
            const int BOUNDS_RIGHT = 200;
            const int BOUNDS_TOP = 600;
            const int BOUNDS_BOTTOM = -100;

            float minX = BOUNDS_RIGHT;
            float maxX = BOUNDS_LEFT;
            float minY = BOUNDS_TOP;
            float maxY = BOUNDS_BOTTOM;

            for (int i = 0; i < GameController.playerNum; i++)
            {
                var x = players[i].transform.position.x;
                var y = players[i].transform.position.y;

                // Only track the character within the specified bounds
                if (x < BOUNDS_RIGHT
                    && x > BOUNDS_LEFT
                    && y > BOUNDS_BOTTOM)
                {
                    minX = Mathf.Min(x, minX);
                    maxX = Mathf.Max(x, maxX);
                    minY = Mathf.Min(y, minY);
                    maxY = Mathf.Max(y, maxY);
                }
            }

            float cameraSize = Mathf.Max(
                MIN_SIZE,
                Mathf.Abs(minX - maxX) / 2 + BUFFER,
                Mathf.Abs(minY - maxY) / 2 + BUFFER);
            Vector3 position = new Vector3((minX + maxX) / 2, (minY + maxY) / 2 + BUFFER_TOP, transform.position.z);
            const int MAX_SIZE_DIFFERENCE = 50;

            if (Mathf.Abs(cameraSize - this.previousSize) > MAX_SIZE_DIFFERENCE || this.doingSmoothTransition)
            {
                smoothTransition(position, cameraSize);
            }
            else
            {
                transform.position = position;
                camera.orthographicSize = cameraSize;
            }

            this.previousSize = cameraSize;
        }

        /// <summary>
        /// Does a smooth camera transition for bigger .
        /// </summary>
        /// <param name='position'>
        /// The position to move to.
        /// </param>
        /// <param name='size'>
        /// The new size of the camera.
        /// </param>
        private void smoothTransition(Vector3 position, float size)
        {
            if (!this.doingSmoothTransition)
            {
                this.doingSmoothTransition = true;
                this.smoothTransitionTimer = 0;
            }
            else if (this.smoothTransitionTimer >= 1)
            {
                // end of the transition
                this.smoothTransitionTimer = 1;
                this.doingSmoothTransition = false;
            }
            else
            {
                this.smoothTransitionTimer += 0.1 * Time.deltaTime;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, position, (float)this.smoothTransitionTimer);
            this.camera.orthographicSize = Mathf.Lerp(this.camera.orthographicSize, size, (float)this.smoothTransitionTimer);
        }
    }
}