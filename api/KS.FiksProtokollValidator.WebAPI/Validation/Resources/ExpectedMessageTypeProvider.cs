﻿using System.Collections.Generic;

namespace KS.FiksProtokollValidator.WebAPI.Validation.Resources
{
    public static class ExpectedResponseMessageTypeProvider
    {
        private static Dictionary<string, List<string>> _expectedMessageTypes;

        private static Dictionary<string, List<string>> InitializeExpectedMessageTypes()
        {
            return new Dictionary<string, List<string>>
            {
                {
                    WebAPI.Resources.RequestMessageTypes.ArkivmeldingV1,
                    new List<string>
                    {
                        WebAPI.Resources.ResponseMessageTypes.MottattV1,
                        WebAPI.Resources.ResponseMessageTypes.KvitteringV1
                    }
                },
                {
                    WebAPI.Resources.RequestMessageTypes.OppdaterSaksmappeV1,
                    new List<string>
                    {
                        WebAPI.Resources.ResponseMessageTypes.MottattV1,
                        WebAPI.Resources.ResponseMessageTypes.KvitteringV1
                    }
                },
                {
                    WebAPI.Resources.RequestMessageTypes.SoekV1,
                    new List<string>
                    {
                        WebAPI.Resources.ResponseMessageTypes.InnsynSoekResultatV1
                    }
                },
            };
        }

        public static List<string> GetExpectedResponseMessageTypes(string requestMessageType)
        {
            _expectedMessageTypes ??= InitializeExpectedMessageTypes();

            return _expectedMessageTypes[requestMessageType];
        }
    }
}