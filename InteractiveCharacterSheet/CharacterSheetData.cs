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
        public Error Error;

        public string CharacterName { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public string Race { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public CharacterSize CharSize { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int Height { get; set; } = 0;
        public int Weight { get; set; } = 0;
        public int Age { get; set; } = 0;
        public string Alignment { get; set; } = string.Empty;
        public string Deity { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string Languages { get; set; } = string.Empty;
        public List<string> LanguagesList { get; set; } = new List<string>();
        public List<Paragraph> Biography { get; set; } = new List<Paragraph>();
        internal CharacterAbilityScore Constitution { get; set; } = new CharacterAbilityScore();
        internal CharacterAbilityScore Strength { get; set; } = new CharacterAbilityScore();
        internal CharacterAbilityScore Dexterity { get; set; } = new CharacterAbilityScore();
        internal CharacterAbilityScore Intelligence { get; set; } = new CharacterAbilityScore();
        internal CharacterAbilityScore Wisdom { get; set; } = new CharacterAbilityScore();
        internal CharacterAbilityScore Charisma { get; set; } = new CharacterAbilityScore();
        public ObservableCollection<CharacterSkill> Skills { get; set; }
        public ObservableCollection<InventoryRow> Inventory { get; set; }
        public ObservableCollection<RacialTrait> Traits { get; set; }
        public ObservableCollection<CharacterFeat> Feats { get; set; }
        public CharacterModCollection Modifications { get; set; }
        
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