using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveCharacterSheet
{
    class DataCache
    {
        public List<string> Alignments;
        public List<string> Genders;
        public List<CharacterSkill> Skills;

        public DataCache()
        {
            LoadAlignments();
            LoadGenders();
            LoadSkills();
        }

        private void LoadAlignments()
        {
            List<string> alignments = new List<string>();
            alignments.Add("Lawful Good");
            alignments.Add("Lawful Neutral");
            alignments.Add("Lawful Evil");
            alignments.Add("Neutral Good");
            alignments.Add("True Neutral");
            alignments.Add("Neutral Evil");
            alignments.Add("Chaotic Good");
            alignments.Add("Chaotic Neutral");
            alignments.Add("Chaotic Evil");
            this.Alignments = alignments;
        }

        private void LoadGenders()
        {
            List<string> genders = new List<string>();
            genders.Add("Female");
            genders.Add("Male");
            genders.Add("Other");
            this.Genders = genders;
        }

        private void LoadSkills()
        {
            List<CharacterSkill> skills = new List<CharacterSkill>();

            skills.Add(new CharacterSkill(SkillName.Acrobatics, AbilityScoreName.Strength, true, false));
            skills.Add(new CharacterSkill(SkillName.Appraise, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.Bluff, AbilityScoreName.Charisma, false, false));
            skills.Add(new CharacterSkill(SkillName.Climb, AbilityScoreName.Strength, true, false));
            skills.Add(new CharacterSkill(SkillName.CraftAlchemy, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftArmor, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftBaskets, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftBooks, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftBows, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftCalligraphy, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftCarpentry, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftCloth, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftClothing, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftGlass, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftJewelry, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftLeather, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftLocks, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftPaintings, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftPottery, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftScupltures, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftShips, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftShoes, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftStonemasonry, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftTraps, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.CraftWeapons, AbilityScoreName.Intelligence, false, true));
            skills.Add(new CharacterSkill(SkillName.Diplomacy, AbilityScoreName.Charisma, false, false));
            skills.Add(new CharacterSkill(SkillName.DisableDevice, AbilityScoreName.Dexterity, true, false));
            skills.Add(new CharacterSkill(SkillName.Disguise, AbilityScoreName.Charisma, false, false));
            skills.Add(new CharacterSkill(SkillName.EscapeArtist, AbilityScoreName.Dexterity, true, false));
            skills.Add(new CharacterSkill(SkillName.Fly, AbilityScoreName.Dexterity, true, false));
            skills.Add(new CharacterSkill(SkillName.HandleAnimal, AbilityScoreName.Charisma, false, false));
            skills.Add(new CharacterSkill(SkillName.Heal, AbilityScoreName.Wisdom, false, false));
            skills.Add(new CharacterSkill(SkillName.Intimidate, AbilityScoreName.Charisma, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeArcana, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeDungeoneering, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeEngineering, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeGeography, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeHistory, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeLocal, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeNature, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeNobility, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgePlanes, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.KnowledgeReligion, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.Lingusitics, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.Perception, AbilityScoreName.Wisdom, false, false));
            skills.Add(new CharacterSkill(SkillName.Perform, AbilityScoreName.Charisma, false, false));
            skills.Add(new CharacterSkill(SkillName.Profession, AbilityScoreName.Wisdom, false, false));
            skills.Add(new CharacterSkill(SkillName.Ride, AbilityScoreName.Dexterity, true, false));
            skills.Add(new CharacterSkill(SkillName.SenseMotive, AbilityScoreName.Wisdom, false, false));
            skills.Add(new CharacterSkill(SkillName.SleightOfHand, AbilityScoreName.Dexterity, true, false));
            skills.Add(new CharacterSkill(SkillName.Spellcraft, AbilityScoreName.Intelligence, false, false));
            skills.Add(new CharacterSkill(SkillName.Stealth, AbilityScoreName.Dexterity, true, false));
            skills.Add(new CharacterSkill(SkillName.Survival, AbilityScoreName.Wisdom, false, false));
            skills.Add(new CharacterSkill(SkillName.Swim, AbilityScoreName.Strength, true, false));
            skills.Add(new CharacterSkill(SkillName.UseMagicDevice, AbilityScoreName.Charisma, true, false));

            this.Skills = skills;
        }
    }
}
