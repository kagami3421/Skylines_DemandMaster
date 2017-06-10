#define HARD

using UnityEngine;

namespace DemandMaster
{
    public static class FeatureUtil
    {
        public static int GetCurrentRCIValue(RCIType _type)
        {
            int _tmp = 0;

            if (ColossalFramework.Singleton<ZoneManager>.exists)
            {
                ZoneManager _ZoneManager = ColossalFramework.Singleton<ZoneManager>.instance;

                switch (_type)
                {
                    case RCIType.Residential:
                        _tmp = _ZoneManager.m_residentialDemand;
                        break;
                    case RCIType.Commercial:
                        _tmp = _ZoneManager.m_commercialDemand;
                        break;
                    case RCIType.Industrial:
                        _tmp = _ZoneManager.m_workplaceDemand;
                        break;
                }
            }
            else
            {
                Debug.LogError("Can't find ZoneManager !");
            }

            return _tmp;
        }

        public static void SetRCIValue(RCIType _type, int value)
        {
            if (ColossalFramework.Singleton<ZoneManager>.exists)
            {
                ZoneManager _ZoneManager = ColossalFramework.Singleton<ZoneManager>.instance;

                switch (_type)
                {
                    case RCIType.Residential:
                        {
                            _ZoneManager.m_residentialDemand = value;
#if HARD
                            _ZoneManager.m_actualResidentialDemand = value;
#endif
                        }
                        break;
                    case RCIType.Commercial:
                        {
#if HARD
                            _ZoneManager.m_actualCommercialDemand = value;
#endif
                            _ZoneManager.m_commercialDemand = value;
                        }
                        break;
                    case RCIType.Industrial:
                        {
                            _ZoneManager.m_workplaceDemand = value;
#if HARD
                            _ZoneManager.m_actualWorkplaceDemand = value;
#endif
                        }
                        break;
                }
            }
            else
            {
                Debug.LogError("Can't find ZoneManager !");
            }
        }
    }
}
