using System.Collections.Generic;

namespace Assets.Dialogues
{
    public class DialoguePart
    {
        public string Speech { get; }
        private IList<Response> Responses { get; }

        public DialoguePart(string speech, IList<Response> responses)
        {
            Speech = speech;
            Responses = responses;
        }

        public IList<string> RetrieveResponses()
        {
            List<string> responseTexts = new List<string>();

            foreach (var response in Responses)
            {
                responseTexts.Add(response.Text);
            }

            return responseTexts;
        }

        public bool IsResponseAvailable(int responseIdentifier)
        {
            return Responses.Count > responseIdentifier;
        }

        public int RetrieveNextDialoguePartIdentifier(int responseIdentifier)
        {
            return Responses[responseIdentifier].NextSpeechIdentifier;
        }
    }
}