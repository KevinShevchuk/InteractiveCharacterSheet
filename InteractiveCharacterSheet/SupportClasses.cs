﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;

namespace InteractiveCharacterSheet
{
    #region AbilityScores

    class CharacterAbilityScore
    {
        public int BaseAbilityScoreValue { get; set; } = 0;
        public int AbilityScore { get; set; } = 0;
        public int Modifier { get; set; } = 0;
        internal string ScoreName { get; set; }

        public CharacterAbilityScore()
        {

        }
    }

    #endregion

    #region Skills

    class CharacterSkill : INotifyPropertyChanged
    {
        private int _baseSkillValue = 0;
        private bool _hasTools = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal string SkillName { get; set; }
        public string SkillDisplayName { get; set; }
        public string GoverningAbilityScore { get; set; }
        public bool AppliesArmorCheckPenalty { get; set; }
        public bool TrainedOnly { get; set; } = false;
        public int BaseSkillValue
        {
            get => _baseSkillValue;
            set
            {
                _baseSkillValue = value;
                NotifyPropertyChanged("BaseSkillValue");
            }
        }
        public int CalculatedValue { get; set; } = 0;
        public List<Paragraph> Description { get; set; } = new List<Paragraph>();
        public bool IsClassSkill { get; set; } = false;
        public bool IsCraftSkill { get; set; } = false;
        public bool IsProfession { get; set; } = false;
        public bool HasTools
        {
            get => _hasTools;
            set
            {
                if (IsCraftSkill == true) { _hasTools = value; }
            }
        }

        public CharacterSkill()
        {

        }

        public CharacterSkill(string skillName, string skilldisplayName, string governingAbilityScore, bool appliesArmorCheckPenalty, bool isCraftSkill)
        {
            SkillName = skillName;
            SkillDisplayName = skilldisplayName;
            GoverningAbilityScore = governingAbilityScore;
            AppliesArmorCheckPenalty = appliesArmorCheckPenalty;
            IsCraftSkill = isCraftSkill;
        }
    }

    #endregion

    #region Attributes

    class CharacterAttribute
    {
        public int BaseAttributeValue { get; set; } = 0;
        public int AttributeValue { get; set; } = 0;
        internal string AttributeName { get; set; }
        internal string GoverningAbilityScore { get; set; }

        public CharacterAttribute()
        {

        }
    }

    #endregion

    #region Feats

