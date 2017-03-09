using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ListenInAnalysis.Classes
{
    class CBindDGLexicalItem : INotifyPropertyChanged
    {       
        CLexicalItem m_lexicalItem;
        private int m_intIdx;
        private int m_intPresentedCtr;
        private int m_intCorrectCtr;

        public event PropertyChangedEventHandler PropertyChanged;
        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        //----------------------------------------------------------------------------------------------------
        // CBindDGLexicalItem
        //----------------------------------------------------------------------------------------------------
        public CBindDGLexicalItem(int intI, CLexicalItem lexicalItem, int intPresentedCtr, int intCorrectCtr)
        {
            m_intIdx = intI;
            m_lexicalItem = lexicalItem;
            m_intPresentedCtr = intPresentedCtr;
            m_intCorrectCtr = intCorrectCtr;
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
                return this.m_lexicalItem.m_strName;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Name");
            }
        }

        /*public int Frequency
        {
            get
            {
                return this.m_lexicalItem.m_intFrequency;
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
                return this.m_lexicalItem.m_intConcreteness;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Concreteness");
            }
        }*/

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
