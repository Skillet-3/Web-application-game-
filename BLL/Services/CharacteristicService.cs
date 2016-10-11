using BLL.Interfaces;
using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces.Models;
using DAL.Interfaces;
using DAL.Interfaces.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CharacteristicService : ICharacteristicService
    {
        IRepository<SkillSet> SkillsRepository;
        IRepository<Characteristics> CharacterRepository;
        IRepository<State> StateRepository;
        IUnitOfWork Unit;

        public CharacteristicService(IRepository<SkillSet> skillsRepository, IRepository<Characteristics> characterRepository, IRepository<State> stateRepository, IUnitOfWork unit)
        {
            SkillsRepository = skillsRepository;
            CharacterRepository = characterRepository;
            StateRepository = stateRepository;
            Unit = unit;
        }

        public DetailedUserCharacteristics GetFullUserCharacteristics(string id)
        {
            var skills = SkillsRepository.Get(id);
            var characteristics = CharacterRepository.Get(id);
            var state = StateRepository.Get(id);
            var result = (new DetailedUserCharacteristics()).
                Add(characteristics).
                Add(skills).
                Add(state).
                DemageGenerator();

            return result;
        }
        
        public void SaveReward(Reward reward, DetailedUserCharacteristics status)
        {
            //update state
            var currentState = StateRepository.Get(reward.ID);
            currentState.CurrentHP = status.Health;
            currentState.CurrentMP = status.Mana;
            StateRepository.Update(currentState);

            var currentChs = CharacterRepository.Get(reward.ID);
            currentChs.Money += reward.Money;
            currentChs.Experience += reward.Experience;
            if (reward.Win)
            {
                currentChs.Wins++;
            }
            else
            {
                currentChs.Losses++;
            }
            CharacterRepository.Update(currentChs);
            Unit.Commit();
        }

        public void RestoreHP(string id)
        {
            var currentState = StateRepository.Get(id);
            var currentChs = CharacterRepository.Get(id);
            currentState.CurrentHP = currentChs.Health;
            currentState.CurrentMP = currentChs.Mana;
            StateRepository.Update(currentState);
            Unit.Commit();
        }
    }

    static class UserCharacteristicsExtension
    {
        public static DetailedUserCharacteristics Add(this DetailedUserCharacteristics src, State state)
        {
            src.Health = state.CurrentHP;
            src.Mana = state.CurrentMP;
            return src;
        }

        public static DetailedUserCharacteristics Add(this DetailedUserCharacteristics src, Characteristics ch)
        {
            src.Mana_Full = ch.Mana;
            src.Health_Full = ch.Health;
            src.Experience = ch.Experience;
            src.Agility = ch.Agility;
            src.Clan = ch.Clan;
            src.Intelligence = ch.Intelligence;
            src.Stamina = ch.Stamina;
            src.Power = ch.Power;
            src.Intuition = ch.Intuition;
            src.Money = ch.Money;
            src.Level = ch.Level;
            src.Wins = ch.Wins;
            src.Losses = ch.Losses;
            src.Standoff = ch.Standoff;

            return src;
        }

        public static DetailedUserCharacteristics Add(this DetailedUserCharacteristics src, SkillSet skills)
        {
            

            return src;
        }

        public static DetailedUserCharacteristics DemageGenerator(this DetailedUserCharacteristics src)
        {
            src.Damage_min = (int)(src.Agility * 0.4 + src.Power * 0.5);

            src.Damage_max =(int) (src.Agility * 0.5 + src.Power * 0.8);
            return src;
        }
    }
}
