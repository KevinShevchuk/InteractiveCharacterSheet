using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace InteractiveCharacterSheet
{
    class CharacterSheetData
    {
        private string _characterName = string.Empty;
        private string _playerName = string.Empty;
        private int _level = 0;
        private string _race = string.Empty;
        private string _size = string.Empty;
        private string _gender = string.Empty;
        private int _height = 0;
        private int _weight = 0;
        private int _age = 0;
        private string _alignment = string.Empty;
        private string _deity = string.Empty;
        private string _occupation = string.Empty;
        private string _languages = string.Empty;
        private List<Paragraph> _biography = new List<Paragraph>();
        private CharacterAbilityScore _constitution = new CharacterAbilityScore();
        private CharacterAbilityScore _strength = new CharacterAbilityScore();
        private CharacterAbilityScore _dexterity = new CharacterAbilityScore();
        private CharacterAbilityScore _intelligence = new CharacterAbilityScore();
        private CharacterAbilityScore _wisdom = new CharacterAbilityScore();
        private CharacterAbilityScore _charisma = new CharacterAbilityScore();
        private ObservableCollection<CharacterSkill> _skills;

        public Error Error;

        public string CharacterName { get => _characterName; set => _characterName = value; }
        public string PlayerName { get => _playerName; set => _playerName = value; }
        public int Level { get => _level; set => _level = value; }
        public string Race { get => _race; set => _race = value; }
        public string Size { get => _size; set => _size = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public int Height { get => _height; set => _height = value; }
        public int Weight { get => _weight; set => _weight = value; }
        public int Age { get => _age; set => _age = value; }
        public string Alignment { get => _alignment; set => _alignment = value; }
        public string Deity { get => _deity; set => _deity = value; }
        public string Occupation { get => _occupation; set => _occupation = value; }
        public string Languages { get => _languages; set => _languages = value; }
        public List<Paragraph> Biography { get => _biography; set => _biography = value; }
        internal CharacterAbilityScore Constitution { get => _constitution; set => _constitution = value; }
        internal CharacterAbilityScore Strength { get => _strength; set => _strength = value; }
        internal CharacterAbilityScore Dexterity { get => _dexterity; set => _dexterity = value; }
        internal CharacterAbilityScore Intelligence { get => _intelligence; set => _intelligence = value; }
        internal CharacterAbilityScore Wisdom { get => _wisdom; set => _wisdom = value; }
        internal CharacterAbilityScore Charisma { get => _charisma; set => _charisma = value; }
        public ObservableCollection<CharacterSkill> Skills { get => _skills; set => _skills = value; }

        public CharacterSheetData()
        {

        }

        public class CharacterAttributes
        {
        }

        public class CharacterEquipment
        {
        }
    }
}