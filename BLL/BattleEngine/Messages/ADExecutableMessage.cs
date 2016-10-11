using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.BattleEngine;
using BLL.Interfaces.BattleEngine.Units;
using BLL.Interfaces.Models;
using Newtonsoft.Json;
using BLL.Interfaces.BattleEngine.Messages;

namespace BLL.BattleEngine.Messages
{
    public enum Position
    {
        HEAD = 1,
        BODY = 2,
        LEGS = 3,
        FEET = 4
    }

    public class ADExecutableMessage : SimpleExecutableMessage
    {
        [JsonIgnore]
        protected Dictionary<string, int> Effects = new Dictionary<string, int>();

        public Position AtackPosition { get; set; }

        public Position DefencePosition { get; set; }

        protected virtual void ApplyDefence(BattleUnit unit, DetailedUserCharacteristics characteristics)
        {
            var chs = unit.Characteristics;
            double factor = 1.5; // 150%
            switch (DefencePosition)
            {
                case Position.HEAD:
                    chs.Armor_head = (int)(characteristics.Armor_head * factor);
                    break;
                case Position.BODY:
                    chs.Armor_body = (int)(characteristics.Armor_body * factor);
                    break;
                case Position.LEGS:
                    chs.Armor_belt = (int)(characteristics.Armor_belt * factor);
                    break;
                case Position.FEET:
                    chs.Armor_feet = (int)(characteristics.Armor_feet * factor);
                    break;
            }

        }

        protected virtual int CalculateDamage(BattleUnit unit, BattleUnit enemy)
        {
            int pureDamage = (new Random().Next(unit.Characteristics.Damage_min, unit.Characteristics.Damage_max));
            var enemyChs = enemy.Characteristics;
            int damage = 0;
            int defence = 0;
            switch (AtackPosition)
            {
                case Position.HEAD:
                    defence = enemyChs.Armor_head;
                    break;
                case Position.BODY:
                    defence = enemyChs.Armor_body;
                    break;
                case Position.LEGS:
                    defence = enemyChs.Armor_belt;
                    break;
                case Position.FEET:
                    defence = enemyChs.Armor_feet;
                    break;
            }
            damage = pureDamage - defence;
            damage = damage < 0 ? 0 : damage;
            return damage;
        }

        protected void RestoreBaseDefenceSettings(BattleUnit unit, DetailedUserCharacteristics characteristics)
        {
            var chs = unit.Characteristics;
            chs.Armor_head = characteristics.Armor_head;
            chs.Armor_body = characteristics.Armor_body;
            chs.Armor_belt = characteristics.Armor_belt;
            chs.Armor_feet = characteristics.Armor_feet;
        }

        protected virtual ActionLog ApplyDamage(BattleUnit enemy, int damage)
        {
            enemy.Characteristics.Health -= damage; ;
            Effects.Add("HP", -damage);
            return (new ActionLog() { ActionName = "Simple atack", AffectedUser = enemy.UserName, Effects = Effects, SourceUser = Unit });
        }

        public override void Execute(IBattle battle)
        {
            var enemy = battle.Units.Values.FirstOrDefault(x => x.UserName != Unit);
            var unit = battle.Units[Unit];
            var pureUnitChar = battle.PureCharacteristics[Unit];

            RestoreBaseDefenceSettings(unit, pureUnitChar);
            ApplyDefence(unit, pureUnitChar);
            var damage = CalculateDamage(unit, enemy);
            var log = ApplyDamage(enemy, damage);
            battle.AddLog(log);
        }
    }
}
