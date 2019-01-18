using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;

namespace InteractiveCharacterSheet
{
    /*Name Enums exist for two reasons:
        1.  validation purposes for XML importing. if the imported name does not exist in the enum it isn't a valid addition.
        2.  Named indices to the various lists if applicable.
     */
    #region AbilityScores
    
    enum AbilityScoreName
    {
        Constitution,
        Dexterity,
        Strength,
        Intelligence,
        Wisdom,
        Charisma,
    }

    class CharacterAbilityScore
    {
        private int _baseAbilityScoreValue = 0;
        private List<AbilityScoreModification> _abilityScoreModification = new List<AbilityScoreModification>();
        private int _AbilityScore = 0;
        private int _modifier = 0;
        private AbilityScoreName scoreName;

        public int BaseAbilityScoreValue { get => _baseAbilityScoreValue; set => _baseAbilityScoreValue = value; }
        internal List<AbilityScoreModification> AbilityScoreModifiers { get => _abilityScoreModification; set => _abilityScoreModification = value; }
        public int AbilityScore { get => _AbilityScore; set => _AbilityScore = value; }
        public int Modifier { get => _modifier; set => _modifier = value; }
        internal AbilityScoreName ScoreName { get => scoreName; set => scoreName = value; }

        public CharacterAbilityScore()
        {
            AbilityScoreModifiers = new List<AbilityScoreModification>();
        }

        public Error AddModification(AbilityScoreModification modification)
        {
            if(modification.AbilityScore == scoreName)
            {
                AbilityScoreModifiers.Add(modification);
                return new Error();
            }
            else
            {
                return new Error("Attempt to add " + modification.AbilityScore + " modification from " + modification.ModificationSource + " to ability score " + scoreName + " failed. Ability score mismatch.");
            }
        }

        public Error RemoveModification(AbilityScoreModification modification)
        {
            if (modification.AbilityScore == scoreName)
            {
                AbilityScoreModifiers.Remove(modification);
                return new Error();
            }
            else
            {
                return new Error("Attempt to remove " + modification.AbilityScore + " modification from " + modification.ModificationSource + " to ability score " + scoreName + " failed. Ability score mismatch.");
            }
        }
    }

    #endregion

    #region Skills

    enum SkillName
    {
        Acrobatics,
        Appraise,
        Bluff,
        Climb,
        CraftAlchemy,
        CraftArmor,
        CraftBaskets,
        CraftBooks,
        CraftBows,
        CraftCalligraphy,
        CraftCarpentry,
        CraftCloth,
        CraftClothing,
        CraftGlass,
        CraftJewelry,
        CraftLeather,
        CraftLocks,
        CraftPaintings,
        CraftPottery,
        CraftScupltures,
        CraftShips,
        CraftShoes,
        CraftStonemasonry,
        CraftTraps,
        CraftWeapons,
        Diplomacy,
        DisableDevice,
        Disguise,
        EscapeArtist,
        Fly,
        HandleAnimal,
        Heal,
        Intimidate,
        KnowledgeArcana,
        KnowledgeDungeoneering,
        KnowledgeEngineering,
        KnowledgeGeography,
        KnowledgeHistory,
        KnowledgeLocal,
        KnowledgeNature,
        KnowledgeNobility,
        KnowledgePlanes,
        KnowledgeReligion,
        Lingusitics,
        Perception,
        Perform,
        ProfessionArchitect,
        ProfessionBaker,
        ProfessionBarrister,
        ProfessionBrewer,
        ProfessionButcher,
        ProfessionClerk,
        ProfessionCook,
        ProfessionCourtesan,
        ProfessionDriver,
        ProfessionEngineer,
        ProfessionFarmer,
        ProfessionFisherman,
        ProfessionGambler,
        ProfessionGardener,
        ProfessionHerbalist,
        ProfessionInnkeeper,
        ProfessionLibrarian,
        ProfessionMerchant,
        ProfessionMidwife,
        ProfessionMiller,
        ProfessionMiner,
        ProfessionPorter,
        ProfessionSailor,
        ProfessionScribe,
        ProfessionShepherd,
        ProfessionStableMaster,
        ProfessionSoldier,
        ProfessionTanner,
        ProfessionTrapper,
        ProfessionWoodcutter,
        Ride,
        SenseMotive,
        SleightOfHand,
        Spellcraft,
        Stealth,
        Survival,
        Swim,
        UseMagicDevice,
    }

