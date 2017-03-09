using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ListenInAnalysis.Classes
{
    class CBindDataGridBlock : INotifyPropertyChanged
    {     
        private int intIdx;
        private CUser_TherapyBlock m_therapyBlock;
        private SolidColorBrush m_scbAnswerForeground;

        List<CLexicalItem> m_lsLexicalItem;
        List<CChallengeItem> m_lsChallengeItem;
        List<CChallengeItemFeatures> m_lsChallengeItemFeatures;
        List<int> m_lsResponseAccuracy;

        public event PropertyChangedEventHandler PropertyChanged;
        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        //----------------------------------------------------------------------------------------------------
        // CBindDataGridItem
        //----------------------------------------------------------------------------------------------------
        public CBindDataGridBlock(int intI, CUser_TherapyBlock tb, List<CLexicalItem> lsLexicalItem, List<CChallengeItem> lsChallengeItem, List<CChallengeItemFeatures> lsChallengeItemFeatures)
        {
            intIdx = intI;
            m_therapyBlock = tb;
            m_lsLexicalItem = lsLexicalItem;
            m_lsChallengeItem = lsChallengeItem;
            m_lsChallengeItemFeatures = lsChallengeItemFeatures; 
            m_lsResponseAccuracy = tb.m_lsResponseAccuracy; 

            m_scbAnswerForeground = new SolidColorBrush();
            //updateAnswerForeground();

        }

        //----------------------------------------------------------------------------------------------------
        // updateAnswerForeground()
        //----------------------------------------------------------------------------------------------------
        /*public void updateAnswerForeground()
        {
            // display answer in green if it is correct, otherwise in red
            if (Answer == Target)
                AnswerForeground.Color = Colors.Green;
            else
                AnswerForeground.Color = Colors.Red;
        }  */

        //----------------------------------------------------------------------------------------------------
        // Getters and Setters
        //----------------------------------------------------------------------------------------------------
        #region Properties Getters and Setters
        public int Idx
        {
            get { return this.intIdx; }
            set
            {
                this.intIdx = value;
                OnPropertyChanged("Idx");
            }
        }

        public string LinguisticCategory
        {
            get
            {
                string strLC = "word/phrase";
                if (this.m_therapyBlock.m_intLinguisticType == 1)
                    strLC = "easy sentence";
                else if (this.m_therapyBlock.m_intLinguisticType == 2)
                    strLC = "hard sentence";
                return strLC;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("LinguisticCategory");
            }
        }

        public int NoiseLevel
        {
            get { return this.m_therapyBlock.m_intNoiseLevel; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("NoiseLevel");
            }
        }

        public int BlockType
        {
            get { return this.m_therapyBlock.m_intBlockType; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("BlockType");
            }
        }

        public double BlockComplexity
        {
            get
            {
                if (this.m_therapyBlock.m_intBlockType != 0) return 0;
                return this.m_therapyBlock.m_dBlockComplexity;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("BlockComplexity");
            }
        }

        public double UserAbility
        {
            get
            {
                if (this.m_therapyBlock.m_intBlockType != 0) return 0;
                return this.m_therapyBlock.m_dUserAbility_Accumulated;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("UserAbility");
            }
        }

        public double TrainingStep
        {
            get
            {
                if (this.m_therapyBlock.m_intBlockType != 0) return 0;
                return this.m_therapyBlock.m_dTrainingStep;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("TrainingStep");
            }
        }

        public double AccuracyRate
        {
            get { return (this.m_therapyBlock.m_dAccuracyRate * 100); }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("AccuracyRate");
            }
        }

        public double Freq_Mean
        {
            get { return this.m_therapyBlock.m_dMean_Frequency; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Freq_Mean");
            }
        }

        public double Freq_StdDev
        {
            get { return this.m_therapyBlock.m_dStdDeviation_Frequency; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Freq_StdDev");
            }
        }

        public double Conc_Mean
        {
            get { return this.m_therapyBlock.m_dMean_Concreteness; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Conc_Mean");
            }
        }

        public double Conc_StdDev
        {
            get { return this.m_therapyBlock.m_dStdDeviation_Concreteness; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Conc_StdDev");
            }
        }

        public double DistractorNum_Mean
        {
            get { return this.m_therapyBlock.m_dMean_DistractorNum; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("DistractorNum_Mean");
            }
        }

        public double DistractorNum_StdDev
        {
            get { return this.m_therapyBlock.m_dStdDeviation_DistractorNum; }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("DistractorNum_StdDev");
            }
        }

        /*public List<int> LsChallengeItemIdx
        {
            get { return this.m_therapyBlock.m_lsChallengeItemIdx; }
            set
            {
                //this.lsTextBlockBackground = value;
                OnPropertyChanged("LsChallengeItemIdx");
            }
        }*/

        public List<string> LsChallengeItemName
        {
            get
            {
                List<string> lsName = new List<string>();
                for (int i = 0; i < this.m_therapyBlock.m_lsChallengeItemFeaturesIdx.Count; i++)
                {
                    int intIdx = m_lsChallengeItemFeatures[this.m_therapyBlock.m_lsChallengeItemFeaturesIdx[i]].m_intChallengeItemIdx;
                    lsName.Add(m_lsChallengeItem[intIdx].m_strName);
                }
                return lsName;
            }
            set
            {
                //this.lsTextBlockBackground = value;
                OnPropertyChanged("LsChallengeItemName");
            }
        }

        public List<SolidColorBrush> LsChallengeItemColour
        {
            get
            {
                List<SolidColorBrush> lsColour = new List<SolidColorBrush>();
                for (int i = 0; i < this.m_lsResponseAccuracy.Count; i++)
                {
                    SolidColorBrush colour = new SolidColorBrush();
                    if (this.m_lsResponseAccuracy[i] == 1)
                        colour.Color = Colors.Green;                      
                    else
                        colour.Color = Colors.Red;
                    lsColour.Add(colour);                    
                }
                return lsColour;
            }
            set
            {
                //this.lsTextBlockBackground = value;
                OnPropertyChanged("LsChallengeItemColour");
            }
        }

        public List<int> LsDistractorNum
        {
            get
            {
                List<int> lsDistractorNum = new List<int>();
                for (int i = 0; i < this.m_therapyBlock.m_lsChallengeItemFeaturesIdx.Count; i++)
                {
                    int intNum = 0;
                    double dComplexityDistractorNum = m_lsChallengeItemFeatures[this.m_therapyBlock.m_lsChallengeItemFeaturesIdx[i]].m_dComplexity_DistractorNum;
                    if (dComplexityDistractorNum == 0.1)
                        intNum = 2;
                    else if (dComplexityDistractorNum == 0.4)
                        intNum = 3;
                    else if (dComplexityDistractorNum == 0.7)
                        intNum = 4;
                    else if (dComplexityDistractorNum == 1)
                        intNum = 5;

                    lsDistractorNum.Add(intNum);
                }
                return lsDistractorNum;
            }
            set
            {
                //this.lsTextBlockBackground = value;
                OnPropertyChanged("LsDistractorNum");
            }
        }


        public List<int> LsFrequency
        {
            get
            {
                List<int> lsFrequency = new List<int>();
                for (int i = 0; i < this.m_therapyBlock.m_lsChallengeItemFeaturesIdx.Count; i++)
                {
                    //int intChallengeItemIdx = m_lsChallengeItemFeatures[this.m_therapyBlock.m_lsChallengeItemFeaturesIdx[i]].m_intChallengeItemIdx;
                    //int intLexcicalItemIdx = m_lsChallengeItem[intChallengeItemIdx].m_intLexicalItemIdx;
                    lsFrequency.Add(m_lsChallengeItemFeatures[this.m_therapyBlock.m_lsChallengeItemFeaturesIdx[i]].m_intFrequency);
                }
                return lsFrequency;
            }
            set
            {
                OnPropertyChanged("LsFrequency");
            }
        }

        public List<int> LsConcreteness
        {
            get
            {
                List<int> lsConcreteness = new List<int>();
                for (int i = 0; i < this.m_therapyBlock.m_lsChallengeItemFeaturesIdx.Count; i++)
                {
                    //int intIdx = m_lsChallengeItemFeatures[this.m_therapyBlock.m_lsChallengeItemFeaturesIdx[i]].m_intChallengeItemIdx;
                    //int intLexcicalItemIdx = m_lsChallengeItem[intIdx].m_intLexicalItemIdx;
                    lsConcreteness.Add(m_lsChallengeItemFeatures[this.m_therapyBlock.m_lsChallengeItemFeaturesIdx[i]].m_intConcreteness);
                }
                return lsConcreteness;
            }
            set
            {
                OnPropertyChanged("LsConcreteness");
            }
        }

        public List<double> LsChallengeItemComplexity
        {
            get
            {
                List<double> lsComplexity = new List<double>();
                for (int i = 0; i < this.m_therapyBlock.m_lsChallengeItemFeatures_Complexity.Count; i++)
                {
                    lsComplexity.Add(this.m_therapyBlock.m_lsChallengeItemFeatures_Complexity[i]);
                }
                return lsComplexity;
            }
            set
            {
                OnPropertyChanged("LsChallengeItemComplexity");
            }
        }

        public List<int> LsChallengeItemDiversity
        {
            get
            {
                List<int> lsDiversity = new List<int>();
                for (int i = 0; i < this.m_therapyBlock.m_lsIsDiversity.Count; i++)
                {
                    lsDiversity.Add(this.m_therapyBlock.m_lsIsDiversity[i]);
                }
                return lsDiversity;
            }
            set
            {
                OnPropertyChanged("LsChallengeItemDiversity");
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------
        // OnPropertyChanged
        //----------------------------------------------------------------------------------------------------
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
