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
            this.DCache = new DataCache();
            this.XManager = new XMLManager();
            this.CharSheetData = new CharacterSheetData();
        }

        public void LoadCharacterSheet(string inputUrl)
        {
            LoadEmptyCharacterSheet();
            CharSheetData = XManager.LoadCharacterSheet(inputUrl);
        }

        public void LoadEmptyCharacterSheet()
        {
            CharSheetData.Skills = DCache.Skills;
        }

        public void SaveCharacterSheet(string inputUrl)
        {
            CharSheetData = XManager.SaveCharacterSheet(CharSheetData, inputUrl);
        }
    }
}