    class CharacterSkill : INotifyPropertyChanged
    {
        private SkillName _skillName;
        private string _skillDisplayName;
        private AbilityScoreName _governingAbilityScore;
        private bool appliesArmorCheckPenalty;
        private int _baseSkillValue = 0;
        private int _calculatedValue = 0;
        private List<Paragraph> _description = new List<Paragraph>();
        private LinkedList<SkillModification> _skillModifiers;
        private bool _trainedOnly = false;
        private bool _isClassSkill = false;
        private bool _isCraftSkill = false;
        private bool _isProfession = false;
        private bool _hasTools = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal SkillName SkillName { get => _skillName; set => _skillName = value; }
        public string SkillDisplayName { get => _skillDisplayName; set => _skillDisplayName = value; }
        public AbilityScoreName GoverningAbilityScore { get => _governingAbilityScore; set => _governingAbilityScore = value; }
        public bool AppliesArmorCheckPenalty { get => appliesArmorCheckPenalty; set => appliesArmorCheckPenalty = value; }
        public bool TrainedOnly { get => _trainedOnly; set => _trainedOnly = value; }
        public int BaseSkillValue
        {
            get => _baseSkillValue;
            set
            {
                _baseSkillValue = value;
                NotifyPropertyChanged("BaseSkillValue");
            }
        }
        public int CalculatedValue { get => _calculatedValue; set => _calculatedValue = value; }
        public List<Paragraph> Description { get => _description; set => _description = value; }
        internal LinkedList<SkillModification> SkillModifiers { get => _skillModifiers; set => _skillModifiers = value; }
        public bool IsClassSkill { get => _isClassSkill; set => _isClassSkill = value; }
        public bool IsCraftSkill { get => _isCraftSkill; set => _isCraftSkill = value; }
        public bool IsProfession { get => _isProfession; set => _isProfession = value; }
        public bool HasTools
        {
            get => _hasTools;
            set
            {
                if (_isCraftSkill == true) { _hasTools = value; }
            }
        }

        public CharacterSkill()
        {

        }

        public CharacterSkill(SkillName skillName, string skilldisplayName, AbilityScoreName governingAbilityScore, bool appliesArmorCheckPenalty, bool isCraftSkill)
        {
            SkillName = skillName;
            GoverningAbilityScore = governingAbilityScore;
            AppliesArmorCheckPenalty = appliesArmorCheckPenalty;
            IsCraftSkill = isCraftSkill;
            SkillModifiers = new LinkedList<SkillModification>();
        }

        public Error AddModification(SkillModification modification)
        {
            if (modification.SkillName == SkillName)
            {
                SkillModifiers.AddLast(modification);
                return new Error();
            }
            else
            {
                return new Error("Attempt to add " + modification.SkillName + " modification from " + modification.ModificationSource + " to skill " + SkillName + " failed. Skill name mismatch.");
            }
        }

        public Error RemoveModification(SkillModification modification)
        {
            if (modification.SkillName == SkillName)
            {
                SkillModifiers.Remove(modification);
                return new Error();
            }
            else
            {
                return new Error("Attempt to remove " + modification.SkillName + " modification from " + modification.ModificationSource + " to skill " + SkillName + " failed. Skill name mismatch.");
            }
        }
    }

    #endregion

    #region Attributes

    enum AttributeName
    {
        ArmorClass,
        TouchArmorClass,
        FlatFootArmorClass,
        FortitudeSavingThrows,
        ReflexSavingThrows,
        WillSavingThrows,
        MeleeAttackModifier,
        RangedAttackModifier,
        CombatManeuverBonus,
        CombatManeuverDefense,
        ArmorPenalty,
        MaxDexterityModifier,
        SpellFailure,
        Initiative,
        DamageReduction,
        SpellResist,
        ActionPoints,
        WalkSpeed,
        FlySpeed,
        SwimSpeed,
        ClimbSpeed,
        FireResistance,
        ColdResistance,
        AcidResistance,
        ElectricityResistance,
        SonicResistance,
        ForceResistance,
        EnergyResistance,
        MentalResistance,
        SneakAttackDamage,
        FeatCount,
        SkillPointCount,
        SkillPointsPerLevel
    }

