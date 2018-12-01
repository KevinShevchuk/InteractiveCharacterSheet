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

    class AbilityScoreModification
    {
        private string _modificationSource;
        private AbilityScoreName _abilityScore;
        private int _scoreValue;

        public int ScoreValue { get => _scoreValue; set => _scoreValue = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        internal AbilityScoreName AbilityScore { get => _abilityScore; set => _abilityScore = value; }
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

    class SkillModification
    {
        private string _modificationSource;
        private SkillName _skillName;
        private int _skillValue;

        public int SkillValue { get => _skillValue; set => _skillValue = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        internal SkillName SkillName { get => _skillName; set => _skillName = value; }
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

    class AttributeModification
    {
        private string _modificationSource;
        private AttributeName _attributeName;
        private int _attributeValue;

        public int AttributeValue { get => _attributeValue; set => _attributeValue = value; }
        public string ModificationSource { get => _modificationSource; set => _modificationSource = value; }
        internal AttributeName AttributeName { get => _attributeName; set => _attributeName = value; }
    }

    #endregion

    #region Feats

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

    class RacialTrait
    {
        private string _traitName;
        private string _traitDescription;
        private List<AttributeModification> _attributeModifications;
        private List<SkillModification> _skillModifications;
        private List<AbilityScoreModification> _abilityScoreModifications;

        public string TraitName { get => _traitName; set => _traitName = value; }
        public string TraitDescription { get => _traitDescription; set => _traitDescription = value; }
        internal List<AttributeModification> AttributeModifications { get => _attributeModifications; set => _attributeModifications = value; }
        internal List<SkillModification> SkillModifications { get => _skillModifications; set => _skillModifications = value; }
        internal List<AbilityScoreModification> AbilityScoreModifications { get => _abilityScoreModifications; set => _abilityScoreModifications = value; }
    }

    #region "Internal Classes"

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
