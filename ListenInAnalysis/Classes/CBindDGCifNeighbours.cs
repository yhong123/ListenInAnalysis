using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ListenInAnalysis.Classes
{
    class CBindDGCifNeighbours : INotifyPropertyChanged
    {
        CChallengeItemFeatures m_challengeItemFeatures;
        private int m_intIdx;
        public double m_dSimilarity = 0;
        private int m_intPresentedCtr;
        private int m_intCorrectCtr;

        List<CLexicalItem> m_lsLexicalItem;
        List<CChallengeItem> m_lsChallengeItem;

        public event PropertyChangedEventHandler PropertyChanged;
        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        //----------------------------------------------------------------------------------------------------
        // CBindDGCifNeighbours
        //----------------------------------------------------------------------------------------------------
        public CBindDGCifNeighbours(int intI, CChallengeItemFeatures challengeItemFeatures, double dSimilarity, int intPresentedCtr, int intCorrectCtr, List<CLexicalItem> lsLexicalItem, List<CChallengeItem> lsChallengeItem)
        {
            m_intIdx = intI;
            m_challengeItemFeatures = challengeItemFeatures;
            m_dSimilarity = dSimilarity;
            m_intPresentedCtr = intPresentedCtr;
            m_intCorrectCtr = intCorrectCtr;
            m_lsLexicalItem = lsLexicalItem;
            m_lsChallengeItem = lsChallengeItem;
        }

        //----------------------------------------------------------------------------------------------------
        // Getters and Setters
        //----------------------------------------------------------------------------------------------------
        #region Properties Getters and Setters
        public int Idx
        {
            get { return this.m_intIdx; }
            set
            {
                this.m_intIdx = value;
                OnPropertyChanged("Idx");
            }
        }

        public string Name
        {
            get
            {
                string strName = m_lsChallengeItem[m_challengeItemFeatures.m_intChallengeItemIdx].m_strName;                               

                return strName + " ( " + m_challengeItemFeatures.m_intDistractorNum + " )";
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Name");
            }
        }

        public string LexicalItem
        {
            get
            {
                int intIdx = m_lsChallengeItem[m_challengeItemFeatures.m_intChallengeItemIdx].m_intLexicalItemIdx;
                return this.m_lsLexicalItem[intIdx].m_strName;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("LexicalItem");
            }
        }

        public int Frequency
        {
            get
            {
                //int intIdx = m_lsChallengeItem[m_challengeItemFeatures.m_intChallengeItemIdx].m_intLexicalItemIdx;
                return this.m_challengeItemFeatures.m_intFrequency;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Frequency");
            }
        }

        public int Concreteness
        {
            get
            {
                //int intIdx = m_lsChallengeItem[m_challengeItemFeatures.m_intChallengeItemIdx].m_intLexicalItemIdx;
                return this.m_challengeItemFeatures.m_intConcreteness;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Concreteness");
            }
        }

        public int DistractorNum
        {
            get
            {                
                return m_challengeItemFeatures.m_intDistractorNum;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("DistractorNum");
            }
        }

        public double Similarity
        {
            get
            {
                return this.m_dSimilarity;
            }
            set
            {
                OnPropertyChanged("Similarity");
            }
        }

        public int PresentedCtr
        {
            get
            {
                return this.m_intPresentedCtr;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("PresentedCtr");
            }
        }

        public int CorrectCtr
        {
            get
            {
                return this.m_intCorrectCtr;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("CorrectCtr");
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
