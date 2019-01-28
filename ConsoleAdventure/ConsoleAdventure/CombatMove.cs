using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class CombatMove
    {
        public enum SkillType { Fist, Longsword, Club, Shortsword}
        public string Name { get; set; }
        public string PActionText { get; set; }
        public string EActionText { get; set; }
        public double DamageMod { get; set; }
        public double AccuracyMod { get; set; }
        public SkillType MoveType { get; set; }

        public CombatMove(string _name, string _p_action, string _e_action, double _damage, double _accuracy, SkillType _type)
        {
            Name = _name;
            PActionText = _p_action;
            EActionText = _e_action;
            DamageMod = _damage;
            AccuracyMod = _accuracy;
            MoveType = _type;
        }
    }
}
