#define HARD

using ColossalFramework;

namespace DemandMaster
{
    public static class FeatureUtil
    {
        private static readonly ZoneManager _ZoneManager = Singleton<ZoneManager>.instance;

        public static int GetCurrentRCIValue(RCIType _type)
        {
            int _tmp = 0;

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

            return _tmp;
        }

        public static void SetRCIValue(RCIType _type, int value)
        {
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
    }
}
