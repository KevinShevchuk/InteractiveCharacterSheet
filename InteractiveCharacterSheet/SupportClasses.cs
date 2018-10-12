using System.Collections.Generic;

namespace InteractiveCharacterSheet
{
    /*Name Enums exist for two reasons:
        1.  validation purposes for XML importing. if the imported name does not exist in the enum it isn't a valid addition.
        2.  Named indices to the various lists if applicable.
     */

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

        public int BaseAbilityScoreValue { get => _baseAbilityScoreValue; set => _baseAbilityScoreValue = value; }
        internal List<AbilityScoreModification> AbilityScoreModification { get => _abilityScoreModification; set => _abilityScoreModification = value; }

        public CharacterAbilityScore()
        {

        }
    }

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
        Profession,
        Ride,
        SenseMotive,
        SleightOfHand,
        Spellcraft,
        Stealth,
        Survival,
        Swim,
        UseMagicDevice,
    }

    class CharacterSkill
    {
        private SkillName _skillName;
        private AbilityScoreName _governingAbilityScore;
        private bool appliesArmorCheckPenalty;
        private int _baseSkillValue = 0;
        private List<SkillModification> _skillModifiers;
        private bool _isClassSkill = false;
        private bool _isCraftSkill = false;
        private bool _hasTools = false;

        internal SkillName SkillName { get => _skillName; set => _skillName = value; }
        public AbilityScoreName GoverningAbilityScore { get => _governingAbilityScore; set => _governingAbilityScore = value; }
        public bool AppliesArmorCheckPenalty { get => appliesArmorCheckPenalty; set => appliesArmorCheckPenalty = value; }
        public int BaseSkillValue { get => _baseSkillValue; set => _baseSkillValue = value; }
        internal List<SkillModification> SkillModifiers { get => _skillModifiers; set => _skillModifiers = value; }
        public bool IsClassSkill { get => _isClassSkill; set => _isClassSkill = value; }
        public bool IsCraftSkill { get => _isCraftSkill; set => _isCraftSkill = value; }
        public bool HasTools
        {
            get => _hasTools;
            set
            {
                if (_isCraftSkill == true) { _hasTools = value; }
            }
        }

        public CharacterSkill(SkillName skillName, AbilityScoreName governingAbilityScore, bool appliesArmorCheckPenalty, bool isCraftSkill)
        {
            SkillName = skillName;
            GoverningAbilityScore = governingAbilityScore;
            AppliesArmorCheckPenalty = appliesArmorCheckPenalty;
            IsCraftSkill = isCraftSkill;
            SkillModifiers = new List<SkillModification>();
        }
    }

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
    }

    class AttributeModification
    {
        private string _modificationSource;
        private AttributeName _attributeName;
        private int _attributeValue;

        public int AttributeValue { get => _attributeValue; set => _attributeValue = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        internal AttributeName AttributeName { get => _attributeName; set => _attributeName = value; }
    }

    class SkillModification
    {
        private string _modificationSource;
        private SkillName _skillName;
        private int _skillValue;

        public int SkillValue { get => _skillValue; set => _skillValue = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        internal SkillName SkillName { get => _skillName; set => _skillName = value; }
    }

    class AbilityScoreModification
    {
        private string _modificationSource;
        private AbilityScoreName _abilityScore;
        private int _scoreValue;

        public int ScoreValue { get => _scoreValue; set => _scoreValue = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        internal AbilityScoreName AbilityScore { get => _abilityScore; set => _abilityScore = value; }
    }

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

    class RacialTrait
    {
        private string _traitName;
        private string _traitDescription;
        private List<AttributeModification> _attributeModifications;
        private List<SkillModification> _skillModifications;
        private List<AbilityScoreModification> _abilityScoreModifications;
    }
}
