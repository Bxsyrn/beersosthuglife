using Blocks.Gameplay.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blocks.Gameplay.Platformer
{
    /// <summary>
    /// Platformer-specific HUD that extends <see cref="CoreHUD"/> to display coin count.
    /// Updates the coin label when the player's Coin stat changes.
    /// </summary>
    public class PlatformerHUD : CoreHUD
    {
        #region Fields & Properties

        private Label m_CoinsLabel;
        private ProgressBar m_PlayerJetpackFuelBar;
        private VisualElement m_PlayerJetpackFuelBarFill;

        // You can pick any color you like. Here we use Orange. (You will need to import using.UnityEngine;)
        private static readonly Color k_JetpackFuelBarColor = new Color(1.0f, 0.647f, 0.0f, 0.75f);

        #endregion

        #region Protected Methods

        /// <summary>
        /// Queries and caches references to UI elements from the visual tree.
        /// </summary>
        /// <param name="root">The root visual element to query from.</param>
        protected override void QueryHUDElements(VisualElement root)
        {
            base.QueryHUDElements(root);
            m_CoinsLabel = root.Q<Label>("coins-label");
            m_PlayerJetpackFuelBar = root.Q<ProgressBar>("player-jetpack-fuel-bar");
            if (m_PlayerJetpackFuelBar != null)
            {
            // Optional: Set specific color for the fill
            m_PlayerJetpackFuelBarFill = m_PlayerJetpackFuelBar.Q<VisualElement>(null, "unity-progress-bar__progress");
            if (m_PlayerJetpackFuelBarFill != null)
            {
                m_PlayerJetpackFuelBarFill.style.backgroundColor = k_JetpackFuelBarColor;
            }
}
        }

        /// <summary>
        /// Handles stat changes for the local player and updates the corresponding UI elements.
        /// </summary>
        /// <param name="payload">The stat change data containing stat name and values.</param>
        protected override void HandleStatChangedLocal(StatChangePayload payload)
        {
            switch (payload.statName)
            {
                case "Coin":
                    if (m_CoinsLabel != null)
                    {
                        m_CoinsLabel.text = $"Coins: {payload.currentValue:F0}";
                    }
                    break;
                case "JetpackFuel":
                    if (m_PlayerJetpackFuelBar != null)
                    {
                      m_PlayerJetpackFuelBar.highValue = payload.maxValue;
                      m_PlayerJetpackFuelBar.value = payload.currentValue;
                    }
                    break;
            }
        }

        #endregion
    }
}
