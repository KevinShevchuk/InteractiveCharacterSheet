using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    reader.ReadToFollowing("Skills");
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Skill"))
                        {
                            CharacterSkill skill = new CharacterSkill();

                            reader.ReadToFollowing("Name");
                            Enum.TryParse<SkillName>(reader.ReadElementContentAsString(), out SkillName sn);
                            skill.SkillName = sn;

                            reader.ReadToFollowing("DisplayName");
                            skill.SkillDisplayName = reader.ReadElementContentAsString();

                            reader.ReadToFollowing("GoverningAbilityScore");
                            Enum.TryParse<AbilityScoreName>(reader.ReadElementContentAsString(), out AbilityScoreName an);
                            skill.GoverningAbilityScore = an;

                            reader.ReadToFollowing("AppliesArmorCheckPenalty");
                            skill.AppliesArmorCheckPenalty = reader.ReadElementContentAsBoolean();

                            reader.ReadToFollowing("TrainedOnly");
                            skill.TrainedOnly = reader.ReadElementContentAsBoolean();

                            reader.ReadToFollowing("IsCraftSkill");
                            skill.IsCraftSkill = reader.ReadElementContentAsBoolean();

                            reader.ReadToFollowing("IsProfession");
                            skill.IsProfession = reader.ReadElementContentAsBoolean();

                            reader.ReadToFollowing("Description");
                            skill.Description = TextBlocktoParagraphs(reader.ReadSubtree());

                            skills.Add(skill);
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

        #region "Support Functions"

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
