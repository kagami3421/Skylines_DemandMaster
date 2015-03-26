using System;
using System.Collections.Generic;
using UnityEngine;
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
                        _ZoneManager.m_actualResidentialDemand = value;
                    }
                    break;
                case RCIType.Commercial:
                    {
                        _ZoneManager.m_actualCommercialDemand = value;
                        _ZoneManager.m_commercialDemand = value;
                    }
                    break;
                case RCIType.Industrial:
                    {
                        _ZoneManager.m_workplaceDemand = value;
                        _ZoneManager.m_actualWorkplaceDemand = value;
                    }
                    break;
            }
        }
    }
}
