using System;
using System.IO;
using System.Collections;
using ACSMinCapture.Log;
using System.Windows.Forms;
using System.Linq;

namespace ACSMinCapture
{

    public class IniParser
    {
        private Hashtable keyPairs = new Hashtable();
        private String iniFilePath;

        ~IniParser()
        {

        }

        private struct SectionPair
        {
            public String Section;
            public String Key;
        }


        string DecriptyConfigYPT(string FileName)
        {
            TextReader ConfigYPT = new StreamReader(FileName);

            string input = null;
            string result = string.Empty;
            while ((input = ConfigYPT.ReadLine()) != null)
            {
                result = result + input + "\n";
            }
            ConfigYPT.Close();

            result = CUtils.Decrypt(result);

            return result;
        }

        /// <summary>
        /// Opens the INI file at the given path and enumerates the values in the IniParser.
        /// </summary>
        /// <param name="iniPath">Full path to INI file.</param>
        public IniParser(String iniPath)
        {
            TextReader iniFile = null;
            String strLine = null;
            String currentRoot = null;
            String[] keyPair = null;

            try
            {
                if (File.Exists(iniPath))
                {
                    iniFilePath = iniPath;

                    if (Path.GetExtension(iniPath).ToUpper() == ".INI")
                    {
                        iniFile = new StreamReader(iniPath);
                    }
                    else if (Path.GetExtension(iniPath).ToUpper() == ".YPT")
                    {
                        iniPath = DecriptyConfigYPT(iniPath);
                        iniFile = new StringReader(iniPath);
                    }
                }
                else
                    iniFile = new StringReader(iniPath);


                strLine = iniFile.ReadLine();
                while (strLine != null)
                {
                    //strLine = strLine.Trim().ToUpper();
                    strLine = strLine.Trim();

                    if (strLine != "")
                    {
                        if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                        {
                            currentRoot = strLine.Substring(1, strLine.Length - 2);
                        }
                        else
                        {
                            keyPair = strLine.Split(new char[] { '=' }, 2);

                            SectionPair sectionPair;
                            String value = null;

                            if (currentRoot == null)
                                currentRoot = "ROOT";

                            sectionPair.Section = currentRoot;
                            sectionPair.Key = keyPair[0];

                            if (keyPair.Length > 1)
                                value = keyPair[1];

                            if (!keyPairs.Contains(sectionPair))
                                keyPairs.Add(sectionPair, value.Trim());
                        }
                    }

                    strLine = iniFile.ReadLine();
                }


            }
            catch (Exception ex)
            {
                WFMessageBox.Show("000000+" + ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
                throw ex;
            }
            finally
            {
                if (iniFile != null)
                {
                    iniFile.Close();
                    iniFile.Dispose();
                }
            }


        }

        /// <summary>
        /// Returns the value for the given section, key pair.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="settingName">Key name.</param>
        public String GetSetting(String sectionName, String settingName, string Default = "")
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();

            var result = (String)keyPairs[sectionPair];
            if (result == null)
                result = Default;

            return result;

        }

        /// <summary>
        /// Enumerates all lines for given section.
        /// </summary>
        /// <param name="sectionName">Section to enum.</param>
        public String[] EnumSection(String sectionName)
        {
            ArrayList tmpArray = new ArrayList();

            foreach (SectionPair pair in keyPairs.Keys)
            {
                if (pair.Section == sectionName.ToUpper())
                    tmpArray.Add(pair.Key);
            }

            return (String[])tmpArray.ToArray(typeof(String));
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        public void AddSetting(String sectionName, String settingName, String settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();

            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);

            keyPairs.Add(sectionPair, settingValue);
            this.SaveSettings();
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved with a null value.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void AddSetting(String sectionName, String settingName)
        {
            AddSetting(sectionName, settingName, null);
        }

        /// <summary>
        /// Remove a setting.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        public void DeleteSetting(String sectionName, String settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName.ToUpper();
            sectionPair.Key = settingName.ToUpper();

            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);
        }

        /// <summary>
        /// Save settings to new file.
        /// </summary>
        /// <param name="newFilePath">New file path.</param>
        public void SaveSettings(String newFilePath)
        {
            ArrayList sections = new ArrayList();
            String tmpValue = "";
            String strToSave = "";

            foreach (SectionPair sectionPair in keyPairs.Keys)
            {
                if (!sections.Contains(sectionPair.Section))
                    sections.Add(sectionPair.Section);
            }

            foreach (String section in sections)
            {
                strToSave += ("[" + section + "]\r\n");

                foreach (SectionPair sectionPair in keyPairs.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        tmpValue = (String)keyPairs[sectionPair];

                        if (tmpValue != null)
                            tmpValue = "=" + tmpValue;

                        strToSave += (sectionPair.Key + tmpValue + "\r\n");
                    }
                }

                strToSave += "\r\n";
            }

            try
            {
                if (Path.GetExtension(iniFilePath).ToUpper() == ".YPT")
                    strToSave = CUtils.Encrypt(strToSave);

                TextWriter tw = new StreamWriter(newFilePath);
                tw.Write(strToSave);
                tw.Close();
                tw.Dispose();
            }
            catch (Exception ex)
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Save settings back to ini file.
        /// </summary>
        public void SaveSettings()
        {
            SaveSettings(iniFilePath);
        }
    }
}