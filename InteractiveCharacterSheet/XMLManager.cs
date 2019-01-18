using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveCharacterSheet
{
    class XMLManager
    {
        private string _executableUrl;

        public XMLManager()
        {
            _executableUrl = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        }

        public CharacterSheetData SaveCharacterSheet(CharacterSheetData csd, string inputUrl)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(inputUrl);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("CharacterSheet");

                xmlWriter.WriteStartElement("Character");
                xmlWriter.WriteAttributeString("level", csd.Level.ToString());
                xmlWriter.WriteAttributeString("charactername", csd.CharacterName);
                xmlWriter.WriteAttributeString("playername", csd.PlayerName);
                xmlWriter.WriteAttributeString("race", csd.Race);
                xmlWriter.WriteAttributeString("size", csd.Size);
                xmlWriter.WriteAttributeString("gender", csd.Gender);
                xmlWriter.WriteAttributeString("age", csd.Age.ToString());
                xmlWriter.WriteAttributeString("height", csd.Height.ToString());
                xmlWriter.WriteAttributeString("weight", csd.Weight.ToString());
                xmlWriter.WriteAttributeString("alignment", csd.Alignment);
                xmlWriter.WriteAttributeString("deity", csd.Height.ToString());
                xmlWriter.WriteAttributeString("occupation", csd.Occupation);
                xmlWriter.WriteAttributeString("languages", csd.Languages);
                xmlWriter.WriteAttributeString("biography", ParagraphsToText(csd.Biography));
                xmlWriter.WriteEndElement(); //Character

                xmlWriter.WriteEndElement(); //CharacterSheet

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            catch (Exception ex)
            {
                csd.Error = new Error(ex.Message);
            }

            return csd;
        }

        public CharacterSheetData LoadCharacterSheet(string inputUrl)
        {
            CharacterSheetData csd = new CharacterSheetData();
            try
            {
                using (XmlReader reader = XmlReader.Create(inputUrl))
                {
                    reader.ReadStartElement("CharacterSheet");
                    //Character
                    while (reader.Name == "Character")
                    {
                        XElement el = (XElement)XNode.ReadFrom(reader);
                        csd.Level = int.Parse(el.Attribute("level").Value);
                        csd.CharacterName = el.Attribute("charactername").Value;
                        csd.PlayerName = el.Attribute("playername").Value;
                        csd.Race = el.Attribute("race").Value;
                        csd.Size = el.Attribute("size").Value;
                        csd.Gender = el.Attribute("gender").Value;
                        csd.Age = int.Parse(el.Attribute("age").Value);
                        csd.Height = int.Parse(el.Attribute("height").Value);
                        csd.Weight = int.Parse(el.Attribute("weight").Value);
                        csd.Alignment = el.Attribute("alignment").Value;
                        csd.Deity = el.Attribute("deity").Value;
                        csd.Occupation = el.Attribute("occupation").Value;
                        csd.Languages = el.Attribute("languages").Value;
                        csd.Biography = TextToParagraphs(el.Attribute("biography").Value);

                        reader.ReadEndElement();
                    }
                }
            }
            catch (Exception ex)
            {
                csd.Error = new Error(ex.Message);
            }

            return csd;
        }

        public ObservableCollection<CharacterSkill> LoadSkills()
        {
            ObservableCollection<CharacterSkill> skills = new ObservableCollection<CharacterSkill>();
            try
            {
                using (XmlReader reader = XmlReader.Create(_executableUrl + "\\XMLDataSheets\\Skills\\Skills.xml"))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "Skill")
                            {
                                XElement el = XNode.ReadFrom(reader) as XElement;
                                if (el != null)
                                {
                                    skills.Add(LoadSkill(el));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }

            return skills;
        }

        public HashSet<Race> LoadRaces()
        {
            HashSet<Race> races = new HashSet<Race>();
            try
            {
                string[] filenames = Directory.GetFiles(_executableUrl + "\\XMLDataSheets\\Races\\");
                for (int i = 0; i < filenames.Length; i++)
                {

                    using (XmlReader reader = XmlReader.Create(_executableUrl + "\\XMLDataSheets\\Skills\\Skills.xml"))
                    {
                        reader.MoveToContent();
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "Race")
                                {
                                    XElement el = XNode.ReadFrom(reader) as XElement;
                                    if (el != null)
                                    {
                                        races.Add(LoadRace(el));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }

            return races;
        }

        //private string LoadLanguages(XmlReader inner)
        //{
        //    string languages = "";

        //    inner.ReadToFollowing("Language");
        //    while ((inner.NodeType == XmlNodeType.Element) && (inner.Name == "Language"))
        //    {
        //        languages += inner.ReadElementContentAsString() + ", ";
        //    }
        //    return languages.Substring(0, languages.Length - 2);
        //}

        #region "Support Functions"

        private CharacterSkill LoadSkill(XElement el)
        {
            CharacterSkill skill = new CharacterSkill();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Name":
                        Enum.TryParse(xe.Value, out SkillName sn);
                        skill.SkillName = sn;
                        break;
                    case "DisplayName":
                        skill.SkillDisplayName = (string)xe;
                        break;
                    case "GoverningAbilityScore":
                        Enum.TryParse(xe.Value, out AbilityScoreName an);
                        skill.GoverningAbilityScore = an;
                        break;
                    case "AppliesArmorCheckPenalty":
                        skill.AppliesArmorCheckPenalty = (bool)xe;
                        break;
                    case "TrainedOnly":
                        skill.TrainedOnly = (bool)xe;
                        break;
                    case "IsCraftSkill":
                        skill.IsCraftSkill = (bool)xe;
                        break;
                    case "IsProfession":
                        skill.IsProfession = (bool)xe;
                        break;
                    case "Description":
                        skill.Description = TextBlocktoParagraphs(xe);
                        break;
                }
            }
            return skill;
        }

        private Race LoadRace(XElement el)
        {
            Race race = new Race();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Name":
                        race.RaceName = (string)xe;
                        break;
                    case "DisplayName":
                        race.RaceDisplayName = (string)xe;
                        break;
                    case "Size":
                        race.Size = (string)xe;
                        break;
                    case "BaseSpeed":
                        race.BaseSpeed = (int)xe;
                        break;
                    case "BaseFlySpeed":
                        race.BaseFlySpeed = (int)xe;
                        break;
                    case "BaseSwimSpeed":
                        race.BaseSwimSpeed = (int)xe;
                        break;
                    case "Description":
                        race.Description = TextBlocktoParagraphs(xe);
                        break;
                    case "RacialTraits":
                        race.TraitList = LoadRacialTraits(xe);
                        break;
                }
            }
            return race;
        }

        private List<RacialTrait> LoadRacialTraits(XElement el)
        {
            List<RacialTrait> traits = new List<RacialTrait>();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                traits.Add(LoadRacialTrait(xe));
            }
            return traits;
        }

        private RacialTrait LoadRacialTrait(XElement el)
        {
            RacialTrait trait = new RacialTrait();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Name":
                        trait.Name = (string)xe;
                        break;
                    case "DisplayName":
                        trait.DisplayName = (string)xe;
                        break;
                    case "Description":
                        trait.Description = TextBlocktoParagraphs(xe);
                        break;
                    case "Modifications":
                        trait.Modifications = LoadModifications(xe);
                        break;
                }
            }
            return trait;
        }

        private List<CharacterModification> LoadModifications(XElement el)
        {
            List<CharacterModification> mods = new List<CharacterModification>();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                mods.Add(LoadModification(xe));
            }
            return mods;
        }

        private CharacterModification LoadModification(XElement el)
        {
            XElement typeNode = el.Descendants("Type").First();
            if (typeNode != null)
            {
                switch ((string)typeNode)
                {
                    case "AbilityScore":
                        return LoadModification2(el, new AbilityScoreModification());
                    case "Attribute":
                        return LoadModification2(el, new AttributeModification());
                    case "Skill":
                        return LoadModification2(el, new SkillModification());
                }
            }
            return null;
        }

        private CharacterModification LoadModification2(XElement el, CharacterModification mod)
        {
            IEnumerable<XElement> nodes = el.Descendants();
            //foreach (XElement xe in nodes)
            //{
            //    switch (xe.Name.LocalName)
            //    {

            //    }
            //}
            return mod;
        }

        private List<Paragraph> TextBlocktoParagraphs(XmlReader inner)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();

            inner.ReadToFollowing("p");
            while ((inner.NodeType == XmlNodeType.Element) && (inner.Name == "p"))
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = inner.ReadElementContentAsString() });
                paragraphs.Add(paragraph);
                inner.Read();
            }
            return paragraphs;
        }

        private List<Paragraph> TextBlocktoParagraphs(XElement textBlock)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
            IEnumerable<XElement> nodes = textBlock.Descendants();
            foreach (XElement xe in nodes)
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = xe.Value });
                paragraphs.Add(paragraph);
            }
            return paragraphs;
        }

        private List<Paragraph> TextToParagraphs(string text)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
            
            string[] lines = text.Split(new string[] { "\\r", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = lines[i] });
                //if (i < lines.Length - 1) paragraph.Inlines.Add(new LineBreak()); //puts extra line in on loads.
                paragraphs.Add(paragraph);
            }

            return paragraphs;
        }

        private string ParagraphsToText(List<Paragraph> paragraphs)
        {
            string text = string.Empty;
            for (int i = 0; i < paragraphs.Count; i++)
            {
                Paragraph p = paragraphs.ElementAt(i);
                text += string.Join(string.Empty, p.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward)));
                if (i < paragraphs.Count - 1)
                {
                    text += "\r";
                }
            }

            return text;
        }

        #endregion
    }
}
