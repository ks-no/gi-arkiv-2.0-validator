using System;
using System.Collections.Generic;
using System.IO;
using KS.Fiks.IO.Client;
using KS.Fiks.IO.Client.Models;
using KS.GIArkivValidator.WebAPI.Models;

namespace KS.GIArkivValidator.WebAPI.FiksIO
{
    public class FiksRequestMessageService : IFiksRequestMessageService
    {
        private readonly Guid _senderId;
        private FiksIOClient _client;

        public FiksRequestMessageService()
        {
            var config = FiksIOConfigurationProvider.GetFromConfigurationFile();

            _client = new FiksIOClient(config);

            _senderId = config.KontoConfiguration.KontoId;
        }

        public Guid Send(FiksRequest fiksRequest, Guid receiverId)
        {
            var messageRequest = new MeldingRequest(_senderId, receiverId, fiksRequest.TestCase.MessageType);

            var payloads = new List<IPayload>();

            var payLoadFileName = fiksRequest.TestCase.PayloadFileName;
            var testsDirectory = "../KS.GIArkivValidator.TestCases/Tests/"; 
            var testCaseDirectory = Path.Combine(testsDirectory, fiksRequest.TestCase.TestName);
            var payLoadFilePath = Path.Combine(testCaseDirectory, payLoadFileName);

            IPayload payload = new StringPayload(File.ReadAllText(payLoadFilePath), payLoadFileName);

            payloads.Add(payload);
            
            var attachmentFileNames = fiksRequest.TestCase.PayloadAttachmentFileNames;

            if (!string.IsNullOrEmpty(attachmentFileNames))
            {
                foreach (var payloadFileName in attachmentFileNames.Split(Path.PathSeparator))
                {
                    var fileStream = File.OpenRead(Path.Combine(testCaseDirectory, "Attachments", payloadFileName));
                    payloads.Add(new StreamPayload(fileStream, payloadFileName));
                }
            }

            fiksRequest.SentAt = DateTime.Now;
            var result = _client.Send(messageRequest, payloads).Result;

            return result.MeldingId;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
