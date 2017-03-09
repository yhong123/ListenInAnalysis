using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ListenInAnalysis.Classes
{
    class CBindDGChallengeItem : INotifyPropertyChanged
    {
        CChallengeItem m_challengeItem;
        private int m_intIdx;
        private int m_intPresentedCtr;
        private int m_intCorrectCtr;

        List<CLexicalItem> m_lsLexicalItem;

        public event PropertyChangedEventHandler PropertyChanged;
        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        //----------------------------------------------------------------------------------------------------
        // CBindDGChallengeItem
        //----------------------------------------------------------------------------------------------------
        public CBindDGChallengeItem(int intI, CChallengeItem challengeItem, int intPresentedCtr, int intCorrectCtr, List<CLexicalItem> lsLexicalItem)
        {
            m_intIdx = intI;
            m_challengeItem = challengeItem;
            m_intPresentedCtr = intPresentedCtr;
            m_intCorrectCtr = intCorrectCtr;
            m_lsLexicalItem = lsLexicalItem;
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
                return this.m_challengeItem.m_strName; // + " ( " + this.m_challengeItem.m_intDistractorNum + " )";
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
                int intIdx = m_challengeItem.m_intLexicalItemIdx;
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
                //int intIdx = m_challengeItem.m_intLexicalItemIdx;
                return this.m_challengeItem.m_intFrequency;
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
                //int intIdx = m_challengeItem.m_intLexicalItemIdx;
                return this.m_challengeItem.m_intConcreteness;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Concreteness");
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