    class CharacterFeat
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Paragraph> Description { get; set; }
        internal List<Restriction> Restrictions { get; set; }
        internal List<CharacterModification> Modifications { get; set; }
    }

    #endregion

    #region Classes

    public enum DieSize
    {
        None,
        d3,
        d4,
        d6,
        d8,
        d10,
        d12,
        d100
    }

    class CharacterClass
    {
        public string ClassName { get; set; }
        public string ClassDisplayName { get; set; }
        public DieSize HitDicePerLevel { get; set; }
        public int SkillRanksPerLevel { get; set; }
        internal List<CharacterSkill> ClassSkills { get; set; }
        internal List<ClassLevel> ClassLevelTable { get; set; }
        internal List<string> TrainedSkills { get; set; }
        internal List<string> Proficencies { get; set; }
        internal List<string> AlignmentRestrictions { get; set; }
    }

    class ClassFeature
    {
        public string FeatureName { get; set; }
        public string FeatureDisplayName { get; set; }
        public bool HasLimitedUses { get; set; }
        public int UsesPerDay { get; set; }
        public List<Paragraph> Description { get; set; }
        public List<CharacterModification> Modifications { get; set; }
    }

    class ClassLevel
    {
        public int Level { get; set; }
        public int BaseAttackBonus { get; set; }
        public int FortitudeSave { get; set; }
        public int ReflexSave { get; set; }
        public int WillSave { get; set; }
        public int L1Spells { get; set; }
        public int L2Spells { get; set; }
        public int L3Spells { get; set; }
        public int L4Spells { get; set; }
        public int L5Spells { get; set; }
        public int L6Spells { get; set; }
        public int L7Spells { get; set; }
        public int L8Spells { get; set; }
        public int L9Spells { get; set; }
        internal List<ClassFeature> Special { get; set; }
    }

    #endregion

    #region Races

    class CharacterRace
    {
        public string RaceName { get; set; }
        public string RaceDisplayName { get; set; }
        public string Size { get; set; }
        public int BaseSpeed { get; set; }
        public int BaseSwimSpeed { get; set; }
        public int BaseFlySpeed { get; set; }
        public string Languages { get; set; }
        internal RaceSubType SubType { get; set; }
        internal Bloodline Bloodline { get; set; }
        internal List<RacialTrait> TraitList { get; set; }
        internal List<FavoredClass> FavoredClassBonuses { get; set; }
        public List<Paragraph> Description { get; set; }

    }

    class RaceSubType
    {
        public string SubTypeName { get; set; }
        public string SubtypeDisplayName { get; set; }
    }

    class FavoredClass
    {
        internal CharacterClass CharacterClass { get; set; }
        internal LevelTable LevelTable { get; set; }
    }

    class Bloodline
    {
        public string BloodlineName { get; set; }
        public string BloodlineDisplayName { get; set; }
    }

    class RacialTrait
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Paragraph> Description { get; set; }
        internal List<Restriction> Restrictions { get; set; }
        internal List<CharacterModification> Modifications { get; set; }
    }

    #endregion

    #region Character

    class CharacterSize
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int SizeModifier { get; set; }
        public int SpecialSizeModifier { get; set; }
        public int SizeModifierFly { get; set; }
        public int SizeModifierStealth { get; set; }
        public double Space { get; set; }
        public int NaturalReach { get; set; }

        public CharacterSize(string size, string display, int sizeModifier, int specialSizeModifier, int sizeModifierFly, int sizeModifierStealth, double space, int naturalReach)
        {
            Name = size;
            DisplayName = display;
            SizeModifier = sizeModifier;
            SpecialSizeModifier = specialSizeModifier;
            SizeModifierFly = sizeModifierFly;
            SizeModifierStealth = sizeModifierStealth;
            Space = space;
            NaturalReach = naturalReach;
        }
    }

    class Alignment
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public Alignment(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }
    }

    #endregion

    #region Inventory

    public enum EquipmentType
    {
        OneHandedWeapon,
        TwoHandedWeapon,
        RangedWeapon,
        Shield,
        Shirt,
        ChestArmor,
        Bracers,
        Gloves,
        Boots,
        Ring,
        Neck,
        Helm,
        Belt,
        Slotless,        
    }

    class InventoryRow
    {

        private double _rowWeight;

        public Item RowItem { get; set; }
        public int Quantity { get; set; }
        public double Weight
        {
            get => _rowWeight;
            set
            {
                _rowWeight = System.Math.Round(Quantity * RowItem.Weight, 2);
            }
        }
    }

    class Item
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public double Weight { get; set; }
        public List<Paragraph> Description { get; set; }
    }

    #endregion

    #region Modifications

    class CharacterModCollection
    {
        private HashSet<string> _abilityScoreNames;
        private HashSet<string> _attributeNames;
        private HashSet<string> _skillNames;

        internal LinkedList<CharacterModification> ModList { get; set; }

        public CharacterModCollection()
        {
            LoadNameCache();
            ModList = new LinkedList<CharacterModification>();
        }

        public Error AddModification(CharacterModification mod)
        {
            switch (mod.Type)
            {
                case ModType.AbilityScore:
                    if (_abilityScoreNames.Contains(mod.Property) == false)
                    {
                        return new Error("Attempt to add " + mod.Property + " modification from " + mod.ModificationSource + " failed. Provided ability score name is not a recognized Ability Score.");
                    }
                    break;
                case ModType.Attribute:
                    if (_attributeNames.Contains(mod.Property) == false)
                    {
                        return new Error("Attempt to add " + mod.Property + " modification from " + mod.ModificationSource + " failed. Provided attribute name is not a recognized character attribute.");
                    }
                    break;
                case ModType.Skill:
                    if (_skillNames.Contains(mod.Property) == false)
                    {
                        return new Error("Attempt to add " + mod.Property + " modification from " + mod.ModificationSource + " failed. Provided skill name is not a recognized skill.");
                    }
                    break;
                default:
                    return new Error("Attempt to add " + mod.Property + " modification from " + mod.ModificationSource + " failed. Unrecognized type.");
            }

            ModList.AddLast(mod);
            return new Error();
        }

        public Error RemoveModification(string modSource)
        {
            return new Error();
        }

        public void LoadNameCache()
        {
            string[] attributes = new string[]
            {
                "ArmorClass",
                "TouchArmorClass",
                "FlatFootArmorClass",
                "FortitudeSavingThrows",
                "ReflexSavingThrows",
                "WillSavingThrows",
                "MeleeAttackModifier",
                "RangedAttackModifier",
                "CombatManeuverBonus",
                "CombatManeuverDefense",
                "ArmorPenalty",
                "MaxDexterityModifier",
                "SpellFailure",
                "Initiative",
                "DamageReduction",
                "SpellResist",
                "ActionPoints",
                "MoveSpeed",
                "FlySpeed",
                "SwimSpeed",
                "ClimbSpeed",
                "FireResistance",
                "ColdResistance",
                "AcidResistance",
                "ElectricityResistance",
                "SonicResistance",
                "ForceResistance",
                "EnergyResistance",
                "MentalResistance",
                "SneakAttackDamage",
                "FeatCount",
                "SkillPointCount",
                "SkillPointsPerLevel"
            };
            _attributeNames = new HashSet<string>(attributes);

            string[] abilityScores = new string[]
            {
                "ArmorClass",
                "TouchArmorClass",
                "FlatFootArmorClass",
                "FortitudeSavingThrows",
                "ReflexSavingThrows",
                "WillSavingThrows",
                "MeleeAttackModifier",
                "RangedAttackModifier",
                "CombatManeuverBonus",
                "CombatManeuverDefense",
                "ArmorPenalty",
                "MaxDexterityModifier",
                "SpellFailure",
                "Initiative",
                "DamageReduction",
                "SpellResist",
                "ActionPoints",
                "WalkSpeed",
                "FlySpeed",
                "SwimSpeed",
                "ClimbSpeed",
                "FireResistance",
                "ColdResistance",
                "AcidResistance",
                "ElectricityResistance",
                "SonicResistance",
                "ForceResistance",
                "EnergyResistance",
                "MentalResistance",
                "SneakAttackDamage",
                "FeatCount",
                "SkillPointCount",
                "SkillPointsPerLevel"
            };
            _abilityScoreNames = new HashSet<string>(abilityScores);

            string[] skills = new string[]
            {
                "Acrobatics",
                "Appraise",
                "Bluff",
                "Climb",
                "CraftAlchemy",
                "CraftArmor",
                "CraftBaskets",
                "CraftBooks",
                "CraftBows",
                "CraftCalligraphy",
                "CraftCarpentry",
                "CraftCloth",
                "CraftClothing",
                "CraftGlass",
                "CraftJewelry",
                "CraftLeather",
                "CraftLocks",
                "CraftPaintings",
                "CraftPottery",
                "CraftScupltures",
                "CraftShips",
                "CraftShoes",
                "CraftStonemasonry",
                "CraftTraps",
                "CraftWeapons",
                "Diplomacy",
                "DisableDevice",
                "Disguise",
                "EscapeArtist",
                "Fly",
                "HandleAnimal",
                "Heal",
                "Intimidate",
                "KnowledgeArcana",
                "KnowledgeDungeoneering",
                "KnowledgeEngineering",
                "KnowledgeGeography",
                "KnowledgeHistory",
                "KnowledgeLocal",
                "KnowledgeNature",
                "KnowledgeNobility",
                "KnowledgePlanes",
                "KnowledgeReligion",
                "Lingusitics",
                "Perception",
                "Perform",
                "ProfessionArchitect",
                "ProfessionBaker",
                "ProfessionBarrister",
                "ProfessionBrewer",
                "ProfessionButcher",
                "ProfessionClerk",
                "ProfessionCook",
                "ProfessionCourtesan",
                "ProfessionDriver",
                "ProfessionEngineer",
                "ProfessionFarmer",
                "ProfessionFisherman",
                "ProfessionGambler",
                "ProfessionGardener",
                "ProfessionHerbalist",
                "ProfessionInnkeeper",
                "ProfessionLibrarian",
                "ProfessionMerchant",
                "ProfessionMidwife",
                "ProfessionMiller",
                "ProfessionMiner",
                "ProfessionPorter",
                "ProfessionSailor",
                "ProfessionScribe",
                "ProfessionShepherd",
                "ProfessionStableMaster",
                "ProfessionSoldier",
                "ProfessionTanner",
                "ProfessionTrapper",
                "ProfessionWoodcutter",
                "Ride",
                "SenseMotive",
                "SleightOfHand",
                "Spellcraft",
                "Stealth",
                "Survival",
                "Swim",
                "UseMagicDevice"
            };
            _skillNames = new HashSet<string>(skills);
        }
    }

    public enum ModType
    {
        AbilityScore,
        Attribute,
        Skill
    }

    public enum ModificationAction
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public enum ModMode
    {
        None,
        LevelTable,
        PlayerChoice
    }

    class CharacterModification
    {
        internal ModType Type { get; set; }
        internal ModMode Mode { get; set; } = ModMode.None;
        public string Property { get; set; }
        internal ModificationAction Action { get; set; } = ModificationAction.Addition;
        public string ModificationSource { get; set; }
        internal LevelTable LevelTable { get; set; }
        public List<string> OptionsList { get; set; }
        public DieSize DieSize { get; set; } = DieSize.None;
        public double Value { get; set; }

        public Error Validate()
        {
            if (Mode == ModMode.LevelTable && LevelTable == null)
                return new Error("Mode is set to LevelTable But no level table is present.");
            if (Mode == ModMode.LevelTable)
            {
                Error e = LevelTable.Validate();
                if (e != null)
                    return e;
            }
            if (Mode == ModMode.PlayerChoice && OptionsList?.Count > 0)
                return new Error("Mode is set to PlayerChoice but no options are specified.");


            return null;
        }
    }

    #endregion

    #region Support Classes

    class LevelTable
    {
        internal Dictionary<int, LevelTableRow> Table { get; set; }

        public LevelTable()
        {
            Table = new Dictionary<int, LevelTableRow>();
        }

        public LevelTableRow GetCurrentLevelTableRow(int level)
        {
            return Table[level];
        }

        public Error Validate()
        {
            if (Table.Count != 20)
                return new Error("LevelTable must have row for each of the characters 20 levels. At least one is missing.");
            return null;
        }
    }

    class LevelTableRow
    {
        public string Property { get; set; }
        public ModificationAction ModAction { get; set; }
        public double Value { get; set; }
        public DieSize DieSize { get; set; } = DieSize.None;
    }

    class Restriction
    {
        public string Property { get; set; }
        public ModificationAction ModAction { get; set; }
        public double Value { get; set; }
    }

    #endregion

    #region Internal Classes

    class Error
    {
        public readonly bool Success;
        public readonly string ErrorMessage;

        public Error()
        {
            Success = true;
        }

        public Error(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }

    #endregion
}
