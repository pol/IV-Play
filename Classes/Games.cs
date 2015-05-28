using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IV_Play
{
    /// <summary>
    /// Our Games list, Dictionary of romname/Game object
    /// </summary>
    internal class Games : Dictionary<string, Game>
    {
        public int TotalGames { get; set; }
        public string MameVersion { get; set; }
        public string MAME { get; set; }

        public Game FindGame(string name)
        {
            //foreach (Game game in Values)
            //{
            //    if (game.Name.Equals(name))
            //        return game;

            //    foreach (var child in game.Children)
            //    {
            //        if (child.Value.Name == name)
            //            return child.Value;
            //    }
            //}

            if (this.ContainsKey(name))
                return this[name];

            //foreach (var child in this[name].Children)
            //{
            //    if (child.Value.Name == name)
            //        return child.Value;
            //}

            return null;
        }
    }
}
