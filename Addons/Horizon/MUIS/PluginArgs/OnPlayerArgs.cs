﻿using MultiServer.Addons.Horizon.MUIS.Models;

namespace MultiServer.Addons.Horizon.MUIS.PluginArgs
{
    public class OnPlayerArgs
    {
        /// <summary>
        /// Player.
        /// </summary>
        public ClientObject Player { get; set; }

        public override string ToString()
        {
            return base.ToString() + " " +
                $"Player: {Player}";
        }
    }
}