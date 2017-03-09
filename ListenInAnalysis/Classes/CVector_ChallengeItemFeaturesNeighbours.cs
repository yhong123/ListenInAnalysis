using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenInAnalysis.Classes
{
    class CChallengeItemFeatures_Neighbour
    {
        public int m_intChallengeItemFeaturesIdx = 0;
        public double m_dSimilarity = 0;

        //----------------------------------------------------------------------------------------------------
        // CChallengeItemFeatures_Neighbour
        //----------------------------------------------------------------------------------------------------
        public CChallengeItemFeatures_Neighbour()
        {
        }

    }

    class CVector_ChallengeItemFeaturesNeighbours
    {
        public List<CChallengeItemFeatures_Neighbour> m_lsChallengeItemFeatures_Neighbour = new List<CChallengeItemFeatures_Neighbour>();

        //----------------------------------------------------------------------------------------------------
        // CVector_ChallengeItemFeatures
        //----------------------------------------------------------------------------------------------------
        public CVector_ChallengeItemFeaturesNeighbours()
        {
        }

    }
}
