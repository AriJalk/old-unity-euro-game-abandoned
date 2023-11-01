using UnityEngine;
using System.Collections.Generic;

namespace EDBG.Engine.ResourceManagement
{
    //TODO: Add rawimage functionality
    public class SpriteManager : MonoBehaviour
    {
        // A dictionary to store loaded sprites by their names
        private Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();

        // Load a sprite by name
        public Sprite LoadSprite(string spriteName)
        {
            if (loadedSprites.ContainsKey(spriteName))
            {
                return loadedSprites[spriteName];
            }
            else
            {
                // Load the sprite from Resources (you can adjust the path accordingly)
                Sprite sprite = Resources.Load<Sprite>("Images/" + spriteName);

                if (sprite != null)
                {
                    loadedSprites.Add(spriteName, sprite);
                    return sprite;
                }
                else
                {
                    Debug.LogError("Sprite not found: " + spriteName);
                    return null;
                }
            }
        }

        // Unload a sprite to free up memory (optional)
        public void UnloadSprite(string spriteName)
        {
            if (loadedSprites.ContainsKey(spriteName))
            {
                loadedSprites.Remove(spriteName);
            }
        }
    }

}