    class CharacterAttribute
    {
        private AttributeName _attributeName;
        private AbilityScoreName _governingAbilityScore;
        private int _baseAttributeValue = 0;
        private LinkedList<AttributeModification> _attributeModifiers;
        private int _attributeValue = 0;

        public int BaseAttributeValue { get => _baseAttributeValue; set => _baseAttributeValue = value; }
        public int AttributeValue { get => _attributeValue; set => _attributeValue = value; }
        internal AttributeName AttributeName { get => _attributeName; set => _attributeName = value; }
        internal AbilityScoreName GoverningAbilityScore { get => _governingAbilityScore; set => _governingAbilityScore = value; }
        internal LinkedList<AttributeModification> AttributeModifiers { get => _attributeModifiers; set => _attributeModifiers = value; }

        public CharacterAttribute()
        {
            AttributeModifiers = new LinkedList<AttributeModification>();
        }

        public Error AddModification(AttributeModification modification)
        {
            if (modification.AttributeName == AttributeName)
            {
                AttributeModifiers.AddLast(modification);
                return new Error();
            }
            else
            {
                return new Error("Attempt to add " + modification.AttributeName + " modification from " + modification.ModificationSource + " to attribute " + AttributeName + " failed. Attribute name mismatch.");
            }
        }

        public Error RemoveModification(AttributeModification modification)
        {
            if (modification.AttributeName == AttributeName)
            {
                AttributeModifiers.Remove(modification);
                return new Error();
            }
            else
            {
                return new Error("Attempt to remove " + modification.AttributeName + " modification from " + modification.ModificationSource + " to attribute " + AttributeName + " failed. Attribute name mismatch.");
            }
        }
    }

    #endregion

    #region Feats

    #endregion

    #region Classes

    public enum HitDice
    {
        d4,
        d6,
        d8,
        d10,
        d12
    }

    class CharacterClass
    {
        private string _className;
        private string _classDisplayName;
        private HitDice _hitDicePerLevel;
        private List<CharacterSkill> _classSkills;
        private int _skillRanksPerLevel;
    }

    class ClassFeature
    {
        private string _featureName;
        private string _featureDisplayName;
        private bool hasLimitedUses;
        private int _usesPerDay;
        private List<Paragraph> _description;
    }

    class ClassLevel
    {
        private int _level;
        private int _baseAttackBonus;
        private int _fortitudeSave;
        private int _reflexSave;
        private int _willSave;
        private List<ClassFeature> _special;
        private int _l1Spells;
        private int _l2Spells;
        private int _l3Spells;
        private int _l4Spells;
        private int _l5Spells;
        private int _l6Spells;
        private int _l7Spells;
        private int _l8Spells;
        private int _l9Spells;
    }

    #endregion

    #region Races

    class Race
    {
        private string _raceName;
        private string _raceDisplayName;
        private string _size;
        private int _baseSpeed;
        private int _baseSwimSpeed;
        private int _baseFlySpeed;
        private string _languages;
        private RaceSubType _subType;
        private Bloodline _bloodline;
        private List<RacialTrait> _traitList;
        private List<FavoredClass> _favoredClassBonuses;
        private List<Paragraph> _description;

