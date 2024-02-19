/*
 * HighScoreManager.cs
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.HighScoreScene
{
    /// <summary>
    /// High Score Manager Class
    /// </summary>
    public class HighScoreManager
    {
        private static string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\LabryinthOfTheDamned\Highscore\";
        private static string fileName = @"hs.bin";
        private static string filePath = dirPath + fileName;
        public static Dictionary<int,int> ScoreSet { get; set; } = new Dictionary<int,int>(2);

        /// <summary>
        /// Deserialize Highscore
        /// </summary>
        public static void DeserializeHighscore()
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                string[] readData = br.ReadString().Split(";");
                foreach (string line in readData)
                {
                    ScoreSet.Add(int.Parse(line.Split(":")[0]), int.Parse(line.Split(":")[1]));
                }
                br.Close();
            }
            catch 
            {
                Directory.CreateDirectory(dirPath);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                
                bw.Write("1:0;2:0");
                bw.Close();
            }
        }

        /// <summary>
        /// Increments the Score for player id
        /// </summary>
        /// <param name="player">id of the player to add score to</param>
        public static void AddScore(int player)
        {
            ScoreSet[player]++;
        }
        
        /// <summary>
        /// Serializes Highscore
        /// </summary>
        public static void SerializeHighscore()
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write("1:" + ScoreSet[1] + ";2:" + ScoreSet[2]);
                bw.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
