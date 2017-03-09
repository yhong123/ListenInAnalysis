using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ListenInAnalysis.Classes
{
    class CBindDGDailyTherapyTime : INotifyPropertyChanged
    {
        private int m_intIdx;
        public string m_strDate;
        public double m_dTherapyTimeMin;
        public double m_dGameTimeMin;

        public event PropertyChangedEventHandler PropertyChanged;
        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        //----------------------------------------------------------------------------------------------------
        // CBindDGDailyTherapyTime
        //----------------------------------------------------------------------------------------------------
        public CBindDGDailyTherapyTime(int intI, string strDate, double dTherapyTimeMin, double dGameTimeMin)
        {
            m_intIdx = intI;
            m_strDate = strDate;
            m_dTherapyTimeMin = dTherapyTimeMin;
            m_dGameTimeMin = dGameTimeMin;
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

        public string Date
        {
            get
            {
                return this.m_strDate;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("Date");
            }
        }
        
        public double TherapyTime
        {
            get
            {
                return this.m_dTherapyTimeMin;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("TherapyTime");
            }
        }

        public double GameTime
        {
            get
            {
                return this.m_dGameTimeMin;
            }
            set
            {
                //this.intIdx = value;
                OnPropertyChanged("GameTime");
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
