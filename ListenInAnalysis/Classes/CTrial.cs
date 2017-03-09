using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenInAnalysis.Classes
{   

    //************************************************************************************************************************
    // CStimulus
    //************************************************************************************************************************
    public class CStimulus
    {
        // each stimulus has a associated name and image file
        public int intOriginalIdx = -1;  // the idx as in the corpus chellengeitem.xml, need to keep track this coz the foils will be randomised bef presenting
        public string m_strName;
        public string m_strImage;
        public string m_strType;  // target / phonological / semantic / unrelated
        public string m_strPType;

        //----------------------------------------------------------------------------------------------------
        // CStimulus
        //----------------------------------------------------------------------------------------------------
        public CStimulus()
        {
            intOriginalIdx = -1;
            m_strName = "";
            m_strImage = "";
            m_strType = "";
            m_strPType = "";
        }
    }

    //************************************************************************************************************************
    // CTrial 
    //************************************************************************************************************************
    public class CTrial
    {
        public int m_intChallengeItemFeaturesIdx = -1;
        public int m_intChallengeItemIdx = -1;
        public List<CStimulus> m_lsStimulus;  // each trial will have a dynamic no of stimulus - 3 / 4 / 5 / 6
        public string m_strTargetAudio;   // audio of the target stimulus
        public int m_intTargetIdx;  // index of the target stimulus in lsStimulus

        //----------------------------------------------------------------------------------------------------
        // CTrial
        //----------------------------------------------------------------------------------------------------
        public CTrial()
        {
            m_intChallengeItemFeaturesIdx = -1;
            m_intChallengeItemIdx = -1;
            m_lsStimulus = new List<CStimulus>();
            m_strTargetAudio = "";
            m_intTargetIdx = -1;
        }

        //----------------------------------------------------------------------------------------------------
        // RandomizeStimuli
        //----------------------------------------------------------------------------------------------------
        public void RandomizeStimuli()
        {
            /*for (int i = 0; i < m_lsStimulus.Count; i++)
            {
                CStimulus temp = m_lsStimulus[i];
                int intRandomIndex = UnityEngine.Random.Range(i, m_lsStimulus.Count);
                m_lsStimulus[i] = m_lsStimulus[intRandomIndex];
                m_lsStimulus[intRandomIndex] = temp;
            }

            // find target in the randomized stimuli
            for (var i = 0; i < m_lsStimulus.Count; i++)
            {
                if (m_lsStimulus[i].m_strType.Equals("target"))
                {
                    m_intTargetIdx = i;
                    break;
                }
            }*/
        }

        //----------------------------------------------------------------------------------------------------
        // Update
        //----------------------------------------------------------------------------------------------------
        void Update()
        {

        }
    }

    //************************************************************************************************************************
    // CResponse 
    //************************************************************************************************************************
    public class CResponse
    {
        public List<int> m_lsSelectedStimulusIdx;  // list of seelcted stimulus idx
        public float m_fRTSec;
        public int m_intReplayBtnCtr;  // how many times tthe replay button has been clicked
        public int m_intScore;   // score earned
        public int m_intCoinNum;  // no of coins earned - rf coin reward schedule

        //----------------------------------------------------------------------------------------------------
        // CResponse
        //----------------------------------------------------------------------------------------------------
        public CResponse()
        {
            m_lsSelectedStimulusIdx = new List<int>();
            m_fRTSec = 0f;
            m_intReplayBtnCtr = 0;
            m_intScore = 0;
            m_intCoinNum = 0;
        }

        //----------------------------------------------------------------------------------------------------
        // Reset
        //----------------------------------------------------------------------------------------------------
        public void Reset()
        {
            m_lsSelectedStimulusIdx.Clear();
            m_fRTSec = 0f;
            m_intReplayBtnCtr = 0;
            m_intScore = 0;
            m_intCoinNum = 0;
        }
    }

    public class CBlockDetail
    {
        public int m_intBlockIdx = -1;
        public List<CTrial> m_lsTrial = new List<CTrial>();
        public List<CResponse> m_lsResponse = new List<CResponse>();
    }

}