        public string RaceName { get => _raceName; set => _raceName = value; }
        public string RaceDisplayName { get => _raceDisplayName; set => _raceDisplayName = value; }
        public string Size { get => _size; set => _size = value; }
        public int BaseSpeed { get => _baseSpeed; set => _baseSpeed = value; }
        public int BaseSwimSpeed { get => _baseSwimSpeed; set => _baseSwimSpeed = value; }
        public int BaseFlySpeed { get => _baseFlySpeed; set => _baseFlySpeed = value; }
        public string Languages { get => _languages; set => _languages = value; }
        internal RaceSubType SubType { get => _subType; set => _subType = value; }
        internal Bloodline Bloodline { get => _bloodline; set => _bloodline = value; }
        internal List<RacialTrait> TraitList { get => _traitList; set => _traitList = value; }
        internal List<FavoredClass> FavoredClassBonuses { get => _favoredClassBonuses; set => _favoredClassBonuses = value; }
        public List<Paragraph> Description { get => _description; set => _description = value; }
        
    }

    class RaceSubType
    {
        private string _subTypeName;
        private string _subtypeDisplayName;

        public string SubTypeName { get => _subTypeName; set => _subTypeName = value; }
        public string SubtypeDisplayName { get => _subtypeDisplayName; set => _subtypeDisplayName = value; }
    }

    class FavoredClass
    {
        private CharacterClass _characterClass;
        private LevelTable _levelTable;

        internal CharacterClass CharacterClass { get => _characterClass; set => _characterClass = value; }
        internal LevelTable LevelTable { get => _levelTable; set => _levelTable = value; }
    }

    class Bloodline
    {
        private string _bloodlineName;
        private string _bloodlineDisplayName;

        public string BloodlineName { get => _bloodlineName; set => _bloodlineName = value; }
        public string BloodlineDisplayName { get => _bloodlineDisplayName; set => _bloodlineDisplayName = value; }
    }

    class RacialTrait
    {
        private string _name;
        private string _displayName;
        private List<Paragraph> _description;
        private List<CharacterModification> _modifications;

        public string Name { get => _name; set => _name = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public List<Paragraph> Description { get => _description; set => _description = value; }
        internal List<CharacterModification> Modifications { get => _modifications; set => _modifications = value; }
    }

    #endregion

    class CharacterSize
    {
        private readonly string _size;
        private readonly int _sizeModifier;
        private readonly int _specialSizeModifier;
        private readonly int _sizeModifierFly;
        private readonly int _sizeModifierStealth;
        private readonly int _space;
        private readonly int _naturalReach;

        public string Size => _size;
        public int SizeModifier => _sizeModifier;
        public int SpecialSizeModifier => _specialSizeModifier;
        public int SizeModifierFly => _sizeModifierFly;
        public int SizeModifierStealth => _sizeModifierStealth;
        public int Space => _space;
        public int NaturalReach => _naturalReach;

        public CharacterSize(string size, int sizeModifier, int specialSizeModifier, int sizeModifierFly, int sizeModifierStealth, int space, int naturalReach)
        {
            this._size = size;
            this._sizeModifier = sizeModifier;
            this._specialSizeModifier = specialSizeModifier;
            this._sizeModifierFly = sizeModifierFly;
            this._sizeModifierStealth = sizeModifierStealth;
            this._space = space;
            this._naturalReach = naturalReach;
        }   
    }

    #region Modifications

    abstract class CharacterModification
    {
        public enum ValueType
        {
            Numeric,
            Dice
        }

        public enum ModificationAction
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        private ModificationAction _action;
        private double _modificationValue;
        private string _modificationSource;

        internal ModificationAction Action { get => _action; set => _action = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        public double ModificationValue { get => _modificationValue; set => _modificationValue = value; }
    }

    class SkillModification : CharacterModification
    {
        
        private SkillName _skillName;

        internal SkillName SkillName { get => _skillName; set => _skillName = value; }

        public SkillModification()
        {

        }
    }

    class AbilityScoreModification : CharacterModification
    {
        private AbilityScoreName _abilityScore;

        internal AbilityScoreName AbilityScore { get => _abilityScore; set => _abilityScore = value; }

        public AbilityScoreModification()
        {

        }
    }

    class AttributeModification : CharacterModification
    {
        private AttributeName _attributeName;

        internal AttributeName AttributeName { get => _attributeName; set => _attributeName = value; }

        public AttributeModification()
        {

        }
    }

    #endregion

    #region Support Classes

    class LevelTable
    {
        private int _level;
        private List<CharacterModification> _modifications;
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
