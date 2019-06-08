using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveCharacterSheet
{
    class CharacterSheetPresenter
    {
        public CharacterSheetData CharSheetData;
        public DataCache DCache;
        private XMLManager XManager;

        public CharacterSheetPresenter()
        {
            DCache = new DataCache();
            XManager = new XMLManager();
            CharSheetData = new CharacterSheetData();
            LoadModificationSources();
            ApplyModifications();
        }

        public void LoadCharacterSheet(string inputUrl)
        {
            LoadEmptyCharacterSheet();
            XManager.LoadCharacterSheet(inputUrl, ref CharSheetData);
        }

        public void LoadEmptyCharacterSheet()
        {
            CharSheetData.Skills = DCache.Skills;
        }

        public void SaveCharacterSheet(string inputUrl)
        {
            CharSheetData = XManager.SaveCharacterSheet(CharSheetData, inputUrl);
        }

        public void LoadModificationSources()
        {
            CharSheetData.Traits.Clear();
            CharSheetData.Size == null;

            CharacterRace charRace = DCache.Races.First(r => r.RaceName = CharSheetData.Race);
            foreach (RacialTrait rt in charRace.TraitList)
            {
                CharSheetData.Traits.Add(rt);
            }
            CharSheetData.CharSize = DCache.Sizes.First(s => s.Name = CharSheetData.Size);
        }

        public void ApplyModifications()
        {
            
        }
    }
}